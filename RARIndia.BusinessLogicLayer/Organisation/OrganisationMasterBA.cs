using RARIndia.DataAccessLayer;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

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
    }
}
