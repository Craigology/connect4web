using System.Collections;
using System.Collections.Generic;
using Connect4.Core.Domain;

namespace Connect4.Core.Enumerators
{
    public class RowEnumerator : IEnumerator<Location>, IEnumerable<Location>
    {
        private readonly Board _board;
        private readonly int _rowNumber;
        private int _columnPosition;

        public Location Current => _board.Locations[_rowNumber, _columnPosition];
        object IEnumerator.Current => Current;

        public RowEnumerator(Board board, int rowNumber)
        {
            _board = board;
            _rowNumber = rowNumber;

            Reset();
        }

        public bool MoveNext()
        {
            _columnPosition++;

            return _columnPosition < _board.NumberOfColumns;
        }

        public void Reset()
        {
            _columnPosition = -1;
        }

        public void Dispose()
        {
        }

        public IEnumerator<Location> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}