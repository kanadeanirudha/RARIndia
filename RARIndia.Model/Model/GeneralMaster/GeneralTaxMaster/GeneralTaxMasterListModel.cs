using System.Collections.Generic;

namespace RARIndia.Model
{
    public class GeneralTaxMasterListModel : BaseListModel
    {
        public List<GeneralTaxMasterModel> GeneralTaxMasterList { get; set; }
        public GeneralTaxMasterListModel()
        {
            GeneralTaxMasterList = new List<GeneralTaxMasterModel>();
        }
    }
}
