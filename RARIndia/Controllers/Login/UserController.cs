using RARIndia.BusinessLogicLayer;
using RARIndia.Model;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System.Web.Mvc;
using System.Web.Security;

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
                if (!string.IsNullOrEmpty(userLoginViewModel.EmailID) && !string.IsNullOrEmpty(userLoginViewModel.Password))
                {
                    userLoginViewModel = _userMasterBA.Login(userLoginViewModel);
                    if (!userLoginViewModel.HasError)
                    {
                        FormsAuthentication.SetAuthCookie(userLoginViewModel.EmailID, false);
                        return RedirectToAction<DashboardController>(x => x.Index());
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
            userModel.SelectedModuleCode = moduleCode;
            RARIndiaSessionHelper.SaveDataInSession<UserModel>(RARIndiaConstant.UserDataSession, userModel);
            return RedirectToAction(method, controllerName);
        }
    }
}