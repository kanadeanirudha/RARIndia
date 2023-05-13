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
    public class GeneralDepartmentMasterDAL
    {
        private readonly IRARIndiaRepository<GeneralDepartmentMaster> _generalDepartmentMasterRepository;
        public GeneralDepartmentMasterDAL()
        {
            _generalDepartmentMasterRepository = new RARIndiaRepository<GeneralDepartmentMaster>();
        }

        public GeneralDepartmentListModel GetDepartmentList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<GeneralDepartmentModel> objStoredProc = new RARIndiaViewRepository<GeneralDepartmentModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralDepartmentModel> DepartmentList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetDepartmentList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralDepartmentListModel listModel = new GeneralDepartmentListModel();

            listModel.GeneralDepartmentList = DepartmentList?.Count > 0 ? DepartmentList : new List<GeneralDepartmentModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Department.
        public GeneralDepartmentModel CreateDepartment(GeneralDepartmentModel generalDepartmentModel)
        {
            if (IsNull(generalDepartmentModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCodeAlreadyExist(generalDepartmentModel.DepartmentName))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Department name"));
            }
            GeneralDepartmentMaster a = generalDepartmentModel.FromModelToEntity<GeneralDepartmentMaster>();
            //Create new Department and return it.
            GeneralDepartmentMaster departmentData = _generalDepartmentMasterRepository.Insert(a);
            if (departmentData?.GeneralDepartmentMasterId > 0)
            {
                generalDepartmentModel.GeneralDepartmentMasterId = departmentData.GeneralDepartmentMasterId;
            }
            else
            {
                generalDepartmentModel.HasError = true;
                generalDepartmentModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDepartmentModel;
        }

        //Get Department by Department id.
        public GeneralDepartmentModel GetDepartment(int DepartmentId)
        {
            if (DepartmentId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DepartmentID"));

            //Get the Department Details based on id.
            GeneralDepartmentMaster DepartmentData = _generalDepartmentMasterRepository.Table.FirstOrDefault(x => x.GeneralDepartmentMasterId == DepartmentId);
            GeneralDepartmentModel GeneralDepartmentModel = DepartmentData.FromEntityToModel<GeneralDepartmentModel>();
            return GeneralDepartmentModel;
        }

        //Update Department.
        public GeneralDepartmentModel UpdateDepartment(GeneralDepartmentModel generalDepartmentModel)
        {
            if (IsNull(generalDepartmentModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalDepartmentModel.GeneralDepartmentMasterId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DepartmentID"));

            //Update Department
            bool isDepartmentUpdated = _generalDepartmentMasterRepository.Update(generalDepartmentModel.FromModelToEntity<GeneralDepartmentMaster>());
            if (!isDepartmentUpdated)
            {
                generalDepartmentModel.HasError = true;
                generalDepartmentModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalDepartmentModel;
        }

        //Delete Department.
        public bool DeleteDepartment(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DepartmentID"));

            RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
            objStoredProc.SetParameter("DepartmentId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteDepartment @DepartmentId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }


        //Get department list.
        public GeneralDepartmentListModel GetDepartmentsByCentreCode(string centreCode)
        {
            GeneralDepartmentListModel list = new GeneralDepartmentListModel();
            list.GeneralDepartmentList = (from a in _generalDepartmentMasterRepository.Table
                                          join b in new RARIndiaRepository<OrganisationCentrewiseDepartment>().Table
                                          on a.GeneralDepartmentMasterId equals b.DepartmentId
                                          where (b.CentreCode == centreCode || centreCode == null) 
                                          select new GeneralDepartmentModel()
                                          {
                                              GeneralDepartmentMasterId = a.GeneralDepartmentMasterId,
                                              DepartmentName = a.DepartmentName,
                                              DepartmentShortCode = a.DepartmentShortCode,
                                              PrintShortDesc = a.PrintShortDesc
                                          })?.ToList();
            return list;
        }
        #region Private Method

        //Check if Department code is already present or not.
        private bool IsCodeAlreadyExist(string departmentName)
         => _generalDepartmentMasterRepository.Table.Any(x => x.DepartmentName == departmentName);
        #endregion
    }
}
