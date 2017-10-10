using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BombSquad
{
    class TheMap
    {
        int _Rows;         // y
        int _Columns;      // x
        int _Bombs;        // number of bombs
        CellImage[,] _Cells;     // the grid
        CellState[,] _States;   // the visibility of the grid
        bool _Generated;    // map generated flag
        Bitmap _CellGraphics;

        enum CellImage { ZERO = 0, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, BOMB, BANG, OOPS, HIDDEN, FLAG };
        [Flags]
        enum CellState { VISIBLE = 0, HIDDEN = 1, FLAG = 2 };

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
                    //_Cells[x, y] = CellImage.ZERO;
                    _Cells[x, y] = CellImage.ZERO;
                    //_States[x, y] = CellState.HIDDEN;
                }
            }

            _Generated = false;
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
                        cell_index = ( int )CellImage.FLAG;
                    }
                    else if ( _States[x, y].HasFlag( CellState.HIDDEN ) )
                    {
                        // determine coords of cell graphics
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
            if ( _Generated && button == MouseButtons.Middle )
            {
                InitialiseMap();
            }

            // clicking only results in an action if a cell is not visible, i.e. has not been clicked
            if ( _States[X, Y] != CellState.FLAG )  // VISIBLE
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
                        if (!_Generated)
                        {
                            // Generate the map
                            Generate( X, Y );
                        }

                        if ( _States[X,Y] == CellState.HIDDEN )
                        {
                            _States[X, Y] = CellState.VISIBLE;

                            // process the bomb click

                            // if an object, you lose
                            // if a zero, flood fill

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

            _Generated = true;
        }
    }
}
