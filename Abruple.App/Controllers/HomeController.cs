namespace Abruple.App.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        // HOME
        public ActionResult Index()
        {
            return View();
        }

        // HELP // TODO: TO SEPAATE IT IN HELP CONTROLLER OR TO LEAVE IT HERE
        public ActionResult Help()
        {
            return this.View();
        }
    }
}