using System;
using Connect4.Core.Domain;
using Connect4.Core.Enumerators;
using NUnit.Framework;
using Shouldly;

namespace Connect4.Tests
{
    [TestFixture]
    public class RowEnumeratorTests
    {
        [Test]
        public void RowEnumerator_InitialState_InvalidUntilMoveNextCalled()
        {
            var board = new Board(3, 3);
            var sut = new RowEnumerator(board, 2);
            
            Assert.Throws<IndexOutOfRangeException>(() => { var location = sut.Current; });

            sut.MoveNext();

            Assert.DoesNotThrow(() => { var location = sut.Current; });
        }

        [Test]
        public void RowEnumerator_MoveNext_ReturnsTheExpectedLocation()
        {
            var board = new Board(3, 3);
            board[1, 0].Occupied = Occupied.Red;
            board[1, 1].Occupied = Occupied.Yellow;
            board[1, 2].Occupied = Occupied.Red;

            var sut = new RowEnumerator(board, 1);

            sut.MoveNext();
            sut.Current.Occupied.ShouldBe(Occupied.Red);
            sut.MoveNext();
            sut.Current.Occupied.ShouldBe(Occupied.Yellow);
            sut.MoveNext();
            sut.Current.Occupied.ShouldBe(Occupied.Red);
        }

        [Test]
        public void RowEnumerator_MoveNext_ReturnsFalseAtEndOfRow()
        {
            var board = new Board(3, 3);
            board[1, 0].Occupied = Occupied.Red;
            board[1, 1].Occupied = Occupied.Yellow;
            board[1, 2].Occupied = Occupied.Red;

            var sut = new RowEnumerator(board, 1);

            sut.MoveNext().ShouldBe(true);
            sut.MoveNext().ShouldBe(true);
            sut.MoveNext().ShouldBe(true);
            sut.MoveNext().ShouldBe(false);
        }
    }
}
