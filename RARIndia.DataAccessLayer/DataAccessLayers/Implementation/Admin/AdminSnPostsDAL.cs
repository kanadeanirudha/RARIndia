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
    public class AdminSnPostsDAL
    {
        private readonly IRARIndiaRepository<AdminSnPost> _adminSnPostsRepository;
        public AdminSnPostsDAL()
        {
            _adminSnPostsRepository = new RARIndiaRepository<AdminSnPost>();
        }

        public AdminSnPostsListModel GetAdminSnPostsList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength, string centreCode, int departmentId)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<AdminSnPostsModel> objStoredProc = new RARIndiaViewRepository<AdminSnPostsModel>();
            objStoredProc.SetParameter("@CentreCode", centreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@DepartmentId", departmentId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AdminSnPostsModel> adminSnPostsList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetAdminSnPostsList @CentreCode,@DepartmentId, @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
            AdminSnPostsListModel listModel = new AdminSnPostsListModel();

            listModel.AdminSnPostsList = adminSnPostsList?.Count > 0 ? adminSnPostsList : new List<AdminSnPostsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create adminSnPosts.
        public AdminSnPostsModel CreateAdminSnPosts(AdminSnPostsModel adminSnPostsModel)
        {
            if (RARIndiaHelperUtility.IsNull(adminSnPostsModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCodeAlreadyExist(adminSnPostsModel))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "AdminSnPosts code"));
            }

            EmployeeDesignationMaster employeeDesignationMaster = new RARIndiaRepository<EmployeeDesignationMaster>().GetById(adminSnPostsModel.DesignationID);
            GeneralDepartmentMaster generalDepartmentMaster = new RARIndiaRepository<GeneralDepartmentMaster>().GetById(adminSnPostsModel.DepartmentID);

            adminSnPostsModel.NomenAdminRoleCode = $"{employeeDesignationMaster.ShortCode}-{generalDepartmentMaster.DeptShortCode}-{adminSnPostsModel.CentreCode}";
            adminSnPostsModel.SactionedPostDescription = $"{employeeDesignationMaster.Description}-{generalDepartmentMaster.DepartmentName}-{adminSnPostsModel.PostType}-{adminSnPostsModel.DesignationType}";
            AdminSnPost adminSnPostEntity = adminSnPostsModel.FromModelToEntity<AdminSnPost>();
            //Create new adminSnPosts and return it.
            AdminSnPost adminSnPostsData = _adminSnPostsRepository.Insert(adminSnPostEntity);
            if (adminSnPostsData?.ID > 0)
            {
                adminSnPostsModel.AdminSnPostsId = adminSnPostsData.ID;
            }
            else
            {
                adminSnPostsModel.HasError = true;
                adminSnPostsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return adminSnPostsModel;
        }

        //Get adminSnPosts by adminSnPosts id.
        public AdminSnPostsModel GetAdminSnPosts(int adminSnPostsId)
        {
            if (adminSnPostsId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminSnPostsID"));

            //Get the adminSnPosts Details based on id.
            AdminSnPost adminSnPostsData = _adminSnPostsRepository.Table.FirstOrDefault(x => x.ID == adminSnPostsId);
            AdminSnPostsModel adminSnPostsModel = adminSnPostsData.FromEntityToModel<AdminSnPostsModel>();
            return adminSnPostsModel;
        }

        ////Update adminSnPosts.
        //public AdminSnPostsModel UpdateAdminSnPosts(AdminSnPostsModel adminSnPostsModel)
        //{
        //    bool isAdminSnPostsUpdated = false;
        //    if (RARIndiaHelperUtility.IsNull(adminSnPostsModel))
        //        throw new RARIndiaException(ErrorCodes.InvalidData, Resources.ModelNotNull);

        //    if (adminSnPostsModel.AdminSnPostsId < 1)
        //        throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(Resources.ErrorIdLessThanOne, "AdminSnPostsID"));

        //    //Update adminSnPosts
        //    isAdminSnPostsUpdated = _adminSnPostsRepository.Update(adminSnPostsModel.FromModelToEntity<AdminSnPosts>());
        //    if (!isAdminSnPostsUpdated)
        //    {
        //        adminSnPostsModel.HasError = true;
        //        adminSnPostsModel.ErrorMessage = Resources.ErrorFailedToCreate;
        //    }
        //    return adminSnPostsModel;
        //}

        ////Delete adminSnPosts.
        //public bool DeleteAdminSnPosts(ParameterModel parameterModel)
        //{
        //    if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
        //        throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(Resources.ErrorIdLessThanOne, "AdminSnPostsID"));

        //    RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
        //    objStoredProc.SetParameter("AdminSnPostsId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
        //    objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
        //    int status = 0;
        //    objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteAdminSnPosts @AdminSnPostsId,  @Status OUT", 1, out status);

        //    return status == 1 ? true : false;
        //}

        #region Private Method

        //Check if adminSnPosts code is already present or not.
        private bool IsCodeAlreadyExist(AdminSnPostsModel adminSnPostsModel)
         => _adminSnPostsRepository.Table.Any(x => x.CentreCode == adminSnPostsModel.CentreCode && x.DepartmentID == adminSnPostsModel.DepartmentID && x.DesignationID == adminSnPostsModel.DesignationID);
        #endregion
    }
}
