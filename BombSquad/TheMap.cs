using System;
using System.Drawing;
using System.Windows.Forms;

namespace BombSquad
{
    class TheMap
    {
        enum GameState { INIT = 0, PLAY, WON, LOST, END };
        enum CellImage { ZERO = 0, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, BOMB, BANG, OOPS, HIDDEN, FLAG, WIN };
        [Flags]
        enum CellState { VISIBLE = 0, HIDDEN = 1, FLAG = 2 };

        int _Rows;         // y
        int _Columns;      // x
        int _Bombs;        // number of bombs
        CellImage[,] _Cells;     // the grid
        CellState[,] _States;   // the visibility of the grid
        Bitmap _CellGraphics;

        GameState _GameState;



        const int DEFAULT_SIZE = 5;
        const float DEFAULT_BOMB_RATIO = 0.2f;
        const float MAX_BOMB_RATIO = 0.5f;

        public TheMap(int columns, int rows, int bombs)
        {

            _Columns = columns;
            _Rows = rows;
            _Bombs = bombs;

            if ( _Columns < 1 ) _Columns = DEFAULT_SIZE;
            if ( _Rows < 1 ) _Rows = DEFAULT_SIZE;

            int total_cells = _Columns * _Rows;
            if ( _Bombs < 1 || _Bombs > ( int )( total_cells * MAX_BOMB_RATIO ) ) _Bombs = ( int )( total_cells * DEFAULT_BOMB_RATIO );
            _CellGraphics = new Bitmap( "Cells.png" );

            InitialiseMap();
        }

        private void InitialiseMap()
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

            _GameState = GameState.INIT;
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
                    int xpos = x * 40;
                    int ypos = y * 40;
                    destRect = new Rectangle( xpos, ypos, 40, 40 );

                    if ( _States[x, y].HasFlag( CellState.FLAG ) )
                    {
                        if ( _GameState == GameState.WON )
                            cell_index = ( int )CellImage.WIN;
                        else
                            cell_index = ( int )CellImage.FLAG;
                    }
                    else if ( _States[x, y].HasFlag( CellState.HIDDEN ) )
                    {
                        if ( _GameState == GameState.WON )
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

                    xcellpos = xcell * 40;
                    ycellpos = ycell * 40;

                    // draw the cell graphics
                    device.DrawImage( _CellGraphics, destRect, xcellpos, ycellpos, 40, 40, GraphicsUnit.Pixel );
                }
            }
        }

        public void Click( int X, int Y, MouseButtons button)
        {
            // check for end
            if ( _GameState == GameState.END || _GameState == GameState.WON )
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
                    }
                    else if ( _States[X, Y].HasFlag( CellState.HIDDEN ) )
                    {
                        _States[X, Y] |= CellState.FLAG;
                    }
                }
                else
                // handle left-click on a hidden cell
                {
                    if ( button == MouseButtons.Left )
                    {
                        if ( _GameState == GameState.INIT )
                        {
                            // Generate the map
                            Generate( X, Y );
                        }

                        ClearCell( X, Y );
                    }
                }
            }
            else if ( button == MouseButtons.Middle )
            {
                // middle-clicks on VISIBLE cells
                // if the cell has a number and is surrounded by that many flags it will
                // clear other cells around it, if any
                MessageBox.Show( "QuickClear" );
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
                    if ( _GameState == GameState.PLAY )
                    {
                        _Cells[X, Y] = CellImage.BANG;
                        _GameState = GameState.END;
                        ShowBombs();
                    }
                }
            }

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

            if ( HiddenCells == _Bombs )
            {
                // only hidden cells are Bombs
                _GameState = GameState.WON;

                // change all hidden cells to green flags
                for ( int x = 0; x < _Columns; x++ )
                {
                    for ( int y = 0; y < _Rows; y++ )
                    {
                        if ( _States[x, y].HasFlag( CellState.HIDDEN ) )
                        {
                            _Cells[x, y] = CellImage.WIN;
                        }
                    }
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

            _GameState = GameState.PLAY;
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
    }
}
