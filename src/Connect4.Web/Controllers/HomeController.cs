using System.Web.Mvc;

namespace Connect4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var fred = Session["dude"];

            Session["dude"] = "xyz";

            return View();
        }
    }
}
