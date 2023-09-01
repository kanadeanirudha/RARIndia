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
    public class GeneralTaxMasterController : BaseController
    {
        GeneralTaxMasterBA _generalTaxMasterBA = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralTaxMaster/CreateEdit.cshtml";
        public GeneralTaxMasterController()
        {
            _generalTaxMasterBA = new GeneralTaxMasterBA();
        }

        public ActionResult List(DataTableModel dataTableModel)
        {
            DataTableModel tempDataTable = TempData[RARIndiaConstant.DataTableModel] as DataTableModel;
            dataTableModel = tempDataTable == null ? dataTableModel ?? new DataTableModel() : tempDataTable;
            GeneralTaxMasterListViewModel list = _generalTaxMasterBA.GetTaxMasterList(dataTableModel);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/GeneralMaster/GeneralTaxMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralTaxMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralTaxMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralTaxMasterViewModel generalTaxMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                generalTaxMasterViewModel = _generalTaxMasterBA.CreateTaxMaster(generalTaxMasterViewModel);
                if (!generalTaxMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
                    return RedirectToAction<GeneralTaxMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalTaxMasterViewModel.ErrorMessage));
            return View(createEdit, generalTaxMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int TaxMasterId)
        {
            GeneralTaxMasterViewModel generalTaxMasterViewModel = _generalTaxMasterBA.GetTaxMaster(TaxMasterId);
            return ActionView(createEdit, generalTaxMasterViewModel);
        }

        //Post:Edit Tax Master.
        [HttpPost]
        public virtual ActionResult Edit(GeneralTaxMasterViewModel generalTaxMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _generalTaxMasterBA.UpdateTaxMaster(generalTaxMasterViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable();
                    return RedirectToAction<GeneralTaxMasterController>(x => x.List(null));
                }
            }
            return View(createEdit, generalTaxMasterViewModel);
        }

        //Delete Tax Master.
        public virtual ActionResult Delete(string TaxMasterId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(TaxMasterId))
            {
                status = _generalTaxMasterBA.DeleteTaxMaster(TaxMasterId, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralTaxMasterController>(x => x.List(null));
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralTaxMasterController>(x => x.List(null));
        }
    }
}








