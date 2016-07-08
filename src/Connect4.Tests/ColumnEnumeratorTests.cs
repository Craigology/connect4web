using System;
using Connect4.Core;
using Connect4.Core.Domain;
using Connect4.Core.Enumerators;
using NUnit.Framework;
using Shouldly;

namespace Connect4.Tests
{
    [TestFixture]
    public class ColumnEnumeratorTests
    {
        [Test]
        public void ColumnEnumerator_InitialState_InvalidUntilMoveNextCalled()
        {
            var board = new Board(3, 3);
            var sut = new ColumnEnumerator(board, 2);

            Location location;

            Assert.Throws<IndexOutOfRangeException>(() => location = sut.Current);

            sut.MoveNext();

            Assert.DoesNotThrow(() => location = sut.Current);
        }

        [Test]
        public void ColumnEnumerator_MoveNext_ReturnsTheExpectedLocation()
        {
            var board = new Board(3, 3);
            board[0, 1].Occupied = Occupied.Red;
            board[1, 1].Occupied = Occupied.Yellow;
            board[2, 1].Occupied = Occupied.Red;

            var sut = new ColumnEnumerator(board, 1);

            sut.MoveNext().ShouldBe(true);
            sut.Current.Occupied.ShouldBe(Occupied.Red);
            sut.MoveNext().ShouldBe(true);
            sut.Current.Occupied.ShouldBe(Occupied.Yellow);
            sut.MoveNext().ShouldBe(true);
            sut.Current.Occupied.ShouldBe(Occupied.Red);
            sut.MoveNext().ShouldBe(false);
        }
    }
}
