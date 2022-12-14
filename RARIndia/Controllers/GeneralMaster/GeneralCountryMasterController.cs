using RARIndia.BusinessLogicLayer;
using RARIndia.ViewModel;

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
            GeneralCountryListViewModel list = new GeneralCountryListViewModel();
            list = generalCountryMasterBA.GetCountryList(null,null,null,1,10);
            return View("~/Views/GeneralMaster/GeneralCountryMaster/List.cshtml", list);
        }
    }
}