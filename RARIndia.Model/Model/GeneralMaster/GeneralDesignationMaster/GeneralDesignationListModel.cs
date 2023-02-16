using System.Collections.Generic;

namespace RARIndia.Model
{
    public class GeneralDesignationListModel : BaseListModel
    {
        public List<GeneralDesignationModel> GeneralDesignationList { get; set; }
        public GeneralDesignationListModel()
        {
            GeneralDesignationList = new List<GeneralDesignationModel>();
        }

        public int SelectedDesignationID { get; set; }
    }
}
