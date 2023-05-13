using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Helper;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.DataAccessLayer
{
	public class AdminRoleMasterDAL : BaseDataAccessLogic
	{
		private readonly IRARIndiaRepository<AdminRoleMaster> _adminRoleMasterRepository;
		private readonly IRARIndiaRepository<AdminRoleCentreRight> _adminRoleCentreRightsRepository;
		private readonly IRARIndiaRepository<AdminSactionPost> _adminSnPostsRepository;
		public AdminRoleMasterDAL()
		{
			_adminRoleMasterRepository = new RARIndiaRepository<AdminRoleMaster>();
			_adminRoleCentreRightsRepository = new RARIndiaRepository<AdminRoleCentreRight>();
			_adminSnPostsRepository = new RARIndiaRepository<AdminSactionPost>();
		}

		public AdminRoleMasterListModel GetAdminRoleMasterList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength, string centreCode, int departmentId)
		{
			//Bind the Filter, sorts & Paging details.
			PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
			RARIndiaViewRepository<AdminRoleMasterModel> objStoredProc = new RARIndiaViewRepository<AdminRoleMasterModel>();
			objStoredProc.SetParameter("@CentreCode", centreCode, ParameterDirection.Input, DbType.String);
			objStoredProc.SetParameter("@DepartmentId", departmentId, ParameterDirection.Input, DbType.Int16);
			objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
			objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
			objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
			objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
			objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
			List<AdminRoleMasterModel> adminRoleMasterList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetAdminRoleList @CentreCode,@DepartmentId, @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();
			AdminRoleMasterListModel listModel = new AdminRoleMasterListModel();

			listModel.AdminRoleMasterList = adminRoleMasterList?.Count > 0 ? adminRoleMasterList : new List<AdminRoleMasterModel>();
			listModel.BindPageListModel(pageListModel);
			return listModel;
		}

		//Get adminRoleMaster by adminRoleMaster id.
		public AdminRoleMasterModel GetAdminRoleMasterDetailsById(int adminRoleMasterId)
		{
			if (adminRoleMasterId <= 0)
				throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterID"));

			//Get the adminRoleMaster Details based on id.
			AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterId);
			AdminRoleMasterModel adminRoleMasterModel = adminRoleMasterData.FromEntityToModel<AdminRoleMasterModel>();
			adminRoleMasterModel.SelectedRoleWiseCentres = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterId && x.IsActive == true)?.Select(y => y.CentreCode)?.Distinct().ToList();
			adminRoleMasterModel.SelectedCentreCodeForSelf = _adminSnPostsRepository.GetById(adminRoleMasterData.AdminSactionPostId).CentreCode;
			adminRoleMasterModel.AllCentreList = OrganisationCentreList();
			return adminRoleMasterModel;
		}

		//Update adminRoleMaster.
		public AdminRoleMasterModel UpdateAdminRoleMaster(AdminRoleMasterModel adminRoleMasterModel)
		{
			bool isAdminRoleMasterUpdated = false;
			if (IsNull(adminRoleMasterModel))
				throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

			if (adminRoleMasterModel.AdminRoleMasterId < 1)
				throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AdminRoleMasterID"));

			AdminRoleMaster adminRoleMasterData = _adminRoleMasterRepository.Table.FirstOrDefault(x => x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId);
			adminRoleMasterData.MonitoringLevel = adminRoleMasterModel.MonitoringLevel;
			adminRoleMasterData.OthCentreLevel = adminRoleMasterModel.MonitoringLevel == RARIndiaConstant.Self ? string.Empty : "Selected";
			adminRoleMasterData.IsLoginAllowFromOutside = adminRoleMasterModel.IsLoginAllowFromOutside;
			adminRoleMasterData.IsAttendaceAllowFromOutside = adminRoleMasterModel.IsAttendaceAllowFromOutside;
			adminRoleMasterData.IsActive = adminRoleMasterModel.IsActive;
			adminRoleMasterData.ModifiedBy = adminRoleMasterModel.ModifiedBy;
			adminRoleMasterData.ModifiedDate = DateTime.Now;

			//Update adminRoleMaster
			isAdminRoleMasterUpdated = _adminRoleMasterRepository.Update(adminRoleMasterData);
			if (!isAdminRoleMasterUpdated)
			{
				adminRoleMasterModel.HasError = true;
				adminRoleMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
			}

			//update  Admin Role Centre Right
			List<AdminRoleCentreRight> adminRoleCentreRightList = _adminRoleCentreRightsRepository.Table.Where(x => x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId && x.CentreCode != adminRoleMasterModel.SelectedCentreCodeForSelf)?.ToList();

			if (adminRoleMasterModel.MonitoringLevel == RARIndiaConstant.Self || (adminRoleMasterModel.MonitoringLevel == RARIndiaConstant.Other && adminRoleMasterModel?.SelectedRoleWiseCentres?.Count == 0))
			{
				adminRoleCentreRightList = adminRoleCentreRightList.Where(x=>x.IsActive == true)?.ToList();
				if (adminRoleCentreRightList?.Count > 0 && adminRoleCentreRightList.Any(x => x.IsActive == true))
				{
					adminRoleCentreRightList.ForEach(x => { x.IsActive = false; x.ModifiedBy = adminRoleMasterModel.ModifiedBy; });
					_adminRoleCentreRightsRepository.BatchUpdate(adminRoleCentreRightList);
				}
			}
			else
			{
				adminRoleMasterModel.AllCentreList = OrganisationCentreList();
				foreach (UserAccessibleCentreModel item in adminRoleMasterModel?.AllCentreList?.Where(x => x.CentreCode != adminRoleMasterModel.SelectedCentreCodeForSelf))
				{
					string selectedCentreCode = adminRoleMasterModel?.SelectedRoleWiseCentres?.FirstOrDefault(x => x == item.CentreCode);
					AdminRoleCentreRight adminRoleCentreRight = adminRoleCentreRightList?.FirstOrDefault(x => x.CentreCode == item.CentreCode && x.AdminRoleMasterId == adminRoleMasterModel.AdminRoleMasterId);

					if (adminRoleCentreRight == null && !string.IsNullOrEmpty(selectedCentreCode))
					{
						adminRoleCentreRight = new AdminRoleCentreRight()
						{
							AdminRoleMasterId = adminRoleMasterModel.AdminRoleMasterId,
							CentreCode = selectedCentreCode,
							IsActive = true,
							CreatedBy = adminRoleMasterModel.CreatedBy,
							ModifiedBy = adminRoleMasterModel.ModifiedBy
						};
						_adminRoleCentreRightsRepository.Insert(adminRoleCentreRight);
					}
					else if (adminRoleCentreRight?.CentreCode == selectedCentreCode && adminRoleCentreRight?.IsActive == false)
					{
						adminRoleCentreRight.IsActive = true;
						adminRoleCentreRight.ModifiedBy = adminRoleMasterModel.ModifiedBy;
						_adminRoleCentreRightsRepository.Update(adminRoleCentreRight);
					}
					else if (selectedCentreCode == null && adminRoleCentreRight?.IsActive == true)
					{
						adminRoleCentreRight.IsActive = false;
						adminRoleCentreRight.ModifiedBy = adminRoleMasterModel.ModifiedBy;
						_adminRoleCentreRightsRepository.Update(adminRoleCentreRight);
					}
				}
			}

			return adminRoleMasterModel;
		}
	}
}
