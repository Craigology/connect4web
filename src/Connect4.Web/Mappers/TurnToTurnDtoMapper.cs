using Connect4.Core.Domain;
using Connect4.Dtos;

namespace Connect4.Mappers
{
    public class TurnToTurnDtoMapper : IMapper<Turn, TurnDto>
    {
        public TurnDto Map(Turn @from)
        {
            return new TurnDto
            {
                LocationCol = from.Location?.LocationCol,
                LocationRow = from.Location?.LocationRow,
                IsInvalidTurn = Turn.InvalidTurn == from,
                IsWinningTurn = from.IsWinningTurn,
                IsDraw = from.IsDraw
            };
        }
    }
}