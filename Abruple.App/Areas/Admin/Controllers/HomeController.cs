namespace Abruple.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        // ADMIN HOME
        public ActionResult Index()
        {
            return this.View();
        }
    }
}