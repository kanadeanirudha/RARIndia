using RARIndia.DataAccessLayer;
using RARIndia.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RARIndia.BusinessLogicLayer
{
    public class GeneralCountryMasterBA
    {
        public GeneralCountryMasterBA()
        {

        }

        public List<GeneralCountryMasterModel> GetGeneralCountryMasterData()
        {
            GeneralCountryMasterDAL generalCountryMasterDAL = new GeneralCountryMasterDAL();
            return generalCountryMasterDAL.GetGeneralCountryMasterData();
        }

    }
}
