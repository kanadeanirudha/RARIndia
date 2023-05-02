using RARIndia.BusinessLogicLayer;
using RARIndia.Model;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;

namespace RARIndia.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        UserMasterBA _userMasterBA = null;

        public UserController()
        {
            _userMasterBA = new UserMasterBA();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml", new UserLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(userLoginViewModel.UserName) && !string.IsNullOrEmpty(userLoginViewModel.Password))
                {
                    userLoginViewModel = _userMasterBA.Login(userLoginViewModel);
                    if (!userLoginViewModel.HasError)
                    {
                        FormsAuthentication.SetAuthCookie(userLoginViewModel.UserName, false);
                        if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                        {
                            return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
                        }
                        else
                        {
                            return RedirectToAction<DashboardController>(x => x.Index());
                        }
                    }
                    ModelState.AddModelError("ErrorMessage", userLoginViewModel.ErrorMessage);
                }
            }
            else
            {
                ModelState.AddModelError("ErrorMessage", "Invalid Email Address or Password");
            }
            return View("~/Views/Login/Login.cshtml", userLoginViewModel);
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            RARIndiaSessionHelper.RemoveDataFromSession(RARIndiaConstant.UserDataSession);
            return RedirectToAction<UserController>(x => x.Login());
        }

        [HttpGet]
        public ActionResult GetMenuListByModuleCode(string moduleCode, string controllerName, string method)
        {
            UserModel userModel = RARIndiaSessionHelper.GetDataFromSession<UserModel>(RARIndiaConstant.UserDataSession);
            if (IsNotNull(userModel))
            {
                userModel.SelectedModuleCode = moduleCode;
                userModel.SelectedModuleName = string.Equals(moduleCode, "dashboard", System.StringComparison.InvariantCultureIgnoreCase) ? "Dashboard" : userModel.ModuleList.FirstOrDefault(x => x.ModuleCode == moduleCode)?.ModuleName;
                RARIndiaSessionHelper.SaveDataInSession<UserModel>(RARIndiaConstant.UserDataSession, userModel);
            }
            return RedirectToAction(method, controllerName);
        }

        [HttpGet]
        public ActionResult GetBalanceSheetById(int balanceSheetId, string controllerName, string method)
        {
            UserModel userModel = RARIndiaSessionHelper.GetDataFromSession<UserModel>(RARIndiaConstant.UserDataSession);
            if (IsNotNull(userModel))
            {
                userModel.SelectedBalanceId = balanceSheetId;
                userModel.SelectedBalanceSheet = userModel.BalanceSheetList.FirstOrDefault(x => x.BalsheetID == balanceSheetId)?.ActBalsheetHeadDesc;
                RARIndiaSessionHelper.SaveDataInSession<UserModel>(RARIndiaConstant.UserDataSession, userModel);
            }
            return RedirectToAction(method, controllerName);
        }

        public ActionResult GetNotificationCount(int userId)
        {
            int notificationCount = userId > 0 ? _userMasterBA.GetNotificationCount(userId) : 0;
            return View("~/Views/Shared/_NotificationCount.cshtml", notificationCount);
        }

        public ActionResult Unauthorized()
        {
            return View("~/Views/Shared/Unauthorized.cshtml");
        }
    }
}