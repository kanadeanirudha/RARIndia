using System.Web.Mvc;

namespace RARIndia.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController()
        {
        }

        public ActionResult Index()
        {
            return View($"~/Views/Dashboard/Index.cshtml");
        }

    }
}