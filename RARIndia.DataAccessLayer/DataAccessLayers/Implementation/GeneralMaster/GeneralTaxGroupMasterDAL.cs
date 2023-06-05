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
    public class GeneralTaxGroupMasterDAL
    {
        private readonly IRARIndiaRepository<GeneralTaxMaster> _generalTaxMasterRepository;
        private readonly IRARIndiaRepository<GeneralTaxGroupMaster> _generalTaxGroupMasterRepository;
        public GeneralTaxGroupMasterDAL()
        {
            _generalTaxGroupMasterRepository = new RARIndiaRepository<GeneralTaxGroupMaster>();
            _generalTaxMasterRepository = new RARIndiaRepository<GeneralTaxMaster>();
        }

        public GeneralTaxGroupMasterListModel GetTaxGroupMasterList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<GeneralTaxGroupMasterModel> objStoredProc = new RARIndiaViewRepository<GeneralTaxGroupMasterModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralTaxGroupMasterModel> taxGroupMasterList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetTaxGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralTaxGroupMasterListModel listModel = new GeneralTaxGroupMasterListModel();
            listModel.GeneralTaxGroupMasterList = taxGroupMasterList?.Count > 0 ? taxGroupMasterList : new List<GeneralTaxGroupMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Tax Group Master.
        public GeneralTaxGroupMasterModel CreateTaxGroupMaster(GeneralTaxGroupMasterModel generalTaxGroupMasterModel)
        {
            if (IsNull(generalTaxGroupMasterModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsCodeAlreadyExist(generalTaxGroupMasterModel.TaxGroupName))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Tax Group Name"));
            }
           
           //_generalTaxMasterRepository.Table.Where(x => generalTaxGroupMasterModel.GeneralTaxMasterIds.Contains(x.GeneralTaxMasterId)).ToList().Sum
            GeneralTaxGroupMaster generalTaxGroupMaster = generalTaxGroupMasterModel.FromModelToEntity<GeneralTaxGroupMaster>();

            //Create new Tax Group Master and return it.
            GeneralTaxGroupMaster taxGroupMasterData = _generalTaxGroupMasterRepository.Insert(generalTaxGroupMaster);
            if (taxGroupMasterData?.GeneralTaxGroupMasterId > 0)
            {
                generalTaxGroupMasterModel.GeneralTaxGroupMasterId = taxGroupMasterData.GeneralTaxGroupMasterId;
            }
            else
            {
                generalTaxGroupMasterModel.HasError = true;
                generalTaxGroupMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalTaxGroupMasterModel;
        }

        //Get Tax Group Master by GeneralTaxGroupMasterId.
        public GeneralTaxGroupMasterModel GetTaxGroupMaster(int taxGroupMasterId)
        {
            if (taxGroupMasterId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxGroupMasterId"));

            //Get the Tax Master Details based on id.
            GeneralTaxGroupMaster taxGroupMasterData = _generalTaxGroupMasterRepository.Table.FirstOrDefault(x => x.GeneralTaxGroupMasterId == taxGroupMasterId);
            GeneralTaxGroupMasterModel generalTaxGroupMasterModel = taxGroupMasterData.FromEntityToModel<GeneralTaxGroupMasterModel>();
            return generalTaxGroupMasterModel;
        }

        //Update Tax Group Master.
        public GeneralTaxGroupMasterModel UpdateTaxGroupMaster(GeneralTaxGroupMasterModel generalTaxGroupMasterModel)
        {
            if (IsNull(generalTaxGroupMasterModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalTaxGroupMasterModel.GeneralTaxGroupMasterId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxGroupMasterId"));
            bool isTaxGroupMasterUpdated = _generalTaxGroupMasterRepository.Update(generalTaxGroupMasterModel.FromModelToEntity<GeneralTaxGroupMaster>());
            if (!isTaxGroupMasterUpdated)
            {
                generalTaxGroupMasterModel.HasError = true;
                generalTaxGroupMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return generalTaxGroupMasterModel;
        }

        //Delete Tax Group Master.
        public bool DeleteTaxGroupMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralTaxGroupMasterId"));

            RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
            objStoredProc.SetParameter(RARIndiaTaxGroupMasterEnum.TaxGroupMasterId.ToString(), parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteTaxGroupMaster @GeneralTaxGroupMasterId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Private Method
        //Check if Tax Group Name is already present or not.
        private bool IsCodeAlreadyExist(string taxGroupName)
         => _generalTaxGroupMasterRepository.Table.Any(x => x.TaxGroupName == taxGroupName);
        #endregion
    }
}
