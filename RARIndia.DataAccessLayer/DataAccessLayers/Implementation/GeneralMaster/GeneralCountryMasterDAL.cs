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
    public class GeneralCountryMasterDAL
    {
        private readonly IRARIndiaRepository<GeneralCountryMaster> _generalCountryMasterRepository;
        public GeneralCountryMasterDAL()
        {
            _generalCountryMasterRepository = new RARIndiaRepository<GeneralCountryMaster>();
        }

        public GeneralCountryListModel GetCountryList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
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

        //Create country.
        public GeneralCountryModel CreateCountry(GeneralCountryModel generalCountryModel)
        {
            if (IsNull(generalCountryModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCodeAlreadyExist(generalCountryModel.CountryCode))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Country code"));
            }
            GeneralCountryMaster a = generalCountryModel.FromModelToEntity<GeneralCountryMaster>();
            //Create new country and return it.
            GeneralCountryMaster countryData = _generalCountryMasterRepository.Insert(a);
            if (countryData?.GeneralCountryMasterId > 0)
            {
                generalCountryModel.GeneralCountryMasterId = countryData.GeneralCountryMasterId;
            }
            else
            {
                generalCountryModel.HasError = true;
                generalCountryModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalCountryModel;
        }

        //Get country by country id.
        public GeneralCountryModel GetCountry(int countryId)
        {
            if (countryId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CountryID"));

            //Get the country Details based on id.
            GeneralCountryMaster countryData = _generalCountryMasterRepository.Table.FirstOrDefault(x => x.GeneralCountryMasterId == countryId);
            GeneralCountryModel generalCountryModel = countryData.FromEntityToModel<GeneralCountryModel>();
            return generalCountryModel;
        }

        //Update country.
        public GeneralCountryModel UpdateCountry(GeneralCountryModel generalCountryModel)
        {
            bool isCountryUpdated = false;
            if (IsNull(generalCountryModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalCountryModel.GeneralCountryMasterId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CountryID"));

            //Update country
            isCountryUpdated = _generalCountryMasterRepository.Update(generalCountryModel.FromModelToEntity<GeneralCountryMaster>());
            if (!isCountryUpdated)
            {
                generalCountryModel.HasError = true;
                generalCountryModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalCountryModel;
        }

        //Delete country.
        public bool DeleteCountry(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CountryID"));

            RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
            objStoredProc.SetParameter(RARIndiaCountryEnum.CountryId.ToString(), parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteCountry @CountryId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Private Method

        //Check if country code is already present or not.
        private bool IsCodeAlreadyExist(string countryCode)
         => _generalCountryMasterRepository.Table.Any(x => x.CountryCode == countryCode);
        #endregion
    }
}
