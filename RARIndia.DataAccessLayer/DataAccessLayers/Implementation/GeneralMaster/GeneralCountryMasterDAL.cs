using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RARIndia.DataAccessLayer
{
    public class GeneralCountryMasterDAL
    {
        RARIndiaEntities db = null;
        public GeneralCountryMasterDAL()
        {
            db = new RARIndiaEntities();
        }
        public List<GeneralCountryMasterModel> GetGeneralCountryMasterData()
        {
            List<GeneralCountryMasterModel> countryList = new List<GeneralCountryMasterModel>();

            try
            {
                using (db)
                {
                    List<GeneralCountryMaster> countryDBList = db.GeneralCountryMasters?.ToList();
                    if (countryDBList?.Count > 0)
                    {
                        foreach (GeneralCountryMaster country in countryDBList)
                        {
                            countryList.Add(new GeneralCountryMasterModel()
                            {
                                ID = country.ID,
                                CountryName = country.CountryName,
                                CountryCode = country.ContryCode
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return countryList;
        }
    }
}
