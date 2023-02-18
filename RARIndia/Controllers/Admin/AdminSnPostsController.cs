using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.ViewModel;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class AdminSnPostsController : BaseController
    {
        readonly GeneralDepartmentMasterBA _generalDepartmentMasterBA = null;
        readonly AdminSnPostsBA _adminSnPostsBA = null;

        public AdminSnPostsController()
        {
            _generalDepartmentMasterBA = new GeneralDepartmentMasterBA();
            _adminSnPostsBA = new AdminSnPostsBA();
        }

        public ActionResult List(DataTableModel dataTableModel)
        {
            DataTableModel tempDataTable = TempData["dataTableModel"] as DataTableModel;

            dataTableModel = tempDataTable == null ? dataTableModel ?? new DataTableModel() : tempDataTable;

            AdminSnPostsListViewModel viewModel = new AdminSnPostsListViewModel();

            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentID > 0)
            {
                viewModel = _adminSnPostsBA.GetAdminSnPostsList(dataTableModel, dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentID);
                if (!Request.IsAjaxRequest())
                {
                    viewModel.GeneralDepartmentList = _generalDepartmentMasterBA.GetDepartmentsByCentreCode(dataTableModel.SelectedCentreCode, Convert.ToString(dataTableModel.SelectedDepartmentID));

                }
            }

            viewModel.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            viewModel.SelectedDepartmentID = dataTableModel.SelectedDepartmentID;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Admin/AdminSnPosts/_List.cshtml", viewModel);
            }
            return View("~/Views/Admin/AdminSnPosts/List.cshtml", viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AdminSnPostsViewModel adminSnPostsViewModel = new AdminSnPostsViewModel();
            BindDropdown(adminSnPostsViewModel);
            return View("~/Views/Admin/AdminSnPosts/Create.cshtml", adminSnPostsViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(AdminSnPostsViewModel adminSnPostsViewModel)
        {
            if (ModelState.IsValid)
            {
                adminSnPostsViewModel = _adminSnPostsBA.CreateAdminSnPosts(adminSnPostsViewModel);
                if (!adminSnPostsViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    TempData["dataTableModel"] = CreateActionDataTable(adminSnPostsViewModel.SelectedCentreCode, System.Convert.ToInt32(adminSnPostsViewModel.SelectedDepartmentID));
                    return RedirectToAction<AdminSnPostsController>(x => x.List(null));
                }
            }

            BindDropdown(adminSnPostsViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(adminSnPostsViewModel.ErrorMessage));
            return View("~/Views/Admin/AdminSnPosts/Create.cshtml", adminSnPostsViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int adminSnPostsId)
        {
            AdminSnPostsViewModel adminSnPostsViewModel = _adminSnPostsBA.GetAdminSnPosts(adminSnPostsId);
            adminSnPostsViewModel.SelectedCentreCode = adminSnPostsViewModel.CentreCode;
            adminSnPostsViewModel.SelectedDepartmentID = System.Convert.ToString(adminSnPostsViewModel.DepartmentID);
            return View("~/Views/Admin/AdminSnPosts/Edit.cshtml", adminSnPostsViewModel);
        }

        [HttpPost]
        public ActionResult Edit(AdminSnPostsViewModel adminSnPostsViewModel)
        {
            ModelState.Remove("PostType");
            ModelState.Remove("DesignationType");

            if (ModelState.IsValid)
            {
                bool status = _adminSnPostsBA.UpdateAdminSnPosts(adminSnPostsViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData["dataTableModel"] = UpdateActionDataTable(adminSnPostsViewModel.SelectedCentreCode, System.Convert.ToInt32(adminSnPostsViewModel.SelectedDepartmentID));
                    return RedirectToAction<AdminSnPostsController>(x => x.List(null));
                }
            }

            SetNotificationMessage(GetErrorNotificationMessage(adminSnPostsViewModel.ErrorMessage));
            return View("~/Views/Admin/AdminSnPosts/Edit.cshtml", adminSnPostsViewModel);
        }

        #region Private
        private void BindDropdown(AdminSnPostsViewModel adminSnPostsViewModel)
        {

            if (!string.IsNullOrEmpty(adminSnPostsViewModel.SelectedCentreCode))
                adminSnPostsViewModel.GeneralDepartmentList = _generalDepartmentMasterBA.GetDepartmentsByCentreCode(adminSnPostsViewModel.SelectedCentreCode, adminSnPostsViewModel.SelectedDepartmentID);

            List<SelectListItem> postTypeList = new List<SelectListItem>();
            postTypeList.Add(new SelectListItem { Text = "Temporary", Value = "Temporary" });
            postTypeList.Add(new SelectListItem { Text = "Permanent", Value = "Permanent" });
            ViewData["PostType"] = postTypeList;

            List<SelectListItem> designationTypeList = new List<SelectListItem>();
            designationTypeList.Add(new SelectListItem { Text = "Regular", Value = "Regular" });
            designationTypeList.Add(new SelectListItem { Text = "AddOn", Value = "AddOn" });
            ViewData["DesignationType"] = designationTypeList;
        }
        #endregion
    }
}