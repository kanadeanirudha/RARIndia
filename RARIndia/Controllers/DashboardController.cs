using RARIndia.Filters;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
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