using System.Collections.Generic;

namespace RARIndia.Model
{
    public class GeneralCityListModel : BaseListModel
    {
        public List<GeneralCityModel> GeneralCityList { get; set; }
        public GeneralCityListModel()
        {
            GeneralCityList = new List<GeneralCityModel>();
        }
    }
}
