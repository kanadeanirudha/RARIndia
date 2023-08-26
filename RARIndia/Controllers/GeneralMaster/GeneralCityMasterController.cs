using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.ViewModel;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class GeneralCityMasterController : BaseController
    {
        GeneralCityMasterBA _generalCityMasterBA = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralCityMaster/CreateEdit.cshtml";
        public GeneralCityMasterController()
        {
            _generalCityMasterBA = new GeneralCityMasterBA();
        }

        public ActionResult List(DataTableModel dataTableModel)
        {
            DataTableModel tempDataTable = TempData[RARIndiaConstant.DataTableModel] as DataTableModel;
            dataTableModel = tempDataTable == null ? dataTableModel ?? new DataTableModel() : tempDataTable;
            
            GeneralCityListViewModel list = _generalCityMasterBA.GetCityList(dataTableModel);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/GeneralMaster/GeneralCityMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralCityMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralCityViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralCityViewModel generalCityViewModel)
        {
            if (ModelState.IsValid)
            {
                generalCityViewModel = _generalCityMasterBA.CreateCity(generalCityViewModel);
                if (!generalCityViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
                    return RedirectToAction<GeneralCityMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalCityViewModel.ErrorMessage));
            return View(createEdit, generalCityViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int cityId)
        {
            GeneralCityViewModel generalCityViewModel = _generalCityMasterBA.GetCity(cityId);
            return ActionView(createEdit, generalCityViewModel);
        }

        //Post:Edit city.
        [HttpPost]
        public virtual ActionResult Edit(GeneralCityViewModel generalCityViewModel)
        {
            if (ModelState.IsValid)
            {

                bool status = _generalCityMasterBA.UpdateCity(generalCityViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable();
                    return RedirectToAction<GeneralCityMasterController>(x => x.List(null));
                }
            }
            return View(createEdit, generalCityViewModel);
        }

        //Delete city.
        public virtual ActionResult Delete(string cityIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(cityIds))
            {
                status = _generalCityMasterBA.DeleteCity(cityIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralCityMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralCityMasterController>(x => x.List(null));
        }

    }
}