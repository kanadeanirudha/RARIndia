using RARIndia.Model;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;

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
            base.OnActionExecuting(filterContext);
        }
    }
}