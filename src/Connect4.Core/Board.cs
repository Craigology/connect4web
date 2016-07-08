using System;

namespace Connect4Web.Core.Domain
{
    public class Board
    {
        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }

        public Board(int numberOfRows, int numberOfColumns)
        {
            if (numberOfRows <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfRows));
            if (numberOfColumns <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfColumns));

            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
        }
    }
}
