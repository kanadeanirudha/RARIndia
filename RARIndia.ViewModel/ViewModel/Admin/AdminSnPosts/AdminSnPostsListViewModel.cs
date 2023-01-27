using RARIndia.Model;

using System.Collections.Generic;

namespace RARIndia.ViewModel
{
    public class AdminSnPostsListViewModel : BaseViewModel
    {
        public AdminSnPostsListViewModel()
        {
            AdminSnPostsList = new List<AdminSnPostsViewModel>();
            GeneralDepartmentList = new GeneralDepartmentListModel();
        }
        public List<AdminSnPostsViewModel> AdminSnPostsList { get; set; }
        public GeneralDepartmentListModel GeneralDepartmentList { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentID { get; set; }
    }
}
