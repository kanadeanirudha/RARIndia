using RARIndia.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RARIndia.DataAccessLayer
{
    public class GeneralCountryMasterDAL
    {
        public GeneralCountryMasterDAL()
        {

        }
        public GeneralCountryMasterListModel GetGeneralCountryMasterData()
        {
            GeneralCountryMasterListModel list = new GeneralCountryMasterListModel();

            list.GeneralCountryMasterList = new List<GeneralCountryMasterModel>();
            GeneralCountryMasterModel model = new GeneralCountryMasterModel()
            {
                ID = 1,
                CountryName = "India",
                CountryCode = "IN"
            };
            list.GeneralCountryMasterList.Add(model);

            GeneralCountryMasterModel model1 = new GeneralCountryMasterModel();
            model1.ID = 1;
            model1.CountryName = "India";
            model1.CountryCode = "IN";

            list.GeneralCountryMasterList.Add(model1);

            return list;
        }
    }
}
