using System;
using System.Collections.Generic;

namespace RARIndia.Model
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            RoleList = new List<AdminRoleModel>();
            ModuleList = new List<UserModuleModel>();
            MenuList = new List<UserMenuModel>();
        }
        public int ID { get; set; }
        public bool IsAdminUser { get; set; }
        public short UserTypeID { get; set; }
        public string UserType { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string DeviceToken { get; set; }
        public string LastModuleCode { get; set; }
        public int SelectedRoleId { get; set; }
        public string SelectedRoleCode { get; set; }
        public string SelectedModuleCode { get; set; }
        public string SelectedModuleName { get; set; } = "Dashboard";
        public int SelectedMenuCode { get; set; }
        public List<AdminRoleModel> RoleList { get; set; }
        public List<UserModuleModel> ModuleList { get; set; }
        public List<UserMenuModel> MenuList { get; set; }
    }
}
