using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using Connect4.Core.Domain;
using Connect4.Dtos;
using Newtonsoft.Json;

namespace Connect4.Controllers
{
    public class BoardController : ApiController
    {
        private const string RoutePrefix = "api/board/";
        private IHttpSessionState GetSessionState()
        {
            return SessionStateUtility.GetHttpSessionStateFromContext(HttpContext.Current);
        }

        [Route(RoutePrefix +"new/{numberOfRows}/{numberOfColumns}")]
        public void New(int numberOfRows, int numberOfColumns)
        {
            var board = new Board(numberOfRows, numberOfColumns);
        }

        [Route(RoutePrefix + "yellowTurn/{column}")]
        public TurnDto YellowTurn(int column)
        {
            return new TurnDto();
        }

        [Route(RoutePrefix + "redTurn/{column}")]
        public TurnDto RedTurn(int column)
        {
            return new TurnDto();
        }
    }
}
