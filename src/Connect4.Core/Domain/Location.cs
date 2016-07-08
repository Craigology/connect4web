
namespace Connect4.Core.Domain
{
    public class Location
    {
        public int LocationRow { get; }
        public int LocationCol { get; }

        public Occupied Occupied { get; set; }

        public Location(int locationRow, int locationCol)
        {
            LocationRow = locationRow;
            LocationCol = locationCol;

            Occupied = Occupied.None;
        }
    }
}
