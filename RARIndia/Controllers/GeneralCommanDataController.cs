using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    //[SessionTimeoutAttribute]
    public class GeneralCommanDataController : BaseController
    {
        readonly GeneralDepartmentMasterBA _generalDepartmentMasterBA = null;
        readonly GeneralDesignationMasterBA _generalDesignationMasterBA = null;
        public GeneralCommanDataController()
        {
            _generalDepartmentMasterBA = new GeneralDepartmentMasterBA();
            _generalDesignationMasterBA = new GeneralDesignationMasterBA();
        }

        public ActionResult GetDepartmentsByCentreCode(string centreCode)
        {
            GeneralDepartmentListModel list = _generalDepartmentMasterBA.GetDepartmentsByCentreCode(centreCode);
            return PartialView($"~/Views/Shared/_DepartmentDropdown.cshtml", list);
        }

        public ActionResult GetDesignation(int designationID)
        {
            GeneralDesignationListModel list = _generalDesignationMasterBA.GetDesignations(designationID);
            return PartialView($"~/Views/Shared/_DesignationDropdown.cshtml", list);
        }
    }
}