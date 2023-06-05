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
    public class GeneralTaxGroupMasterController : BaseController
    {
        GeneralTaxGroupMasterBA _generalTaxGroupMasterBA = null;
        GeneralTaxMasterBA _generalTaxMasterBA = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralTaxGroupMaster/CreateEdit.cshtml";
        public GeneralTaxGroupMasterController()
        {
            _generalTaxGroupMasterBA = new GeneralTaxGroupMasterBA();
            _generalTaxMasterBA = new GeneralTaxMasterBA();
        }

        public ActionResult List(DataTableModel dataTableModel)
        {
            DataTableModel tempDataTable = TempData[RARIndiaConstant.DataTableModel] as DataTableModel;
            dataTableModel = tempDataTable == null ? dataTableModel ?? new DataTableModel() : tempDataTable;
            GeneralTaxGroupMasterListViewModel list = _generalTaxGroupMasterBA.GetTaxGroupMasterList(dataTableModel);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/GeneralMaster/GeneralTaxGroupMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralTaxGroupMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            GeneralTaxGroupMasterViewModel taxGroupMasterViewModel = new GeneralTaxGroupMasterViewModel();
            BindDropDown(taxGroupMasterViewModel);
            return View(createEdit, taxGroupMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                generalTaxGroupMasterViewModel = _generalTaxGroupMasterBA.CreateTaxGroupMaster(generalTaxGroupMasterViewModel);
                if (!generalTaxGroupMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
                    return RedirectToAction<GeneralTaxGroupMasterController>(x => x.List(null));
                }
            }
            BindDropDown(generalTaxGroupMasterViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(generalTaxGroupMasterViewModel.ErrorMessage));
            return View(createEdit, generalTaxGroupMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int taxGroupMasterId)
        {
            GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel = _generalTaxGroupMasterBA.GetTaxGroupMaster(taxGroupMasterId);
            BindDropDown(generalTaxGroupMasterViewModel);
            return ActionView(createEdit, generalTaxGroupMasterViewModel);
        }

        //Post:Edit Tax Group Master.
        [HttpPost]
        public virtual ActionResult Edit(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _generalTaxGroupMasterBA.CreateTaxGroupMaster(generalTaxGroupMasterViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable();
                    return RedirectToAction<GeneralTaxGroupMasterController>(x => x.List(null));
                }
            }
            BindDropDown(generalTaxGroupMasterViewModel);
            return View(createEdit, generalTaxGroupMasterViewModel);
        }

        //Delete Tax Master.
        public virtual ActionResult Delete(string taxGroupMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(taxGroupMasterIds))
            {
                status = _generalTaxGroupMasterBA.DeleteTaxGroupMaster(taxGroupMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralTaxGroupMasterController>(x => x.List(null));
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralTaxGroupMasterController>(x => x.List(null));
        }

        #region Private
        protected void BindDropDown(GeneralTaxGroupMasterViewModel taxGroupMasterViewModel)
        {
            taxGroupMasterViewModel.AllTaxList = _generalTaxMasterBA.GetAllTaxList().GeneralTaxMasterList;
        }
        #endregion
    }
}
