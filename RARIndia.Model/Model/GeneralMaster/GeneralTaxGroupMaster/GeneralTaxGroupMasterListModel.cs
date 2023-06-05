using System.Collections.Generic;

namespace RARIndia.Model
{
    public class GeneralTaxGroupMasterListModel : BaseListModel
    {
        public List<GeneralTaxGroupMasterModel> GeneralTaxGroupMasterList { get; set; }
        public GeneralTaxGroupMasterListModel()
        {
            GeneralTaxGroupMasterList = new List<GeneralTaxGroupMasterModel>();
        }
    }
}