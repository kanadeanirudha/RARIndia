using System;
using System.Collections.Generic;

namespace RARIndia.Model
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            RoleList = new List<AdminRoleDetails>();
        }
        public int ID { get; set; }
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
        public List<AdminRoleDetails> RoleList { get; set; }
    }
}
