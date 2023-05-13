using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
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
        public virtual ActionResult Edit()
        {
            OrganisationMasterViewModel organisationMasterViewModel = _organisationMasterBA.GetOrganisationDetails();
            return ActionView("~/Views/Organisation/OrganisationMaster/CreateEdit.cshtml", organisationMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(OrganisationMasterViewModel organisationMasterViewModel)
        {
            if (ModelState.IsValid)
            {

                bool status = true;//_generalCountryMasterBA.UpdateCountry(generalCountryViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable();
                    return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
                }
            }
            return View("~/Views/Organisation/OrganisationMaster/CreateEdit.cshtml", organisationMasterViewModel);
        }
    }
}