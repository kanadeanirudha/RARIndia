using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.Model;
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

    }
}
