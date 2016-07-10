using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using Connect4.Core.Domain;

namespace Connect4.Controllers
{
    public class BoardController : ApiController
    {
        // GET: api/Api
        public string[] NewBoard(int numberOfRows, int numberOfColumns)
        {
            var board = new Board(numberOfRows, numberOfColumns);

            return new string[] { "value1", "value2" };
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
