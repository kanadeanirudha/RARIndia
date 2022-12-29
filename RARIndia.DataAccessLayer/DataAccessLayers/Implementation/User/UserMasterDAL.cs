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
            }
            else
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
            return userModel;
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

        #endregion
    }
}
