using RARIndia.BusinessLogicLayer;
using RARIndia.Model;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    public class GeneralCountryMasterController : Controller
    {
        GeneralCountryMasterBA generalCountryMasterBA = null;
        public GeneralCountryMasterController()
        {
            generalCountryMasterBA= new GeneralCountryMasterBA();
        }

        [HttpGet]
        public ActionResult List()
        {
            GeneralCountryMasterListModel list = generalCountryMasterBA.GetGeneralCountryMasterData();
            return View("~/Views/GeneralMaster/GeneralCountryMaster/List.cshtml", list);
        }
    }
}