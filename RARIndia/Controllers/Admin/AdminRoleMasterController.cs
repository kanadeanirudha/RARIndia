using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RARIndia.Controllers
{
	[SessionTimeoutAttribute]
	public class AdminRoleMasterController : BaseController
	{
		readonly GeneralDepartmentMasterBA _generalDepartmentMasterBA = null;
		readonly AdminRoleMasterBA _adminRoleMasterBA = null;

		public AdminRoleMasterController()
		{
			_generalDepartmentMasterBA = new GeneralDepartmentMasterBA();
			_adminRoleMasterBA = new AdminRoleMasterBA();
		}

		public ActionResult List(DataTableModel dataTableModel)
		{
			DataTableModel tempDataTable = TempData[RARIndiaConstant.DataTableModel] as DataTableModel;

			dataTableModel = tempDataTable == null ? dataTableModel ?? new DataTableModel() : tempDataTable;

			AdminRoleMasterListViewModel viewModel = new AdminRoleMasterListViewModel();

			if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentID > 0)
			{
				viewModel = _adminRoleMasterBA.GetAdminRoleMasterList(dataTableModel, dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentID);
				if (!Request.IsAjaxRequest())
				{
					viewModel.GeneralDepartmentList = _generalDepartmentMasterBA.GetDepartmentsByCentreCode(dataTableModel.SelectedCentreCode, Convert.ToString(dataTableModel.SelectedDepartmentID));
				}
			}

			viewModel.SelectedCentreCode = dataTableModel.SelectedCentreCode;
			viewModel.SelectedDepartmentID = dataTableModel.SelectedDepartmentID;

			if (Request.IsAjaxRequest())
			{
				return PartialView("~/Views/Admin/AdminRoleMaster/_List.cshtml", viewModel);
			}
			return View("~/Views/Admin/AdminRoleMaster/List.cshtml", viewModel);
		}

		[HttpGet]
		public ActionResult Edit(int adminRoleMasterId, string selectedCentreCode, string selectedDepartmentID)
		{
			AdminRoleMasterViewModel adminRoleMasterViewModel = _adminRoleMasterBA.GetAdminRoleMasterDetailsById(adminRoleMasterId);
			adminRoleMasterViewModel.SelectedCentreCode = selectedCentreCode;
			adminRoleMasterViewModel.SelectedDepartmentID = selectedDepartmentID;
			BindDropdown(adminRoleMasterViewModel);
			return View("~/Views/Admin/AdminRoleMaster/Edit.cshtml", adminRoleMasterViewModel);
		}

		[HttpPost]
		public ActionResult Edit(AdminRoleMasterViewModel adminRoleMasterViewModel)
		{
			if (ModelState.IsValid)
			{
				bool status = _adminRoleMasterBA.UpdateAdminRoleMaster(adminRoleMasterViewModel).HasError;
				SetNotificationMessage(status
				? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
				: GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

				if (!status)
				{
					TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable(adminRoleMasterViewModel.SelectedCentreCode, System.Convert.ToInt32(adminRoleMasterViewModel.SelectedDepartmentID));
					return RedirectToAction<AdminRoleMasterController>(x => x.List(null));
				}
			}

			SetNotificationMessage(GetErrorNotificationMessage(adminRoleMasterViewModel.ErrorMessage));
			return View("~/Views/Admin/AdminRoleMaster/Edit.cshtml", adminRoleMasterViewModel);
		}

		#region Private
		private void BindDropdown(AdminRoleMasterViewModel adminRoleMasterViewModel)
		{
			List<SelectListItem> monitoringLevelList = new List<SelectListItem>();
			monitoringLevelList.Add(new SelectListItem { Text = RARIndiaConstant.Self, Value = RARIndiaConstant.Self, Selected = adminRoleMasterViewModel.MonitoringLevel == RARIndiaConstant.Self ? true : false });
			monitoringLevelList.Add(new SelectListItem { Text = RARIndiaConstant.Other, Value = RARIndiaConstant.Other, Selected = adminRoleMasterViewModel.MonitoringLevel == RARIndiaConstant.Other ? true : false });
			ViewData["MonitoringLevel"] = monitoringLevelList;

			List<SelectListItem> roleWiseCentreList = new List<SelectListItem>();
			foreach (var item in adminRoleMasterViewModel.SelectedRoleWiseCentreList)
			{
				roleWiseCentreList.Add(new SelectListItem { Text = item.CentreName, Value = item.CentreCode });
			}
			ViewData["RoleWiseCentreList"] = roleWiseCentreList;

			adminRoleMasterViewModel.SelectedCentreNameForSelf = adminRoleMasterViewModel.SelectedRoleWiseCentreList?.FirstOrDefault(x => x.CentreCode == adminRoleMasterViewModel.SelectedCentreCodeForSelf)?.CentreName;
		}
		#endregion
	}
}