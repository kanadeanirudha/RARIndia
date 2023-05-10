using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model.Model;
using RARIndia.ViewModel;
using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class GeneralNationalityMasterController : BaseController
    {
        GeneralNationalityMasterBA _generalNationalityMasterBA = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralNationalityMaster/CreateEdit.cshtml";
        public GeneralNationalityMasterController()
        {
            _generalNationalityMasterBA = new GeneralNationalityMasterBA();
        }
        public ActionResult List(DataTableModel dataTableModel)
        {
            dataTableModel = dataTableModel ?? new DataTableModel();
            GeneralNationalityListViewModel list = _generalNationalityMasterBA.GetNationalityList(dataTableModel);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/GeneralMaster/GeneralNationalityMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralNationalityMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralNationalityViewModel());
        }
    }
}