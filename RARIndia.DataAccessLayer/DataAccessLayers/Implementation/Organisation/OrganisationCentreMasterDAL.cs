using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Helper;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Helper;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;

namespace RARIndia.DataAccessLayer
{
    public class OrganisationCentreMasterDAL
    {
        private readonly IRARIndiaRepository<GeneralCountryMaster> _generalCountryMasterRepository;

        public OrganisationCentreMasterDAL()
        {
            _generalCountryMasterRepository = new RARIndiaRepository<GeneralCountryMaster>();
        }

        public GeneralCountryListModel GetOrganisationCentreList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<GeneralCountryModel> objStoredProc = new RARIndiaViewRepository<GeneralCountryModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralCountryModel> countryList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetCountryList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralCountryListModel listModel = new GeneralCountryListModel();

            listModel.GeneralCountryList = countryList?.Count > 0 ? countryList : new List<GeneralCountryModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Organisation Centre.
        public GeneralCountryModel CreateOrganisationCentre(GeneralCountryModel generalCountryModel)
        {
            if (RARIndiaHelperUtility.IsNull(generalCountryModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCodeAlreadyExist(generalCountryModel.CountryCode))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, GeneralResources.ErrorCountryCodeExists);
            }
            GeneralCountryMaster a = generalCountryModel.FromModelToEntity<GeneralCountryMaster>();
            //Create new country and return it.
            GeneralCountryMaster countryData = _generalCountryMasterRepository.Insert(a);
            if (countryData?.ID > 0)
            {
                generalCountryModel.CountryId = countryData.ID;
            }
            else
            {
                generalCountryModel.HasError = true;
                generalCountryModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalCountryModel;
        }

        //Get Organisation Centre by OrganisationCentreid.
        public GeneralCountryModel GetOrganisationCentre(int countryId)
        {
            if (countryId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, GeneralResources.ErrorCountryIdLessThanOne);

            //Get the country Details based on id.
            GeneralCountryMaster countryData = _generalCountryMasterRepository.Table.FirstOrDefault(x => x.ID == countryId);
            GeneralCountryModel generalCountryModel = countryData.FromEntityToModel<GeneralCountryModel>();
            return generalCountryModel;
        }

        public GeneralCountryModel GetOrganisationCentreByRoleId(bool isAdminUser, int roleId, string rightName)
        {
            GeneralCountryMaster countryData = _generalCountryMasterRepository.Table.FirstOrDefault(x => x.ID == roleId);
            GeneralCountryModel generalCountryModel = countryData.FromEntityToModel<GeneralCountryModel>();
            return generalCountryModel;
        }

        //Update Organisation Centre.
        public GeneralCountryModel UpdateOrganisationCentre(GeneralCountryModel generalCountryModel)
        {
            bool isCountryUpdated = false;
            if (RARIndiaHelperUtility.IsNull(generalCountryModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalCountryModel.CountryId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, GeneralResources.IdCanNotBeLessThanOne);

            //Update country
            isCountryUpdated = _generalCountryMasterRepository.Update(generalCountryModel.FromModelToEntity<GeneralCountryMaster>());
            if (!isCountryUpdated)
            {
                generalCountryModel.HasError = true;
                generalCountryModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalCountryModel;
        }

        //Delete Organisation Centre.
        public bool DeleteOrganisationCentre(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, GeneralResources.ErrorCountryIdLessThanOne);

            RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
            objStoredProc.SetParameter(RARIndiaCountryEnum.CountryId.ToString(), parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteCountry @CountryId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }


        #region Private Method

        //Check if Organisation Centre code is already present or not.
        private bool IsCodeAlreadyExist(string countryCode)
         => _generalCountryMasterRepository.Table.Any(x => x.ContryCode == countryCode);
        #endregion
    }
}
