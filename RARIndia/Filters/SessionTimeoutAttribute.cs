using RARIndia.Model;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RARIndia.Filters
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string[] excludeFromName = new string[] { "generalcommandata", "dashboard" };

            HttpContext ctx = HttpContext.Current;
            UserModel userModel = RARIndiaSessionHelper.GetDataFromSession<UserModel>(RARIndiaConstant.UserDataSession);
            if (userModel == null)
            {
                filterContext.Result = new RedirectResult("~/User/Login");
                return;
            }
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName?.ToLower();
            //if (!excludeFromName.Any(x => x == controllerName) && !userModel.MenuList.Any(x => x.ControllerName == controllerName))
            //{
            //    filterContext.Result = new RedirectResult("~/User/Unauthorized");
            //    return;
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}