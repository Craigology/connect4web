using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Connect4.Core.Enumerators;

namespace Connect4.Core.Domain
{
    public class Board
    {
        public const int WinningSequenceLength = 4;

        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }

        public Location[,] Locations { get; }

        public int TurnCount { get; private set; }

        public Location this[int r, int c] => Locations[r,c];

        public bool IsNextTurnRed { get; private set; }
        public bool IsNextTurnYellow { get; private set; }

        public Board(int numberOfRows, int numberOfColumns)
        {
            if (numberOfRows <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfRows));
            if (numberOfColumns <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfColumns));

            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
            Locations = new Location[NumberOfRows, NumberOfColumns];

            Reset();
            ChooseFirstTurn();
        }

        public Turn RedTurn(int column)
        {
            return TakeTurn(column, Occupied.Red);
        }

        public Turn YellowTurn(int column)
        {
            return TakeTurn(column, Occupied.Yellow);
        }

        private void Reset()
        {
            for (int colPosition = 0; colPosition < NumberOfColumns; colPosition++)
            {
                for (int rowPosition = 0; rowPosition < NumberOfRows; rowPosition++)
                {
                    Locations[rowPosition, colPosition] = new Location(rowPosition, colPosition);
                }
            }
        }

        private void ValidateRange(int column)
        {
            if (column < 0 || column >= NumberOfColumns) throw new ArgumentOutOfRangeException(nameof(column));
        }

        private void ChooseFirstTurn()
        {
            byte[] seedGenerator = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(seedGenerator);
            int seed = BitConverter.ToInt32(seedGenerator, 0);
            var rand = new Random(seed);
            if (rand.NextDouble() >= 0.5)
            {
                IsNextTurnRed = true;
                IsNextTurnYellow = false;
            }
            else
            {
                IsNextTurnRed = false;
                IsNextTurnYellow = true;
            }
        }

        private Turn TakeTurn(int column, Occupied occupied)
        {
            ValidateRange(column);

            var columnEnumerator = new ColumnEnumerator(this, column);

            var availableLocation = columnEnumerator.LastOrDefault(location => location.Occupied == Occupied.None);
            if (availableLocation == null) // column full
                return Turn.InvalidTurn;

            availableLocation.Occupied = occupied;

            TurnCount++;
            IsNextTurnRed = !IsNextTurnRed;
            IsNextTurnYellow = !IsNextTurnYellow;

            var isWin = IsWinningTurn(availableLocation);
            return new Turn { Location = availableLocation, IsWinningTurn = isWin, IsDraw = !isWin && IsDraw(), IsNextTurnRed = IsNextTurnRed, IsNextTurnYellow = IsNextTurnYellow };
        }

        private bool IsWinningTurn(Location location)
        {
            return
               CheckForSequence(new RowEnumerator(this, location.LocationRow), location.Occupied)
            || CheckForSequence(new ColumnEnumerator(this, location.LocationCol), location.Occupied)
            || CheckForSequence(new DiagonalDownEnumerator(this, location.LocationRow, location.LocationCol), location.Occupied)
            || CheckForSequence(new DiagonalUpEnumerator(this, location.LocationRow, location.LocationCol), location.Occupied);
        }

        private bool IsDraw()
        {
            return NumberOfColumns*NumberOfRows == TurnCount;
        }

        private bool CheckForSequence(IEnumerator<Location> enumerator, Occupied occupied)
        {
            int countInSequence = 0;
            while (enumerator.MoveNext())
            {
                if (occupied == enumerator.Current.Occupied)
                {
                    countInSequence++;
                    if (countInSequence == WinningSequenceLength)
                        return true;
                }
                else
                {
                    // There is a break in a sequence, seeya.
                    if (countInSequence > 0)
                        break;
                }
            }
            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var currentRow = 0; currentRow < NumberOfRows; currentRow++)
            {
                foreach (var location in new RowEnumerator(this, currentRow))
                {
                    sb.AppendFormat("[{0}]",
                        location.Occupied == Occupied.Yellow ? "Y" : location.Occupied == Occupied.Red ? "R" : " ");
                }
                sb.AppendLine();

            }

            return sb.ToString();
        }
    }
}
