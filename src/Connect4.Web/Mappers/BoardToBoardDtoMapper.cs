using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Connect4.Core.Domain;
using Connect4.Dtos;

namespace Connect4.Mappers
{
    public class BoardToBoardDtoMapper : IMapper<Board, BoardDto>
    {
        private readonly IMapper<Location, LocationDto> _locationMapper;

        public BoardToBoardDtoMapper(IMapper<Location, LocationDto> locationMapper)
        {
            _locationMapper = locationMapper;
        }

        public BoardDto Map(Board from)
        {
            return new BoardDto
            {
                NumberOfRows = from.NumberOfRows,
                NumberOfColumns = from.NumberOfColumns,
                Locations = from.Locations.Cast<Location>().Select(x => _locationMapper.Map(x)).ToArray()
            };
        }
    }
}