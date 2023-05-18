using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Helper;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;

using System.Collections.Generic;
using System.Data;
using System.Linq;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.DataAccessLayer
{
    public class UserMasterDAL : BaseDataAccessLogic
    {
        private readonly IRARIndiaRepository<AdminRoleApplicableDetail> _adminRoleApplicableDetailsRepository;
        private readonly IRARIndiaRepository<UserMaster> _userMasterRepository;
        public UserMasterDAL()
        {
            _adminRoleApplicableDetailsRepository = new RARIndiaRepository<AdminRoleApplicableDetail>();
            _userMasterRepository = new RARIndiaRepository<UserMaster>();
        }

        #region Public Method
        public UserModel Login(UserModel userModel)
        {
            if (IsNull(userModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.UserName == userModel.UserName && x.Password == userModel.Password);

            if (IsNull(userMasterData))
                throw new RARIndiaException(ErrorCodes.NotFound, null);
            else if (userMasterData.IsActive == false)
                throw new RARIndiaException(ErrorCodes.ContactAdministrator, null);

            userModel = userMasterData?.FromEntityToModel<UserModel>();
            userModel.IsAdminUser = IsAdminUser(userModel.UserType);
            //Bind Role
            BindRoleTypes(userModel);

            List<UserModuleMaster> userAllModuleList = new RARIndiaRepository<UserModuleMaster>().Table.Where(x => x.ModuleActiveFlag == true)?.ToList();
            List<UserMainMenuMaster> userAllMenuList = new RARIndiaRepository<UserMainMenuMaster>().Table.Where(x => x.IsEnable == true)?.ToList();
            List<AdminRoleMenuDetail> userRoleMenuList = new List<AdminRoleMenuDetail>();
            if (!userModel.IsAdminUser)
            {
                userRoleMenuList = new RARIndiaRepository<AdminRoleMenuDetail>().Table.Where(x => x.IsActive == true && x.AdminRoleMasterId == userModel.SelectedRoleId)?.ToList();
                if (userRoleMenuList?.Count == 0)
                {
                    throw new RARIndiaException(ErrorCodes.ContactAdministrator, null);
                }
                else
                {
                    //Bind Menu And Modules For Admin User
                    BindMenuAndModulesForNonAdminUser(userModel, userAllModuleList, userAllMenuList, userRoleMenuList);

                    //Bind Balance Sheet
                    userModel.BalanceSheetList = BindAccountBalanceSheetByRoleID(userModel);
                }
            }
            else
            {
                //Bind Menu And Modules For Non Admin User
                BindMenuAndModulesForAdminUser(userModel, userAllModuleList, userAllMenuList);
                userModel.AccessibleCentreList = OrganisationCentreList();
            }
            userModel.SelectedCentreCode = userModel.AccessibleCentreList?.FirstOrDefault().CentreCode;
            return userModel;
        }


        public int GetNotificationCount(int userId)
        {
            int? notificationCount = new RARIndiaRepository<UserNotificationCount>().Table.FirstOrDefault(x => x.UserId == userId)?.NotificationCount;
            return notificationCount != null && notificationCount > 0 ? (int)notificationCount : 0;
        }

        #endregion

        #region Private Method

        //Bind Role Types
        private void BindRoleTypes(UserModel userModel)
        {
            if (!userModel.IsAdminUser)
            {
                List<AdminRoleApplicableDetail> roleList = _adminRoleApplicableDetailsRepository.Table.Where(x => x.EmployeeId == userModel.UserMasterId && x.IsActive)?.ToList();
                if (roleList?.Count() == 0)
                {
                    throw new RARIndiaException(ErrorCodes.ContactAdministrator, null);
                }
                else
                {
                    userModel.SelectedRoleId = roleList.FirstOrDefault(x => x.RoleType == RARIndiaConstant.Regular).AdminRoleMasterId;
                    //userModel.SelectedRoleCode = roleList.FirstOrDefault(x => x.RoleType == RARIndiaConstant.Regular). AdminRoleCode;
                    foreach (AdminRoleApplicableDetail item in roleList)
                    {
                        userModel.RoleList.Add(new AdminRoleModel()
                        {
                            AdminRoleMasterID = item.AdminRoleMasterId,
                            RoleType= item.RoleType,
                        });
                    }
                }
            }
        }

        //Bind Menu And Modules For Admin User
        private void BindMenuAndModulesForAdminUser(UserModel userModel, List<UserModuleMaster> userAllModuleList, List<UserMainMenuMaster> userAllMenuList)
        {
            foreach (UserModuleMaster item in userAllModuleList)
            {
                userModel.ModuleList.Add(new UserModuleModel()
                {
                    UserModuleMasterId = item.UserModuleMasterId,
                    ModuleCode = item.ModuleCode,
                    ModuleName = item.ModuleName,
                    ModuleSeqNumber = item.ModuleSeqNumber,
                    ModuleTooltip = item.ModuleTooltip,
                    ModuleIconName = item.ModuleIconName,
                    ModuleColorClass = item.ModuleColorClass,
                });
            }

            foreach (UserMainMenuMaster item in userAllMenuList)
            {
                userModel.MenuList.Add(new UserMenuModel()
                {
                    UserMainMenuMasterId = item.UserMainMenuMasterId,
                    ModuleCode = item.ModuleCode,
                    MenuCode = item.MenuCode,
                    MenuName = item.MenuName,
                    ParentMenuID = item.ParentMenuId,
                    MenuDisplaySeqNo = item.MenuDisplaySeqNo,
                    ControllerName = item.ControllerName?.ToLower(),
                    ActionName = item.ActionName?.ToLower(),
                    MenuLink = item.ControllerName?.ToLower() + "/" + item.ActionName?.ToLower(),
                    MenuToolTip = item.MenuToolTip,
                    MenuIconName = item.MenuIconName
                });
            }
        }

        //Bind Menu And Modules For Non Admin User
        private void BindMenuAndModulesForNonAdminUser(UserModel userModel, List<UserModuleMaster> userAllModuleList, List<UserMainMenuMaster> userAllMenuList, List<AdminRoleMenuDetail> userRoleMenuList)
        {
            //Bind Menu & Module for non admin user
            foreach (AdminRoleMenuDetail item in userRoleMenuList)
            {
                UserMainMenuMaster userMenuModel = userAllMenuList.FirstOrDefault(x => x.MenuCode == item.MenuCode);
                if (IsNotNull(userMenuModel))
                {
                    userModel.MenuList.Add(new UserMenuModel()
                    {
                        UserMainMenuMasterId = userMenuModel.UserMainMenuMasterId,
                        ModuleCode = userMenuModel.ModuleCode,
                        MenuCode = userMenuModel.MenuCode,
                        MenuName = userMenuModel.MenuName,
                        ParentMenuID = userMenuModel.ParentMenuId,
                        MenuDisplaySeqNo = userMenuModel.MenuDisplaySeqNo,
                        ControllerName = userMenuModel.ControllerName?.ToLower(),
                        ActionName = userMenuModel.ActionName?.ToLower(),
                        MenuLink = userMenuModel.ControllerName?.ToLower() + "/" + userMenuModel.ActionName?.ToLower(),
                        MenuToolTip = userMenuModel.MenuToolTip,
                        MenuIconName = userMenuModel.MenuIconName
                    });

                    if (!userModel.ModuleList.Any(x => x.ModuleCode == userMenuModel.ModuleCode))
                    {
                        userModel.ModuleList.Add(userAllModuleList.FirstOrDefault(x => x.ModuleCode == userMenuModel.ModuleCode).FromEntityToModel<UserModuleModel>());
                    }
                }
            }
        }

        public List<UserBalanceSheetModel> BindAccountBalanceSheetByRoleID(UserModel userModel)
        {
            int errorCode = 0;
            RARIndiaViewRepository<UserBalanceSheetModel> objStoredProc = new RARIndiaViewRepository<UserBalanceSheetModel>();
            objStoredProc.SetParameter("@iAdminRoleId", userModel.SelectedRoleId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@iErrorCode", userModel.ErrorCode, ParameterDirection.Output, DbType.Int32);
            List<UserBalanceSheetModel> accountBalanceSheetList = objStoredProc.ExecuteStoredProcedureList("USP_GetBalancesheetList @iAdminRoleId,@iErrorCode OUT", 1, out errorCode)?.ToList();
            if (errorCode == 0 && accountBalanceSheetList?.Count > 0)
            {
                userModel.SelectedBalanceId = accountBalanceSheetList.FirstOrDefault().BalsheetID;
                userModel.SelectedBalanceSheet = accountBalanceSheetList.FirstOrDefault().ActBalsheetHeadDesc;
            }
            return accountBalanceSheetList;
        }
        #endregion
    }
}
