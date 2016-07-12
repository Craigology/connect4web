using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using Connect4.Core.Domain;
using Connect4.Dtos;
using Connect4.Mappers;

namespace Connect4.Controllers
{
    public class BoardController : ApiController
    {
        private readonly IMapper<Board, BoardDto> _boardMapper;
        private readonly IMapper<Turn, TurnDto> _turnMapper;
        private const string RoutePrefix = "api/board/";
        private const string BoardSessionStateKey = "board";

        public BoardController(IMapper<Board, BoardDto> boardMapper, IMapper<Turn, TurnDto> turnMapper)
        {
            _boardMapper = boardMapper;
            _turnMapper = turnMapper;
        }

        private IHttpSessionState GetSessionState()
        {
            return SessionStateUtility.GetHttpSessionStateFromContext(HttpContext.Current);
        }

        public BoardDto Get()
        {
            var sessionState = GetSessionState();
            var board = (Board) sessionState[BoardSessionStateKey];
            return board != null ? _boardMapper.Map(board) : null;
        }

        [Route(RoutePrefix +"new/{numberOfRows}/{numberOfColumns}")]
        public BoardDto New(int numberOfRows, int numberOfColumns)
        {
            var board = new Board(numberOfRows, numberOfColumns);
            var sessionState = GetSessionState();
            sessionState[BoardSessionStateKey] = board;
            return _boardMapper.Map(board);
        }

        [Route(RoutePrefix + "yellowTurn/{column}")]
        public TurnDto YellowTurn(int column)
        {
            var sessionState = GetSessionState();
            var board = (Board)sessionState[BoardSessionStateKey];
            return board != null ? _turnMapper.Map(board.YellowTurn(column)) : null;
        }

        [Route(RoutePrefix + "redTurn/{column}")]
        public TurnDto RedTurn(int column)
        {
            var sessionState = GetSessionState();
            var board = (Board)sessionState[BoardSessionStateKey];
            return board != null ? _turnMapper.Map(board.RedTurn(column)) : null;
        }
    }
}
