using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.ViewModel;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class OrganisationMasterController : BaseController
    {
        OrganisationMasterBA _organisationMasterBA = null;
        public OrganisationMasterController()
        {
            _organisationMasterBA = new OrganisationMasterBA();
        }

        //Get Organisation Details
        [HttpGet]
        public virtual ActionResult GetOrganisationDetails()
        {
            OrganisationMasterViewModel organisationMasterViewModel = _organisationMasterBA.GetOrganisationDetails();
            return ActionView("~/Views/OrganisationMaster/CreateEdit.cshtml", organisationMasterViewModel);
        }
    }
}