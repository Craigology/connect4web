using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using Connect4.Core.Domain;
using Newtonsoft.Json;

namespace Connect4.Controllers
{
    public class BoardController : ApiController
    {
        private IHttpSessionState GetSessionState()
        {
            return SessionStateUtility.GetHttpSessionStateFromContext(HttpContext.Current);
        }

        public void NewBoard(int numberOfRows, int numberOfColumns)
        {
            var board = new Board(numberOfRows, numberOfColumns);
        }

        // GET: api/Api/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Api
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Api/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Api/5
        public void Delete(int id)
        {
        }
    }
}
