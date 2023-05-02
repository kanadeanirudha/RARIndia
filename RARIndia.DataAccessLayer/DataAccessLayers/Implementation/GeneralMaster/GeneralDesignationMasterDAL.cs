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
    public class GeneralDesignationMasterDAL
    {
        private readonly IRARIndiaRepository<EmployeeDesignationMaster> _generalDesignationMasterRepository;
        public GeneralDesignationMasterDAL()
        {
            _generalDesignationMasterRepository = new RARIndiaRepository<EmployeeDesignationMaster>();
        }

        public GeneralDesignationListModel GetDesignationList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<GeneralDesignationModel> objStoredProc = new RARIndiaViewRepository<GeneralDesignationModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralDesignationModel> DesignationList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetDesignationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralDesignationListModel listModel = new GeneralDesignationListModel();

            listModel.GeneralDesignationList = DesignationList?.Count > 0 ? DesignationList : new List<GeneralDesignationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Designation.
        public GeneralDesignationModel CreateDesignation(GeneralDesignationModel generalDesignationModel)
        {
            if (RARIndiaHelperUtility.IsNull(generalDesignationModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCodeAlreadyExist(generalDesignationModel.Description))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Designation name"));
            }
            EmployeeDesignationMaster a = generalDesignationModel.FromModelToEntity<EmployeeDesignationMaster>();
            //Create new Designation and return it.
            EmployeeDesignationMaster DesignationData = _generalDesignationMasterRepository.Insert(a);
            if (DesignationData?.EmployeeDesignationMasterId > 0)
            {
                generalDesignationModel.DesignationId = DesignationData.EmployeeDesignationMasterId;
            }
            else
            {
                generalDesignationModel.HasError = true;
                generalDesignationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDesignationModel;
        }

        //Get Designation by Designation id.
        public GeneralDesignationModel GetDesignation(int DesignationId)
        {
            if (DesignationId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationID"));

            //Get the Designation Details based on id.
            EmployeeDesignationMaster designationData = _generalDesignationMasterRepository.Table.FirstOrDefault(x => x.EmployeeDesignationMasterId == DesignationId);
            GeneralDesignationModel GeneralDesignationModel = designationData.FromEntityToModel<GeneralDesignationModel>();
            return GeneralDesignationModel;
        }

        //Update Designation.
        public GeneralDesignationModel UpdateDesignation(GeneralDesignationModel generalDesignationModel)
        {
            if (RARIndiaHelperUtility.IsNull(generalDesignationModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalDesignationModel.DesignationId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationID"));

            //Update Designation
            bool isDesignationUpdated = _generalDesignationMasterRepository.Update(generalDesignationModel.FromModelToEntity<EmployeeDesignationMaster>());
            if (!isDesignationUpdated)
            {
                generalDesignationModel.HasError = true;
                generalDesignationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDesignationModel;
        }

        //Delete Designation.
        public bool DeleteDesignation(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DesignationID"));

            RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
            objStoredProc.SetParameter("DesignationId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteDesignation @DesignationId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }


        //Get Designation list.
        public GeneralDesignationListModel GetDesignations()
        {
            GeneralDesignationListModel list = new GeneralDesignationListModel();
            list.GeneralDesignationList = (from a in _generalDesignationMasterRepository.Table
                                          where a.IsActive == true
                                          orderby a.Description ascending
                                          select new GeneralDesignationModel()
                                          {
                                              DesignationId = a.EmployeeDesignationMasterId,
                                              Description = a.Description,
                                              ShortCode = a.ShortCode,
                                          })?.ToList();
            return list;
        }
        #region Private Method

        //Check if Designation code is already present or not.
        private bool IsCodeAlreadyExist(string DesignationName)
         => _generalDesignationMasterRepository.Table.Any(x => x.Description == DesignationName);
        #endregion
    }
}
