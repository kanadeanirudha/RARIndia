using System.Collections.Generic;

namespace RARIndia.Model
{
    public class AdminRoleMasterListModel : BaseListModel
    {
        public List<AdminRoleMasterModel> AdminRoleMasterList { get; set; }
        public AdminRoleMasterListModel()
        {
            AdminRoleMasterList = new List<AdminRoleMasterModel>();
        }
    }
}
