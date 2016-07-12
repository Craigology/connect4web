namespace Connect4.Core.Domain
{
    public class Turn
    {
        public Location Location { get; set; }

        public bool IsWinningTurn { get; set; }

        public bool IsDraw { get; set; }

        public static Turn InvalidTurn = new Turn {  Location = null, IsWinningTurn = false };
    }
}
