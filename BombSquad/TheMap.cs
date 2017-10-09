using System;
using System.Drawing;

namespace BombSquad
{
    class TheMap
    {
        int _Rows;         // y
        int _Columns;      // x
        int _Bombs;        // number of bombs
        Cellstate[,] _Cells;     // the grid
        bool _Generated;    // map generated flag
        Bitmap _CellGraphics;

        [Flags]
        enum Cellstate { ZERO = 0, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, BOMB, BANG, OOPS, HIDDEN, FLAG };

        const int DEFAULT_SIZE = 5;
        const float DEFAULT_BOMB_RATIO = 0.2f;
        const float MAX_BOMB_RATIO = 0.5f;

        public TheMap(int columns, int rows, int bombs)
        {

            _Columns = columns;
            _Rows = rows;
            _Bombs = bombs;

            if ( _Columns < 1 ) _Columns = DEFAULT_SIZE;
            if ( _Rows < 1) _Rows = DEFAULT_SIZE;

            int total_cells = _Columns * _Rows;
            if ( _Bombs < 1 || _Bombs > (int)( total_cells * MAX_BOMB_RATIO ) ) _Bombs = (int)( total_cells * DEFAULT_BOMB_RATIO );

            _Cells = new Cellstate[_Columns, _Rows];

            // set all cells to empty and hidden
            for (int x = 0; x < _Columns; x++ )
            {
                for ( int y = 0; y < _Rows; y++ )
                {
                    _Cells[x, y] = Cellstate.ZERO | Cellstate.HIDDEN;
                }
            }

            _CellGraphics = new Bitmap( "Cells.png" );

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

                    if ( _Cells[x, y].HasFlag( Cellstate.FLAG) )
                    {
                        cell_index = ( int )Cellstate.FLAG;
                    }
                    else if ( _Cells[x, y].HasFlag( Cellstate.HIDDEN ) )
                    {
                        // determine coords of cell graphics
                        cell_index = ( int )Cellstate.HIDDEN;
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
    }
}
