namespace Connect4.Dtos
{
    public class TurnDto
    {
        public int LocationRow { get; set; }
        public int LocationCol { get; set; }
        public bool IsWinningTurn { get; set; }
        public bool IsDraw { get; set; }
    }
}