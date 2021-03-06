﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace BombSquad
{
    class TheMap
    {
        public enum GameStateEnum { INIT = 0, PLAY, WON, LOST, END };
        enum CellImage { ZERO = 0, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, BOMB, BANG, OOPS, HIDDEN, FLAG, WIN };
        [Flags]
        enum CellState { VISIBLE = 0, HIDDEN = 1, FLAG = 2 };

        int _Rows;         // y
        int _Columns;      // x
        int _Bombs;        // number of bombs
        CellImage[,] _Cells;     // the grid
        CellState[,] _States;   // the visibility of the grid
        int _CellWidth;
        Bitmap _CellGraphics;
        Timer _gameTimer;
        int _gameTimeElapsed;
        int _flagsPlaced;
        Label _lbBombsRemaining;
        Label _lbTimeElapsed;

        GameStateEnum _GameState;
        public GameStateEnum GameState { get { return _GameState; } }

        private HighScores _highScores;

        const int DEFAULT_SIZE = 5;
        const float DEFAULT_BOMB_RATIO = 0.2f;
        const float MAX_BOMB_RATIO = 0.5f;

        public TheMap(int columns, int rows, int bombs, int cellWidth, Label bombLabel, Label timeLabel)
        {
            _Columns = columns;
            _Rows = rows;
            _Bombs = bombs;
            _CellWidth = cellWidth;
            _lbBombsRemaining = bombLabel;
            _lbTimeElapsed = timeLabel;

            // get the high scores
            _highScores = new HighScores();

            if ( _Columns < 1 ) _Columns = DEFAULT_SIZE;
            if ( _Rows < 1 ) _Rows = DEFAULT_SIZE;

            int total_cells = _Columns * _Rows;
            if ( _Bombs < 1 || _Bombs > ( int )( total_cells * MAX_BOMB_RATIO ) ) _Bombs = ( int )( total_cells * DEFAULT_BOMB_RATIO );

            // Resource containing cell graphics
            //_CellGraphics = Properties.Resources.Cells;
            _CellGraphics = Properties.Resources.Cells30;

            // setup the timer
            _gameTimer = new Timer();
            _gameTimer.Interval = 1000;
            _gameTimer.Tick += _gameTimer_Tick;

            InitialiseMap();
        }

        private void _gameTimer_Tick(object sender, EventArgs e)
        {
            if (_gameTimeElapsed < 999)
            {
                _gameTimeElapsed++;
            }

            SetTimeLabelText();
        }

        private void SetTimeLabelText()
        {
            int thou = _gameTimeElapsed / 100;
            int rmdr = _gameTimeElapsed % 100;
            int tens = rmdr / 10;
            int units = rmdr % 10;

            string value = string.Format("{0}{1}{2}", thou, tens, units);
            _lbTimeElapsed.Text = value;
        }

        private void SetBombLabelText()
        {
            int bombsLeft = _Bombs - _flagsPlaced;
            if (bombsLeft < 0)
            {
                bombsLeft = 0;
            }

            int thou =  bombsLeft / 100;
            int rmdr = bombsLeft % 100;
            int tens = rmdr / 10;
            int units = rmdr % 10;

            string value = string.Format("{0}{1}{2}", thou, tens, units);
            _lbBombsRemaining.Text = value;
        }

        public void InitialiseMap()
        {
            _Cells = new CellImage[_Columns, _Rows];
            _States = new CellState[_Columns, _Rows];

            // set all cells to empty and hidden
            for ( int x = 0; x < _Columns; x++ )
            {
                for ( int y = 0; y < _Rows; y++ )
                {
                    _Cells[x, y] = CellImage.ZERO;
                    _States[x, y] = CellState.HIDDEN;
                }
            }

            // stop and reset the timer
            _gameTimer.Enabled = false;
            _gameTimeElapsed = 0;
            SetTimeLabelText();

            _flagsPlaced = 0;
            SetBombLabelText();

            _GameState = GameStateEnum.INIT;
        }

        public void InitialiseMap(int Columns, int Rows, int Bombs)
        {
            _Bombs = Bombs;
            _Columns = Columns;
            _Rows = Rows;

            InitialiseMap();
        }

        public void Draw(Graphics device, Bitmap surface)
        {
            Rectangle destRect;
            int xcellpos;
            int ycellpos;
            int cell_index;

            // draw all of the cells
            for ( int x = 0; x < _Columns; x++ )
            {
                for ( int y = 0; y < _Rows; y++ )
                {
                    // calculate destination rectangle
                    int xpos = x * _CellWidth;
                    int ypos = y * _CellWidth;
                    destRect = new Rectangle( xpos, ypos, _CellWidth, _CellWidth );

                    if ( _States[x, y].HasFlag( CellState.FLAG ) )
                    {
                        if ( _GameState == GameStateEnum.WON )
                            cell_index = ( int )CellImage.WIN;
                        else
                            cell_index = ( int )CellImage.FLAG;
                    }
                    else if ( _States[x, y].HasFlag( CellState.HIDDEN ) )
                    {
                        if ( _GameState == GameStateEnum.WON )
                            cell_index = ( int )CellImage.WIN;
                        else
                            cell_index = ( int )CellImage.HIDDEN;
                    }
                    else
                    {
                        cell_index = ( int )_Cells[x, y];
                    }

                    int ycell = ( int )( cell_index / 4 );
                    int xcell = cell_index % 4;

                    xcellpos = xcell * _CellWidth;
                    ycellpos = ycell * _CellWidth;

                    // draw the cell graphics
                    device.DrawImage( _CellGraphics, destRect, xcellpos, ycellpos, 
                        _CellWidth, _CellWidth, GraphicsUnit.Pixel );
                }
            }
        }

        public void Click( int X, int Y, MouseButtons button)
        {
            Trace.WriteLine("Click");

            // generate the map
            if (_GameState == GameStateEnum.INIT)
            {
                // Generate the map
                Generate(X, Y);
            }

            // check for end
            if ( _GameState == GameStateEnum.END || _GameState == GameStateEnum.WON )
            {
                if ( button == MouseButtons.Middle )
                    // reset the map
                    InitialiseMap();
            }
            else if ( _States[X, Y] != CellState.VISIBLE )
            {
                // handle right click, placing and removing flags indicating "found" objects
                if ( button == MouseButtons.Right )
                {
                    if ( _States[X, Y].HasFlag( CellState.FLAG ) )
                    {
                        _States[X, Y] &= ~CellState.FLAG;
                        if (--_flagsPlaced < 0) _flagsPlaced = 0;
                        SetBombLabelText();
                    }
                    else if ( _States[X, Y].HasFlag( CellState.HIDDEN ) )
                    {
                        _States[X, Y] |= CellState.FLAG;
                        ++_flagsPlaced;
                        SetBombLabelText();
                    }
                }
                else
                // handle left-click on a hidden cell
                {
                    if ( button == MouseButtons.Left )
                    {
                        ClearCell( X, Y );
                    }
                }
            }
            else if ( button == MouseButtons.Middle )
            {
                // middle-clicks on VISIBLE cells
                // if the cell has a number and is surrounded by that many flags it will
                // clear other cells around it, if any
                QuickClear(X, Y);
            }
        }

        private void ClearCell( int X, int Y )
        {
            // don't handle left-clicks on FLAGS
            if ( _States[X, Y] == CellState.HIDDEN )
            {
                _States[X, Y] = CellState.VISIBLE;

                // if a zero, flood fill
                if ( _Cells[X, Y] == CellImage.ZERO )
                {
                    Floodfill( X, Y );
                }

                // check for bomb
                if ( _Cells[X,Y] == CellImage.BOMB)
                {
                    // change it to a BANG
                    if ( _GameState == GameStateEnum.PLAY )
                    {
                        _Cells[X, Y] = CellImage.BANG;
                        _GameState = GameStateEnum.END;

                        // stop the timer
                        _gameTimer.Enabled = false;

                        ShowBombs();
                    }
                }
            }

            // TODO: only check win if not ended 
            CheckWin();
        }

        private void CheckWin()
        {
            int HiddenCells = 0;
            for ( int x = 0; x < _Columns; x++ )
            {
                for ( int y = 0; y < _Rows; y++ )
                {
                    if ( _States[x, y].HasFlag( CellState.HIDDEN ) )
                    {
                        HiddenCells++;
                    }
                }
            }

            if ( HiddenCells == _Bombs && _GameState == GameStateEnum.PLAY )
            {
                // only hidden cells are Bombs
                _GameState = GameStateEnum.WON;

                // stop the timer
                _gameTimer.Enabled = false;

                // change all hidden cells to green flags
                for (int x = 0; x < _Columns; x++)
                {
                    for (int y = 0; y < _Rows; y++)
                    {
                        if (_States[x, y].HasFlag(CellState.HIDDEN))
                        {
                            _Cells[x, y] = CellImage.WIN;
                            _flagsPlaced++;
                            SetBombLabelText();

                        }
                    }
                }
            }
        }

        public void CheckHighScores()
        {
            // check for high score
            // this needs to be fixed
            if (_Bombs == 10)
            {
                // easy mode
                int timeTaken = Convert.ToInt32(_lbTimeElapsed.Text);
                int previousBest = Convert.ToInt32(_highScores.EasyScore.Score);

                if (timeTaken < previousBest)
                {
                    // new high score
                    Form nameForm = new GetNameForm();

                    // set message for the form
                    string message = "Congratulations, you have achieved a new fastest time for Easy difficulty.";
                    ((Label)nameForm.Controls["lblMessage"]).Text = message;
                    DialogResult result = nameForm.ShowDialog();
                    TextBox txtName = (TextBox)nameForm.Controls["txtName"];
                    string name = txtName.Text;
                    if (String.IsNullOrWhiteSpace(name))
                    {
                        name = "Unknown";
                    }
                    _highScores.newEasyHighScore(name, timeTaken.ToString());
                    _highScores.SaveHighScores();
                }
            }
            else if (_Bombs == 40)
            {
                // medium
                int timeTaken = Convert.ToInt32(_lbTimeElapsed.Text);
                int previousBest = Convert.ToInt32(_highScores.MediumScore.Score);

                if (timeTaken < previousBest)
                {
                    // new high score
                    Form nameForm = new GetNameForm();
                    string message = "Congratulations, you have achieved a new fastest time for Medium difficulty.";
                    ((Label)nameForm.Controls["lblMessage"]).Text = message;
                    DialogResult result = nameForm.ShowDialog();
                    TextBox txtName = (TextBox)nameForm.Controls["txtName"];
                    string name = txtName.Text;
                    if (String.IsNullOrWhiteSpace(name))
                    {
                        name = "Unknown";
                    }
                    _highScores.newMediumHighScore(name, timeTaken.ToString());
                    _highScores.SaveHighScores();
                }
            }
            else if (_Bombs == 99)
            {
                int timeTaken = Convert.ToInt32(_lbTimeElapsed.Text);
                int previousBest = Convert.ToInt32(_highScores.HardScore.Score);

                if (timeTaken < previousBest)
                {
                    // new high score
                    Form nameForm = new GetNameForm();
                    string message = "Congratulations, you have achieved a new fastest time for Hard difficulty.";
                    ((Label)nameForm.Controls["lblMessage"]).Text = message;
                    DialogResult result = nameForm.ShowDialog();
                    TextBox txtName = (TextBox)nameForm.Controls["txtName"];
                    string name = txtName.Text;
                    if (String.IsNullOrWhiteSpace(name))
                    {
                        name = "Unknown";
                    }
                    _highScores.newHardHighScore(name, timeTaken.ToString());
                    _highScores.SaveHighScores();
                }
            }
        }

        private void ShowBombs()
        {
            for ( int x = 0; x < _Columns; x++ )
            {
                for ( int y = 0; y < _Rows; y++ )
                {
                    if ( _States[x,y] == CellState.HIDDEN )
                    {
                        if ( _Cells[x,y] == CellImage.BOMB )
                        {
                            ClearCell( x, y );
                        }
                    }
                    if ( _States[x,y].HasFlag(CellState.FLAG))
                    {
                        if ( _Cells[x, y] != CellImage.BOMB )
                        {
                            _States[x, y] = CellState.HIDDEN;
                            _Cells[x, y] = CellImage.OOPS;
                            ClearCell( x, y );
                        }
                    }
                }
            }
        }

        // Generates the map
        void Generate(int col, int row)
        {
            Random rand = new Random();
            int hits = 0;

            for ( int i = 0; i < _Bombs; i++ )
            {
                // get random col
                int x = rand.Next( 0, _Columns );
                int y = rand.Next( 0, _Rows );

                while ( ( x == col && y == row ) || _Cells[x,y] == CellImage.BOMB)
                {
                    hits++;
                    x = rand.Next( 0, _Columns );
                    y = rand.Next( 0, _Rows );
                }

                _Cells[x, y] = CellImage.BOMB;

                // add one to surrounding cells
                for ( int j = x - 1; j <= x + 1; j++ )
                {
                    for ( int k = y - 1; k <= y + 1; k++ )
                    {
                        if ( (j >= 0 && j < _Columns) && ( k >= 0 && k < _Rows ))
                        {
                            if ( _Cells[j,k] != CellImage.BOMB )
                            {
                                _Cells[j, k] = ( CellImage )( ( int )_Cells[j, k] + 1 );
                            }
                        }
                    }
                }
            }

            // start the timer
            _gameTimer.Enabled = true;
            _gameTimeElapsed = 0;

            _GameState = GameStateEnum.PLAY;
        }

        // Floodfill - clears all zero cells
        void Floodfill(int col, int row)
        {
            // row above
            for ( int y = row - 1; y <= row + 1; y++ )
            {
                for ( int x = col - 1; x <= col + 1; x++ )
                {
                    // only check valid indexes
                    if ( ( x >= 0 && x < _Columns ) && ( y >= 0 && y < _Rows ) )
                    {
                        if ( _States[x,y] == CellState.HIDDEN && _Cells[x,y] != CellImage.BOMB )
                        {
                            ClearCell(x, y);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles two button click on visible cell
        /// If the cells surrounding this cell have flags equal to the number
        /// in this cell then clear all other surrounding cells
        /// </summary>
        /// <param name="X">column of this cell</param>
        /// <param name="Y">row of this cell</param>
        public void QuickClear(int X, int Y)
        {
            Trace.WriteLine("QuickClear");
            int surroundFlags = 0; // number of flags set around this cell

            if ( _States[X,Y] == CellState.VISIBLE)
            {
                if ( _Cells[X,Y] >= CellImage.ONE && _Cells[X,Y] <= CellImage.EIGHT)
                {
                    // TODO: get surrounding cells - in a list
                    List<Point> surroundCells = new List<Point>();
                    for ( int i = X - 1; i <= X + 1; i++)
                    {
                        for ( int j = Y - 1; j <= Y + 1; j++)
                        {
                            if ( ( i >= 0 && i < _Columns) && (j >= 0 && j < _Rows) )
                            {
                                // valid cell
                                if ( _States[i,j] != CellState.VISIBLE )
                                {
                                    surroundCells.Add(new Point(i,j));
                                    // count flags set
                                    if (_States[i,j].HasFlag(CellState.FLAG))
                                    {
                                        surroundFlags++;
                                    }
                                }
                            }
                        }
                    }
                    
                    // if # flags >= number of this cell
                    if ( surroundFlags >= (int)_Cells[X,Y])
                    {
                        // clear all hidden cells
                        foreach(Point p in surroundCells)
                        {
                            if ( _GameState == GameStateEnum.PLAY)
                                if ( _States[p.X,p.Y] == CellState.HIDDEN)
                                    ClearCell(p.X,p.Y);
                        }
                    }
                }
            }
        }

        public void ShowHighScores()
        {
            Form showHighScores = new HighScoresForm();
            ((Label)showHighScores.Controls["lblEasyName"]).Text = _highScores.EasyScore.Name;
            ((Label)showHighScores.Controls["lblEasyScore"]).Text = _highScores.EasyScore.Score;
            ((Label)showHighScores.Controls["lblMediumName"]).Text = _highScores.MediumScore.Name;
            ((Label)showHighScores.Controls["lblMediumScore"]).Text = _highScores.MediumScore.Score;
            ((Label)showHighScores.Controls["lblHardName"]).Text = _highScores.HardScore.Name;
            ((Label)showHighScores.Controls["lblHardScore"]).Text = _highScores.HardScore.Score;

            showHighScores.ShowDialog();
        }
    }
}
