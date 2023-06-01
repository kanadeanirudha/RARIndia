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
    public class GeneralTaxMasterDAL
    {
        private readonly IRARIndiaRepository<GeneralTaxMaster> _generalTaxMasterRepository;
        public GeneralTaxMasterDAL()
        {
            _generalTaxMasterRepository = new RARIndiaRepository<GeneralTaxMaster>();
        }

        public GeneralTaxMasterListModel GetTaxMasterList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<GeneralTaxMasterModel> objStoredProc = new RARIndiaViewRepository<GeneralTaxMasterModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralTaxMasterModel> TaxMasterList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetTaxList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralTaxMasterListModel listModel = new GeneralTaxMasterListModel();
            listModel.GeneralTaxMasterList = TaxMasterList?.Count > 0 ? TaxMasterList : new List<GeneralTaxMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Tax Master.
        public GeneralTaxMasterModel CreateTaxMaster(GeneralTaxMasterModel generalTaxMasterModel)
        {
            if (IsNull(generalTaxMasterModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsCodeAlreadyExist(generalTaxMasterModel.TaxName))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Tax Name"));
            }
            GeneralTaxMaster generalTaxMaster = generalTaxMasterModel.FromModelToEntity<GeneralTaxMaster>();

            //Create new Tax Master and return it.
            GeneralTaxMaster taxMasterData = _generalTaxMasterRepository.Insert(generalTaxMaster);
            if (taxMasterData?.GeneralTaxMasterId > 0)
            {
                generalTaxMasterModel.GeneralTaxMasterId = taxMasterData.GeneralTaxMasterId;
            }
            else
            {
                generalTaxMasterModel.HasError = true;
                generalTaxMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalTaxMasterModel;
        }

        //Get Tax Master by GeneralTaxMasterId.
        public GeneralTaxMasterModel GetTaxMaster(int TaxMasterId)
        {
            if (TaxMasterId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxMasterId"));

            //Get the Tax Master Details based on id.
            GeneralTaxMaster TaxMasterData = _generalTaxMasterRepository.Table.FirstOrDefault(x => x.GeneralTaxMasterId == TaxMasterId);
            GeneralTaxMasterModel generalTaxMasterModel = TaxMasterData.FromEntityToModel<GeneralTaxMasterModel>();
            return generalTaxMasterModel;
        }

        //Update Tax Master.
        public GeneralTaxMasterModel UpdateTaxMaster(GeneralTaxMasterModel generalTaxMasterModel)
        {
            if (IsNull(generalTaxMasterModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalTaxMasterModel.GeneralTaxMasterId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxMasterId"));
            bool isTaxMasterUpdated = _generalTaxMasterRepository.Update(generalTaxMasterModel.FromModelToEntity<GeneralTaxMaster>());
            if (!isTaxMasterUpdated)
            {
                generalTaxMasterModel.HasError = true;
                generalTaxMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return generalTaxMasterModel;
        }

        //Delete Tax Master.
        public bool DeleteTaxMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralTaxMasterId"));

            RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
            objStoredProc.SetParameter(RARIndiaTaxMasterEnum.TaxMasterId.ToString(), parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteTaxMaster @GeneralTaxMasterId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Private Method
        //Check if Tax Name is already present or not.
        private bool IsCodeAlreadyExist(string taxName)
         => _generalTaxMasterRepository.Table.Any(x => x.TaxName == taxName);
        #endregion
    }
}
