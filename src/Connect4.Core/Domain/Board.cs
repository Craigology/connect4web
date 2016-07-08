using System;

namespace Connect4.Core.Domain
{
    public class Board
    {
        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }

        public Location[,] Locations { get; }

        public Location this[int r, int c] => Locations[r,c];

        public Board(int numberOfRows, int numberOfColumns)
        {
            if (numberOfRows <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfRows));
            if (numberOfColumns <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfColumns));

            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;

            Locations = new Location[NumberOfRows,NumberOfColumns];

            for (int colPosition = 0; colPosition < NumberOfColumns; colPosition++)
            {
                for (int rowPosition = 0; rowPosition < NumberOfRows; rowPosition++)
                {
                    Locations[rowPosition, colPosition] = new Location(rowPosition, colPosition);
                }
            }
        }

        public void RedTurn(int column)
        {
            ValidateRange(column);
        }

        public void YellowTurn(int column)
        {
            ValidateRange(column);
        }

        private void ValidateRange(int column)
        {
            if (column <= 0 || column > NumberOfColumns) throw new ArgumentOutOfRangeException(nameof(column));


        }
    }
}
