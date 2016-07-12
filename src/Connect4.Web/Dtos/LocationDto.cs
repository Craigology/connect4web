namespace Connect4.Dtos
{
    public class LocationDto
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsYellow { get; set; }
        public bool IsRed { get; set; }
        public bool IsWin { get; set; }
    }
}