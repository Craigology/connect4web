using Connect4.Core.Domain;
using Connect4.Dtos;

namespace Connect4.Mappers
{
    public class LocationToLocationDtoMapper : IMapper<Location, LocationDto>
    {
        public LocationDto Map(Location @from)
        {
            return new LocationDto
            {
                Col = from.LocationCol,
                Row = from.LocationRow,
                IsRed = from.Occupied == Occupied.Red,
                IsYellow = from.Occupied == Occupied.Yellow,
                IsWin = false // TODO
            };
        }
    }
}