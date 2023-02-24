using RARIndia.Model;

using System.Collections.Generic;

namespace RARIndia.ViewModel
{
    public class AdminRoleMasterListViewModel : BaseViewModel
    {
        public AdminRoleMasterListViewModel()
        {
            AdminRoleMasterList = new List<AdminRoleMasterViewModel>();
            GeneralDepartmentList = new GeneralDepartmentListModel();
        }
        public List<AdminRoleMasterViewModel> AdminRoleMasterList { get; set; }
        public GeneralDepartmentListModel GeneralDepartmentList { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentID { get; set; }
    }
}
