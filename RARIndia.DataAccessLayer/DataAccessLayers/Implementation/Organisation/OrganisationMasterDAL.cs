using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Helper;

using System.Linq;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.DataAccessLayer
{
    public class OrganisationMasterDAL : BaseDataAccessLogic
    {
        private readonly IRARIndiaRepository<OrganisationMaster> _organisationMasterRepository;
        public OrganisationMasterDAL()
        {
            _organisationMasterRepository = new RARIndiaRepository<OrganisationMaster>();
        }

        //Get Organisation Details
        public OrganisationMasterModel GetOrganisationDetails()
        {
            OrganisationMaster organisationMasterData = _organisationMasterRepository.Table.FirstOrDefault();
            OrganisationMasterModel model = IsNull(organisationMasterData) ? new OrganisationMasterModel() : organisationMasterData.FromEntityToModel<OrganisationMasterModel>();
            return model;
        }

        //Update OrganisationMaster.
        public OrganisationMasterModel UpdateOrganisation(OrganisationMasterModel organisationMasterModel)
        {
            bool isOrganisationMasterUpdated = false;
            if (IsNull(organisationMasterModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationMasterModel.OrganisationMasterId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "OrganisationMasterID"));

            //Update OrganisationMaster
            isOrganisationMasterUpdated = _organisationMasterRepository.Update(organisationMasterModel.FromModelToEntity<OrganisationMaster>());
            if (!isOrganisationMasterUpdated)
            {
                organisationMasterModel.HasError = true;
                organisationMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return organisationMasterModel;
        }
    }
}
