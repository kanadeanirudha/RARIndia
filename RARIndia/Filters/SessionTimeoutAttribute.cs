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
            HttpContext ctx = HttpContext.Current;
            UserModel userModel = RARIndiaSessionHelper.GetDataFromSession<UserModel>(RARIndiaConstant.UserDataSession);
            if (userModel == null)
            {
                filterContext.Result = new RedirectResult("~/User/Login");
                return;
            }
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName?.ToLower();
            if (!userModel.MenuList.Any(x => x.MenuLink?.ToLower() == controllerName) && controllerName != "dashboard")
            {
                filterContext.Result = new RedirectResult("~/User/Unauthorized");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}