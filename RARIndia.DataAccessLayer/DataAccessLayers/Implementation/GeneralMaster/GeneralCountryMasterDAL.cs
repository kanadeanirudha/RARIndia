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
        public List<GeneralCountryMasterModel> GetGeneralCountryMasterData()
        {
            List<GeneralCountryMasterModel> list = new List<GeneralCountryMasterModel>();

            GeneralCountryMasterModel model = new GeneralCountryMasterModel()
            {
                ID = 1,
                CountryName = "India",
                CountryCode = "IN"
            };
            list.Add(model);
            return list;
        }
    }
}
