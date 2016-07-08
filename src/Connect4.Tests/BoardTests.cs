using System;
using Connect4.Core;
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
    }
}
