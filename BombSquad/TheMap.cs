using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSquad
{
    class TheMap
    {
        int m_Rows;         // y
        int m_Columns;      // x
        int m_Bombs;        // number of bombs
        int[,] m_Cells;     // the grid
        bool m_Generated;    // map generated flag

        [Flags]
        enum Cellstate { ZERO = 0, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, BOMB, BANG, OOPS, HIDDEN, FLAG };

        const int DEFAULT_SIZE = 5;
        const float DEFAULT_BOMB_RATIO = 0.2f;
        const float MAX_BOMB_RATIO = 0.5f;

        public TheMap(int columns, int rows, int bombs)
        {
            if ( columns < 1 ) columns = DEFAULT_SIZE;
            if (rows < 1) rows = DEFAULT_SIZE;

            int total_cells = columns * rows;
            if ( bombs < 1 || bombs > (int)( total_cells * MAX_BOMB_RATIO ) ) bombs = (int)( total_cells * DEFAULT_BOMB_RATIO );

            m_Cells = new int[columns, rows];

            m_Generated = false;
        }

    }
}
