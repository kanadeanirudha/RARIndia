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
    public class GeneralDesignationMasterController : BaseController
    {
        readonly GeneralDesignationMasterBA _generalDesignationMasterBA = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralDesignationMaster/CreateEdit.cshtml";
        public GeneralDesignationMasterController()
        {
            _generalDesignationMasterBA = new GeneralDesignationMasterBA();
        }

        public ActionResult List(DataTableModel dataTableModel)
        {
            dataTableModel = dataTableModel ?? new DataTableModel();
            GeneralDesignationListViewModel list = _generalDesignationMasterBA.GetDesignationList(dataTableModel);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/GeneralMaster/GeneralDesignationMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralDesignationMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralDesignationViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralDesignationViewModel generalDesignationViewModel)
        {
            if (ModelState.IsValid)
            {
                generalDesignationViewModel = _generalDesignationMasterBA.CreateDesignation(generalDesignationViewModel);
                if (!generalDesignationViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    return RedirectToAction<GeneralDesignationMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalDesignationViewModel.ErrorMessage));
            return View(createEdit, generalDesignationViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int DesignationId)
        {
            GeneralDesignationViewModel generalDesignationViewModel = _generalDesignationMasterBA.GetDesignation(DesignationId);
            return ActionView(createEdit, generalDesignationViewModel);
        }

        //Post:Edit Designation.
        [HttpPost]
        public virtual ActionResult Edit(GeneralDesignationViewModel generalDesignationViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _generalDesignationMasterBA.UpdateDesignation(generalDesignationViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                    return RedirectToAction<GeneralDesignationMasterController>(x => x.List(new DataTableModel() { SortByColumn = SortKeys.ModifiedDate, SortBy = RARIndiaConstant.DESCKey }));
            }
            return View(createEdit, generalDesignationViewModel);
        }

        //Delete Designation.
        public virtual ActionResult Delete(string DesignationIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(DesignationIds))
            {
                status = _generalDesignationMasterBA.DeleteDesignation(DesignationIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralDesignationMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralDesignationMasterController>(x => x.List(null));
        }
    }
}