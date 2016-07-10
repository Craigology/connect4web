using System.Collections;
using System.Collections.Generic;
using Connect4.Core.Domain;

namespace Connect4.Core.Enumerators
{
    public class DiagonalDownEnumerator : IEnumerator<Location>, IEnumerable<Location>
    {
        private readonly Board _board;
        private int _rowPosition;
        private int _columnPosition;

        public Location Current => _board.Locations[_rowPosition, _columnPosition];
        object IEnumerator.Current => Current;

        public DiagonalDownEnumerator(Board board, int rowPosition, int columnPosition)
        {
            _board = board;
            _rowPosition = rowPosition;
            _columnPosition = columnPosition;

            Reset();
        }

        public bool MoveNext()
        {
            _rowPosition++;
            _columnPosition++;

            return _rowPosition < _board.NumberOfRows && _columnPosition < _board.NumberOfColumns;
        }

        public void Reset()
        {
            do
            {
                _rowPosition--;
                _columnPosition--;

            } while (_rowPosition >= 0 && _columnPosition >= 0);
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