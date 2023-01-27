using System.Collections.Generic;

namespace RARIndia.Model
{
    public class GeneralDepartmentListModel : BaseListModel
    {
        public List<GeneralDepartmentModel> GeneralDepartmentList { get; set; }
        public GeneralDepartmentListModel()
        {
            GeneralDepartmentList = new List<GeneralDepartmentModel>();
        }

        public int SelectedDepartmentID { get; set; }
    }
}
