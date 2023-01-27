using RARIndia.Filters;
using RARIndia.ViewModel;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class AdminSnPostsController : BaseController
    {
        public AdminSnPostsController()
        {

        }

        public ActionResult List()
        {
            return View($"~/Views/Admin/AdminSnPosts/List.cshtml", new AdminSnPostsListViewModel());
        }
    }
}