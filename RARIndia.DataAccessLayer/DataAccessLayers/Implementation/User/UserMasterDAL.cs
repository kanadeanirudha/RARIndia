using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Helper;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Helper;

using System.Collections.Generic;
using System.Data;
using System.Linq;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.DataAccessLayer
{
    public class UserMasterDAL
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

            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.EmailID == userModel.EmailID && x.Password == userModel.Password);

            if (IsNull(userMasterData))
                throw new RARIndiaException(ErrorCodes.NotFound, null);
            else if (userMasterData.IsActive == false)
                throw new RARIndiaException(ErrorCodes.ContactAdministrator, null);

            userModel = userMasterData?.FromEntityToModel<UserModel>();
            userModel.IsAdminUser = IsAdminUser(userModel.UserType);
            //Bind Role
            BindRoleTypes(userModel);

            List<UserModuleMaster> userAllModuleList = new RARIndiaRepository<UserModuleMaster>().Table.Where(x => x.ModuleActiveFlag == true)?.ToList();
            List<UserMainMenuMaster> userAllMenuList = new RARIndiaRepository<UserMainMenuMaster>().Table.Where(x => x.IsEnable == true && x.IsDeleted == false)?.ToList();
            List<AdminRoleMenuDetail> userRoleMenuList = new List<AdminRoleMenuDetail>();
            if (!userModel.IsAdminUser)
            {
                userRoleMenuList = new RARIndiaRepository<AdminRoleMenuDetail>().Table.Where(x => x.IsActive == true && x.AdminRoleMasterID == userModel.SelectedRoleId)?.ToList();
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
                List<OrganisationStudyCentreMaster> centreList = new RARIndiaRepository<OrganisationStudyCentreMaster>().Table.ToList();
                foreach (OrganisationStudyCentreMaster item in centreList)
                {
                    userModel.AccessibleCentreList.Add(new UserAccessibleCentreModel()
                    {
                        CentreCode = item.CentreCode,
                        CentreName = item.CentreName,
                        ScopeIdentity = "Centre"
                    });
                }
            }
            return userModel;
        }

        public int GetNotificationCount(int userId)
        {
            int? notificationCount = new RARIndiaRepository<UserNotificationCount>().Table.FirstOrDefault(x => x.PersonID == userId)?.NotificationCount;
            return notificationCount != null && notificationCount > 0 ? (int)notificationCount : 0;
        }

        #endregion

        #region Private Method

        //Bind Role Types
        private void BindRoleTypes(UserModel userModel)
        {
            if (!userModel.IsAdminUser)
            {
                List<AdminRoleApplicableDetail> roleList = _adminRoleApplicableDetailsRepository.Table.Where(x => x.EmployeeID == userModel.ID && x.IsActive == true)?.ToList();
                if (roleList?.Count() == 0)
                {
                    throw new RARIndiaException(ErrorCodes.ContactAdministrator, null);
                }
                else
                {
                    userModel.SelectedRoleId = roleList.FirstOrDefault().AdminRoleMasterID;
                    userModel.SelectedRoleCode = roleList.FirstOrDefault().AdminRoleCode;
                    foreach (AdminRoleApplicableDetail item in roleList)
                    {
                        userModel.RoleList.Add(new AdminRoleModel()
                        {
                            AdminRoleMasterID = item.AdminRoleMasterID,
                            AdminRoleCode = item.AdminRoleCode,
                            RoleType = item.RoleType
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
                    ID = item.ID,
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
                    ID = item.ID,
                    ModuleID = item.ModuleID,
                    ModuleCode = item.ModuleCode,
                    MenuCode = item.MenuCode,
                    MenuName = item.MenuName,
                    ParentMenuID = item.ParentMenuID,
                    MenuDisplaySeqNo = item.MenuDisplaySeqNo,
                    MenuLink = item.MenuLink,
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
                        ID = userMenuModel.ID,
                        ModuleID = userMenuModel.ModuleID,
                        ModuleCode = userMenuModel.ModuleCode,
                        MenuCode = userMenuModel.MenuCode,
                        MenuName = userMenuModel.MenuName,
                        ParentMenuID = userMenuModel.ParentMenuID,
                        MenuDisplaySeqNo = userMenuModel.MenuDisplaySeqNo,
                        MenuLink = userMenuModel.MenuLink,
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
