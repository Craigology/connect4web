
using System;

namespace Connect4.Core.Domain
{
    public class Location : IEquatable<Location>
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

        public bool Equals(Location other)
        {
            return LocationRow == other.LocationRow
                   && LocationCol == other.LocationCol
                   && Occupied == other.Occupied;
        }

        public override string ToString()
        {
            return $@"[{LocationRow}, {LocationCol}] : {Occupied}";
        }
    }
}
