using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Helper;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.DataAccessLayer
{
	public class AdminSnPostsDAL : BaseDataAccessLogic
	{
		private readonly IRARIndiaRepository<AdminSnPost> _adminSnPostsRepository;
		private readonly IRARIndiaRepository<AdminRoleMaster> _adminRoleMasterRepository;
		private readonly IRARIndiaRepository<AdminRoleCentreRight> _adminRoleCentreRightsRepository;

		public AdminSnPostsDAL()
		{
			_adminSnPostsRepository = new RARIndiaRepository<AdminSnPost>();
			_adminRoleMasterRepository = new RARIndiaRepository<AdminRoleMaster>();
			_adminRoleCentreRightsRepository = new RARIndiaRepository<AdminRoleCentreRight>();
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

			EmployeeDesignationMaster employeeDesignationMaster = GetDesignationDetails(adminSnPostsModel.DesignationID);
			GeneralDepartmentMaster generalDepartmentMaster = GetDepartmentDetails(adminSnPostsModel.DepartmentID);

			adminSnPostsModel.NomenAdminRoleCode = $"{employeeDesignationMaster.ShortCode}-{generalDepartmentMaster.DeptShortCode}-{adminSnPostsModel.CentreCode}";
			adminSnPostsModel.SactionedPostDescription = $"{employeeDesignationMaster.Description}-{generalDepartmentMaster.DepartmentName}-{adminSnPostsModel.PostType}-{adminSnPostsModel.DesignationType}";
			AdminSnPost adminSnPostEntity = adminSnPostsModel.FromModelToEntity<AdminSnPost>();
			//Create new adminSnPosts and return it.
			AdminSnPost adminSnPostsData = _adminSnPostsRepository.Insert(adminSnPostEntity);
			if (adminSnPostsData?.ID > 0)
			{
				adminSnPostsModel.AdminSnPostsId = adminSnPostsData.ID;
				AdminRoleMaster adminRoleMaster = new AdminRoleMaster()
				{
					AdminSnPostID = adminSnPostsModel.AdminSnPostsId,
					SanctPostName = adminSnPostsModel.SactionedPostDescription,
					MonitoringLevel = RARIndiaConstant.Self,
					AdminRoleCode = adminSnPostsModel.NomenAdminRoleCode,
					OthCentreLevel = string.Empty,
					IsActive = true,
					CreatedBy = adminSnPostsModel.CreatedBy
				};
				//Create new adminRoleMaster
				adminRoleMaster = _adminRoleMasterRepository.Insert(adminRoleMaster);

				AdminRoleCentreRight adminRoleCentreRight = new AdminRoleCentreRight()
				{
					AdminRoleMasterID = adminRoleMaster.ID,
					CentreCode = adminSnPostsModel.CentreCode,
					AdminRoleRightTypeID = 0,
					IsActive = true,
					CreatedBy = adminSnPostsModel.CreatedBy
				};

				//Create new adminRoleCentreRight
				adminRoleCentreRight = _adminRoleCentreRightsRepository.Insert(adminRoleCentreRight);
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
			if (IsNotNull(adminSnPostsModel))
			{
				adminSnPostsModel.CentreName = GetOrganisationCentreDetails(adminSnPostsModel.CentreCode)?.CentreName;
				adminSnPostsModel.DepartmentName = GetDepartmentDetails(adminSnPostsModel.DepartmentID)?.DepartmentName;
				adminSnPostsModel.DesignationName = GetDesignationDetails(adminSnPostsModel.DesignationID)?.Description;
			}
			return adminSnPostsModel;
		}

		//Update adminSnPosts.
		public AdminSnPostsModel UpdateAdminSnPosts(AdminSnPostsModel adminSnPostsModel)
		{
			bool isAdminSnPostsUpdated = false;
			if (RARIndiaHelperUtility.IsNull(adminSnPostsModel))
				throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

			if (adminSnPostsModel.AdminSnPostsId < 1)
				throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminSnPostsID"));

			AdminSnPost adminSnPostsData = _adminSnPostsRepository.Table.FirstOrDefault(x => x.ID == adminSnPostsModel.AdminSnPostsId);
			adminSnPostsData.NoOfPosts = adminSnPostsModel.NoOfPosts;
			adminSnPostsData.IsActive = adminSnPostsModel.IsActive;
			adminSnPostsData.ModifiedBy = adminSnPostsModel.ModifiedBy;


			//Update adminSnPosts
			isAdminSnPostsUpdated = _adminSnPostsRepository.Update(adminSnPostsData);
			if (!isAdminSnPostsUpdated)
			{
				adminSnPostsModel.HasError = true;
				adminSnPostsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
			}
			return adminSnPostsModel;
		}
		#region Private Method

		//Check if adminSnPosts code is already present or not.
		private bool IsCodeAlreadyExist(AdminSnPostsModel adminSnPostsModel)
		 => _adminSnPostsRepository.Table.Any(x => x.CentreCode == adminSnPostsModel.CentreCode && x.DepartmentID == adminSnPostsModel.DepartmentID && x.DesignationID == adminSnPostsModel.DesignationID);
		#endregion
	}
}
