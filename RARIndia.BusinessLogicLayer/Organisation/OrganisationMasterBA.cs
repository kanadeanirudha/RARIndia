using RARIndia.DataAccessLayer;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.BusinessLogicLayer
{
    //[SessionTimeoutAttribute]
    public class OrganisationMasterBA : BaseBusinessLogic
    {
        OrganisationMasterDAL _organisationMasterDAL = null;

        public OrganisationMasterBA()
        {
            _organisationMasterDAL = new OrganisationMasterDAL();
        }

        //Get Organisation Details
        public OrganisationMasterViewModel GetOrganisationDetails()
            => _organisationMasterDAL.GetOrganisationDetails().ToViewModel<OrganisationMasterViewModel>();

        //Update Organisation.
        public OrganisationMasterViewModel UpdateOrganisation(OrganisationMasterViewModel organisationMasterViewModel)
        {
            try
            {
                organisationMasterViewModel.ModifiedBy = LoginUserId();
                OrganisationMasterModel organisationMasterModel = _organisationMasterDAL.UpdateOrganisation(organisationMasterViewModel.ToModel<OrganisationMasterModel>());
                return IsNotNull(organisationMasterModel) ? organisationMasterModel.ToViewModel<OrganisationMasterViewModel>() : (OrganisationMasterViewModel)GetViewModelWithErrorMessage(new OrganisationMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.City.ToString());
                return (OrganisationMasterViewModel)GetViewModelWithErrorMessage(organisationMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
    }
}
