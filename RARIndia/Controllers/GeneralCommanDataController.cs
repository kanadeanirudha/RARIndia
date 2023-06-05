using RARIndia.Filters;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class GeneralCommanDataController : BaseController
    {
        public GeneralCommanDataController()
        {
        }

        //Get Departments By CentreCode
        public ActionResult GetDepartmentsByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseDepartment.ToString(),
                DropdownName = "SelectedDepartmentID",
                Parameter = centreCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }
    }
}