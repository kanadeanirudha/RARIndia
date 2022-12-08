using RARIndia.BusinessLogicLayer;
using RARIndia.Helper;
using RARIndia.Model;
using RARIndia.ViewModel;

using System.Collections.Generic;
using System.Web.Mvc;

namespace RARIndia.Controllers
{
    public class GeneralCountryMasterController : BaseController
    {
        GeneralCountryMasterBA generalCountryMasterBA = null;
        public GeneralCountryMasterController()
        {
            generalCountryMasterBA = new GeneralCountryMasterBA();
        }

        [HttpGet]
        public ActionResult List()
        {
            GeneralCountryMasterListViewModel list = new GeneralCountryMasterListViewModel();
            list.GeneralCountryMasterList = generalCountryMasterBA.GetGeneralCountryMasterData();
            return View("~/Views/GeneralMaster/GeneralCountryMaster/List.cshtml", list);
        }
    }
}