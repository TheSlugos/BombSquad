using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSquad
{
    class TheMap
    {
        int _Rows;         // y
        int _Columns;      // x
        int _Bombs;        // number of bombs
        int[,] _Cells;     // the grid
        bool _Generated;    // map generated flag

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

            _Cells = new int[_Columns, _Rows];

            _Generated = false;
        }

    }
}
