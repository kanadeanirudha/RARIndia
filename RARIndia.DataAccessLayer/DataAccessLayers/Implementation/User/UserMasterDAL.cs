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
            if (!IsAdminUser(userModel.UserType))
            {
                List<AdminRoleApplicableDetail> roleList = _adminRoleApplicableDetailsRepository.Table.Where(x => x.EmployeeID == userModel.ID && x.IsActive == true)?.ToList();
                if (roleList?.Count() == 0)
                {
                    throw new RARIndiaException(ErrorCodes.ContactAdministrator, null);
                }
                else
                {
                    userModel.SelectedRoleId = roleList.FirstOrDefault().AdminRoleMasterID;
                    foreach (AdminRoleApplicableDetail item in roleList)
                    {
                        userModel.RoleList.Add(new AdminRoleDetails()
                        {
                            AdminRoleMasterID = item.AdminRoleMasterID,
                            AdminRoleCode = item.AdminRoleCode,
                            RoleType = item.RoleType
                        });
                    }
                }
            }
            return userModel;
        }

        #endregion

        #region Private Method


        #endregion
    }
}
