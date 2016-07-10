using System;
using Connect4.Core.Domain;
using NUnit.Framework;

namespace Connect4.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void Board_NumberOfRows_ThrowsExceptionIfOutOfRange()
        {
            Assert.DoesNotThrow(() => new Board(1,1));

            Assert.Throws<ArgumentOutOfRangeException>(() => new Board(0, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Board(-1, 1));
        }

        [Test]
        public void Board_NumberOfColumns_ThrowsExceptionIfOutOfRange()
        {
            Assert.DoesNotThrow(() => new Board(1, 1));

            Assert.Throws<ArgumentOutOfRangeException>(() => new Board(1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Board(1, -1));
        }


        [Test]
        public void Board_RedTurn_ThrowsExceptionIfOutOfRange()
        {
            var sut = new Board(3, 3);

            Assert.DoesNotThrow(() => sut.RedTurn(2));

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.RedTurn(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.RedTurn(3));
        }

        [Test]
        public void Board_YellowTurn_ThrowsExceptionIfOutOfRange()
        {
            var sut = new Board(3, 3);

            Assert.DoesNotThrow(() => sut.YellowTurn(2));

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.YellowTurn(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.YellowTurn(3));
        }
    }
}
