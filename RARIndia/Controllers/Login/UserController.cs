using RARIndia.ViewModel;

using System.Web.Mvc;
using System.Web.Security;

namespace RARIndia.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        public UserController()
        {
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml", new UserLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(registrationViewModel.EmailAddress) && !string.IsNullOrEmpty(registrationViewModel.Password))
                {
                    //UserModel model = new GrievanceUserDetailsBusinessLogic().Login(registrationViewModel.EmailAddress, registrationViewModel.Password);
                    //if (!model.HasError)
                    if (registrationViewModel.EmailAddress == "admin@rarindia.com" && registrationViewModel.Password == "admin@rarindia.com")
                    {
                        FormsAuthentication.SetAuthCookie(registrationViewModel.EmailAddress, false);
                        return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
                    }  
                }
            }
            ModelState.AddModelError("", "Invalid Email Address or Password");
            return View("~/Views/Login/Login.cshtml", registrationViewModel);
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction<UserController>(x => x.Login());
        }
    }
}