using RARIndia.ViewModel;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
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
                    //{
                    //    FormsAuthentication.SetAuthCookie(model.EmailAddress, false);
                    //    Session[Constant.UserSessionData] = model;
                    //    if (model?.EntityType == Constant.StudentEntityType)
                    //    {
                    //        return RedirectToAction("GrievanceList", "Student");
                    //    }
                    //    return RedirectToAction("GrievanceList", "Employee");
                    //}
                }
            }
            ModelState.AddModelError("", "Invalid Email Address or Password");
            return View("~/Views/Login/Login.cshtml", registrationViewModel);
        }
    }
}