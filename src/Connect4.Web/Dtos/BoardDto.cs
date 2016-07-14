namespace Connect4.Dtos
{
    public class BoardDto
    {
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }
        public LocationDto[] Locations { get; set; }
        public bool IsNextTurnRed { get; set; }
        public bool IsNextTurnYellow { get; set; }
    }
}