﻿using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.ViewModel;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class GeneralCountryMasterController : BaseController
    {
        GeneralCountryMasterBA _generalCountryMasterBA = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralCountryMaster/CreateEdit.cshtml";
        public GeneralCountryMasterController()
        {
            _generalCountryMasterBA = new GeneralCountryMasterBA();
        }

        public ActionResult List(DataTableModel dataTableModel)
        {
            DataTableModel tempDataTable = TempData[RARIndiaConstant.DataTableModel] as DataTableModel;
            dataTableModel = tempDataTable == null ? dataTableModel ?? new DataTableModel() : tempDataTable;
            
            GeneralCountryListViewModel list = _generalCountryMasterBA.GetCountryList(dataTableModel);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/GeneralMaster/GeneralCountryMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralCountryMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralCountryViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralCountryViewModel generalCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                generalCountryViewModel = _generalCountryMasterBA.CreateCountry(generalCountryViewModel);
                if (!generalCountryViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
                    return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalCountryViewModel.ErrorMessage));
            return View(createEdit, generalCountryViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int countryId)
        {
            GeneralCountryViewModel generalCountryViewModel = _generalCountryMasterBA.GetCountry(countryId);
            return ActionView(createEdit, generalCountryViewModel);
        }

        //Post:Edit country.
        [HttpPost]
        public virtual ActionResult Edit(GeneralCountryViewModel generalCountryViewModel)
        {
            if (ModelState.IsValid)
            {

                bool status = _generalCountryMasterBA.UpdateCountry(generalCountryViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable();
                    return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
                }
            }
            return View(createEdit, generalCountryViewModel);
        }

        //Delete country.
        public virtual ActionResult Delete(string countryIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(countryIds))
            {
                status = _generalCountryMasterBA.DeleteCountry(countryIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
        }

    }
}