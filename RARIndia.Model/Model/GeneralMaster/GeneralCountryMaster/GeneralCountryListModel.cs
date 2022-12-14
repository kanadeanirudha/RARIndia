using System.Collections.Generic;

namespace RARIndia.Model
{
    public class GeneralCountryListModel : BaseListModel
    {
        public List<GeneralCountryModel> GeneralCountryList { get; set; }
        public GeneralCountryListModel()
        {
            GeneralCountryList = new List<GeneralCountryModel>();
        }
    }
}
