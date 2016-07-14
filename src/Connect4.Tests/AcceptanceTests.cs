using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Connect4.Core.Domain;
using NUnit.Framework;
using Shouldly;

namespace Connect4.Tests
{
    [TestFixture]
    public class AcceptanceTests
    {
        [Test]
        //0 [Y][ ][ ][ ][ ]
        //1 [R][ ][ ][ ][ ]
        //2 [R][ ][ ][ ][ ]
        //3 [Y][Y][Y][Y][ ]  <--------
        //4 [R][R][R][Y][R]
        //   0  1  2  3  4
        public void Scenario_One_Yellow_Wins_Horizontal()
        {
            var board = new Board(5, 5);
            var turn = board.RedTurn(0);
            turn.Location.ShouldBe(new Location(4, 0) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(0);
            turn.Location.ShouldBe(new Location(3, 0) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(4);
            turn.Location.ShouldBe(new Location(4, 4) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(3);
            turn.Location.ShouldBe(new Location(4, 3) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(2);
            turn.Location.ShouldBe(new Location(4, 2) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(3);
            turn.Location.ShouldBe(new Location(3, 3) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(0);
            turn.Location.ShouldBe(new Location(2, 0) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(2);
            turn.Location.ShouldBe(new Location(3, 2) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(0);
            turn.Location.ShouldBe(new Location(1, 0) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(0);
            turn.Location.ShouldBe(new Location(0, 0) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(1);
            turn.Location.ShouldBe(new Location(4, 1) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(1);
            turn.Location.ShouldBe(new Location(3, 1) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(true);

            Console.WriteLine($"Yellow wins in {board.TurnCount} turns.");
        }

        [Test]
        //         |
        //         |
        //         v
        //0 [ ][ ][ ][ ][R]
        //1 [ ][ ][ ][ ][R]
        //2 [ ][ ][Y][ ][R]
        //3 [ ][Y][R][ ][R] 
        //4 [Y][Y][R][Y][Y]
        //   0  1  2  3  4
        public void Scenario_Two_Red_Wins_Vertical()
        {
            var board = new Board(5, 5);
            var turn = board.YellowTurn(0);
            turn.Location.ShouldBe(new Location(4, 0) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(2);
            turn.Location.ShouldBe(new Location(4, 2) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(4);
            turn.Location.ShouldBe(new Location(4, 4) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(2);
            turn.Location.ShouldBe(new Location(3, 2) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(2);
            turn.Location.ShouldBe(new Location(2, 2) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(4);
            turn.Location.ShouldBe(new Location(3, 4) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(1);
            turn.Location.ShouldBe(new Location(4, 1) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(4);
            turn.Location.ShouldBe(new Location(2, 4) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(1);
            turn.Location.ShouldBe(new Location(3, 1) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(4);
            turn.Location.ShouldBe(new Location(1, 4) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(3);
            turn.Location.ShouldBe(new Location(4, 3) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(4);
            turn.Location.ShouldBe(new Location(0, 4) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(true);

            Console.WriteLine($"Red wins in {board.TurnCount} turns.");
        }

        [Test]
        //                   /
        //                  /
        //                |/_
        //0 [ ][ ][ ][ ][ ]
        //1 [ ][ ][ ][Y][ ]
        //2 [ ][ ][Y][R][R]
        //3 [ ][Y][R][R][R] 
        //4 [Y][Y][R][Y][Y]
        //   0  1  2  3  4
        public void Scenario_Three_Yellow_Wins_Diagonal()
        {
            var board = new Board(5, 5);
            var turn = board.YellowTurn(0);
            turn.Location.ShouldBe(new Location(4, 0) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(2);
            turn.Location.ShouldBe(new Location(4, 2) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(4);
            turn.Location.ShouldBe(new Location(4, 4) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(2);
            turn.Location.ShouldBe(new Location(3, 2) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(2);
            turn.Location.ShouldBe(new Location(2, 2) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(2);
            turn.Location.ShouldBe(new Location(1, 2) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(1);
            turn.Location.ShouldBe(new Location(4, 1) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(3);
            turn.Location.ShouldBe(new Location(4, 3) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(1);
            turn.Location.ShouldBe(new Location(3, 1) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(3);
            turn.Location.ShouldBe(new Location(3, 3) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(3);
            turn.Location.ShouldBe(new Location(2, 3) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.RedTurn(1);
            turn.Location.ShouldBe(new Location(2, 1) { Occupied = Occupied.Red });
            turn.IsWinningTurn.ShouldBe(false);
            turn = board.YellowTurn(3);
            turn.Location.ShouldBe(new Location(1, 3) { Occupied = Occupied.Yellow });
            turn.IsWinningTurn.ShouldBe(true);

            Console.WriteLine($"Yellow wins in {board.TurnCount} turns.");
        }

        [Test]
        public void Scenario_Four_Draw()
        {
            var board = new Board(5, 5);
            var turn = board.YellowTurn(3);
            turn = board.RedTurn(0);
            turn = board.YellowTurn(3);
            turn = board.RedTurn(4);
            turn = board.YellowTurn(3);
            turn = board.RedTurn(1);
            turn = board.YellowTurn(2);
            turn = board.RedTurn(3);
            turn = board.YellowTurn(4);
            turn = board.RedTurn(2);
            turn = board.YellowTurn(0);
            turn = board.RedTurn(2);
            turn = board.YellowTurn(2);
            turn = board.RedTurn(3);
            turn = board.YellowTurn(0);
            turn = board.RedTurn(0);
            turn = board.YellowTurn(0);
            turn = board.RedTurn(4);
            turn = board.YellowTurn(1);
            turn = board.RedTurn(1);
            turn = board.YellowTurn(1);
            turn = board.RedTurn(1);
            turn = board.YellowTurn(4);
            turn = board.RedTurn(4);
            turn.IsDraw.ShouldBe(false);
            board.IsDraw.ShouldBe(false);

            turn = board.YellowTurn(2);
     
            turn.IsDraw.ShouldBe(true);
            board.IsDraw.ShouldBe(true);
        }

        [Test]
        public void Scenario_Five_Invalid_Board_Dimensions()
        {
        }

        [Test]
        public void Scenario_Six_Invalid_Move()
        {
            DrawGenerator();
        }

        /// <summary>
        /// Call from a test to iterate games until a Draw is detected, and show the Turn sequence that was used.
        /// </summary>
        private void DrawGenerator()
        {
            int iteration = 0;
            bool isDraw = false;
            byte[] seedGenerator = new Byte[4];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            while (!isDraw)
            {
                rng.GetBytes(seedGenerator);
                int seed = BitConverter.ToInt32(seedGenerator, 0);
                var rand = new Random(seed);

                Console.WriteLine($"Iteration {++iteration}");
                Console.Out.Flush();

                var board = new Board(5, 5);
                for (;;)
                {
                    Turn yelloWTurn = board.YellowTurn(rand.Next(5));
                    for (; !yelloWTurn.IsWinningTurn && yelloWTurn == Turn.InvalidTurn && board.TurnCount < 25; yelloWTurn = board.YellowTurn(rand.Next(5)))
                        ;

                    Console.WriteLine($"{yelloWTurn.Location}");

                    Turn redTurn = board.RedTurn(rand.Next(5));
                    for (; !redTurn.IsWinningTurn && redTurn == Turn.InvalidTurn && board.TurnCount < 25; redTurn = board.RedTurn(rand.Next(5)))
                        ;

                    Console.WriteLine($"{redTurn.Location}");

                    if (yelloWTurn.IsWinningTurn)
                    {
                        Console.WriteLine($"YELLOW won in {board.TurnCount} turns.");
                        break;
                    }
                    if (redTurn.IsWinningTurn)
                    {
                        Console.WriteLine($"RED won in {board.TurnCount} turns.");
                        break;
                    }
                    if (board.TurnCount == 25)
                    {
                        isDraw = true;
                        Console.WriteLine("DRAW!");
                        break;
                    }
                }
            }
        }
    }
}
