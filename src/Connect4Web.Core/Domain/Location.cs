using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4Web.Core.Domain
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
        }


    }
}
