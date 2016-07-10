using System;
using Connect4.Core.Domain;
using Connect4.Core.Enumerators;
using NUnit.Framework;
using Shouldly;

namespace Connect4.Tests
{
    [TestFixture]
    public class DiagonalDownEnumeratorTests
    {
        [Test]
        public void DiagonalDownEnumerator_InitialState_InvalidUntilMoveNextCalled()
        {
            var board = new Board(3, 3);
            var sut = new DiagonalDownEnumerator(board, 1, 1);

            Assert.Throws<IndexOutOfRangeException>(() => { var location = sut.Current; });

            sut.MoveNext();

            Assert.DoesNotThrow(() => { var location = sut.Current; });
        }

        [Test]
        public void DiagonalDownEnumerator_MoveNext_ReturnsTheExpectedLocation()
        {
            var board = new Board(3, 3);
            board[0, 0].Occupied = Occupied.Red;
            board[1, 1].Occupied = Occupied.Yellow;
            board[2, 2].Occupied = Occupied.Red;

            var sut = new DiagonalDownEnumerator(board, 1, 1);

            sut.MoveNext();
            sut.Current.Occupied.ShouldBe(Occupied.Red);
            sut.MoveNext();
            sut.Current.Occupied.ShouldBe(Occupied.Yellow);
            sut.MoveNext();
            sut.Current.Occupied.ShouldBe(Occupied.Red);
        }

        [Test]
        public void DiagonalDownEnumerator_MoveNext_ReturnsFalseAtEndOfDiagonal()
        {
            var board = new Board(3, 3);
            board[0, 0].Occupied = Occupied.Red;
            board[1, 1].Occupied = Occupied.Yellow;
            board[2, 2].Occupied = Occupied.Red;

            var sut = new DiagonalDownEnumerator(board, 1, 1);

            sut.MoveNext().ShouldBe(true);
            sut.MoveNext().ShouldBe(true);
            sut.MoveNext().ShouldBe(true);
            sut.MoveNext().ShouldBe(false);
        }
    }
}
