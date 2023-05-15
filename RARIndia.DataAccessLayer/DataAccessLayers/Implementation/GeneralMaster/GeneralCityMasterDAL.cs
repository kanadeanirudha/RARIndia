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
    public class GeneralCityMasterDAL
    {
        private readonly IRARIndiaRepository<GeneralCityMaster> _generalCityMasterRepository;
        public GeneralCityMasterDAL()
        {
            _generalCityMasterRepository = new RARIndiaRepository<GeneralCityMaster>();
        }

        public GeneralCityListModel GetCityList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<GeneralCityModel> objStoredProc = new RARIndiaViewRepository<GeneralCityModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralCityModel> cityList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetCityList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralCityListModel listModel = new GeneralCityListModel();

            listModel.GeneralCityList = cityList?.Count > 0 ? cityList : new List<GeneralCityModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create City.
        public GeneralCityModel CreateCity(GeneralCityModel generalCityModel)
        {
            if (IsNull(generalCityModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCodeAlreadyExist(generalCityModel))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "City code"));
            }
            GeneralCityMaster a = generalCityModel.FromModelToEntity<GeneralCityMaster>();
            //Create new City and return it.
            GeneralCityMaster cityData = _generalCityMasterRepository.Insert(a);
            if (cityData?.GeneralCityMasterId > 0)
            {
                generalCityModel.GeneralCityMasterId = cityData.GeneralCityMasterId;
            }
            else
            {
                generalCityModel.HasError = true;
                generalCityModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalCityModel;
        }

        //Get City by City id.
        public GeneralCityModel GetCity(int cityId)
        {
            if (cityId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CityID"));

            //Get the City Details based on id.
            GeneralCityMaster cityData = _generalCityMasterRepository.Table.FirstOrDefault(x => x.GeneralCityMasterId == cityId);
            GeneralCityModel generalCityModel = cityData.FromEntityToModel<GeneralCityModel>();
            return generalCityModel;
        }

        //Update City.
        public GeneralCityModel UpdateCity(GeneralCityModel generalCityModel)
        {
            bool isCityUpdated = false;
            if (IsNull(generalCityModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalCityModel.GeneralCityMasterId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CityID"));

            //Update City
            isCityUpdated = _generalCityMasterRepository.Update(generalCityModel.FromModelToEntity<GeneralCityMaster>());
            if (!isCityUpdated)
            {
                generalCityModel.HasError = true;
                generalCityModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return generalCityModel;
        }

        //Delete City.
        public bool DeleteCity(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CityID"));

            RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
            objStoredProc.SetParameter(RARIndiaCityEnum.CityId.ToString(), parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteCity @CityId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Private Method

        //Check if City code is already present or not.
        private bool IsCodeAlreadyExist(GeneralCityModel generalCityModel)
         => _generalCityMasterRepository.Table.Any(x => x.CityName == generalCityModel.CityName);
        #endregion
    }
}
