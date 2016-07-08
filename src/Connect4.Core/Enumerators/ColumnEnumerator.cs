using System.Collections;
using System.Collections.Generic;
using Connect4.Core.Domain;

namespace Connect4.Core.Enumerators
{
    public class ColumnEnumerator : IEnumerator<Location>, IEnumerable<Location>
    {
        private readonly Board _board;
        private readonly int _columnNumber;
        private int _rowPosition;

        public Location Current => _board.Locations[_rowPosition, _columnNumber];
        object IEnumerator.Current => Current;

        public ColumnEnumerator(Board board, int columnNumber)
        {
            _board = board;
            _columnNumber = columnNumber;

            Reset();
        }

        public bool MoveNext()
        {
            _rowPosition++;

            return _rowPosition < _board.NumberOfRows;
        }

        public void Reset()
        {
            _rowPosition = -1;
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