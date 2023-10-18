using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class GymUserRegistrationController : BaseController
    {
        GymUserRegistrationBA _gymUserRegistrationBA = null;
        private const string createEdit = "~/Views/Gym/GymUserRegistration/CreateEdit.cshtml";
        public GymUserRegistrationController()
        {
            _gymUserRegistrationBA = new GymUserRegistrationBA();
        }

        public ActionResult List(DataTableModel dataTableModel)
        {
            DataTableModel tempDataTable = TempData[RARIndiaConstant.DataTableModel] as DataTableModel;
            dataTableModel = tempDataTable == null ? dataTableModel ?? new DataTableModel() : tempDataTable;

            GymUserRegistrationListViewModel list = _gymUserRegistrationBA.GetGymUserRegistrationList(dataTableModel);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Gym/GymUserRegistration/_List.cshtml", list);
            }
            return View($"~/Views/Gym/GymUserRegistration/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            GymUserRegistrationViewModel gmUserRegistrationViewModel = new GymUserRegistrationViewModel();
            BindDropDown(gmUserRegistrationViewModel);
            return View(createEdit, gmUserRegistrationViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(GymUserRegistrationViewModel gymUserRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                gymUserRegistrationViewModel = _gymUserRegistrationBA.CreateGymUserRegistration(gymUserRegistrationViewModel);
                if (!gymUserRegistrationViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
                    return RedirectToAction<GymUserRegistrationController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymUserRegistrationViewModel.ErrorMessage));
            BindDropDown(gymUserRegistrationViewModel);
            return View(createEdit, gymUserRegistrationViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int gymUserRegistrationId)
        {
            GymUserRegistrationViewModel gymUserRegistrationViewModel = _gymUserRegistrationBA.GetGymUserRegistration(gymUserRegistrationId);
            return ActionView(createEdit, gymUserRegistrationViewModel);
        }

        //Post:Edit gymUserRegistration.
        [HttpPost]
        public virtual ActionResult Edit(GymUserRegistrationViewModel gymUserRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {

                bool status = _gymUserRegistrationBA.UpdateGymUserRegistration(gymUserRegistrationViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable();
                    return RedirectToAction<GymUserRegistrationController>(x => x.List(null));
                }
            }
            return View(createEdit, gymUserRegistrationViewModel);
        }

        ////Delete gymUserRegistration.
        //public virtual ActionResult Delete(string gymUserRegistrationIds)
        //{
        //    string message = string.Empty;
        //    bool status = false;
        //    if (!string.IsNullOrEmpty(gymUserRegistrationIds))
        //    {
        //        status = _gymUserRegistrationBA.DeleteGymUserRegistration(gymUserRegistrationIds, out message);
        //        SetNotificationMessage(!status
        //        ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
        //        return RedirectToAction<GymUserRegistrationController>(x => x.List(null));
        //    }

        //    SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
        //    return RedirectToAction<GymUserRegistrationController>(x => x.List(null));
        //}
        [HttpPost]
        public ActionResult GetMembershipPlanDurationAmount(string gymMembershipPlanMasterId, string gymPlanDurationId)
        {
            int amount = _gymUserRegistrationBA.GetMembershipPlanDurationAmount(Convert.ToInt32(gymMembershipPlanMasterId), Convert.ToInt32(gymPlanDurationId));
            return Json(Convert.ToString(amount), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GymPrintUserRegistration(string gymUserRegistrationId)
        {
            GymUserRegistrationPrintModel model = _gymUserRegistrationBA.GymPrintUserRegistration(Convert.ToInt32(gymUserRegistrationId));
            return View($"~/Views/Gym/GymUserRegistration/GymPrintUserRegistration.cshtml", model);
        }
        #region Private
        protected void BindDropDown(GymUserRegistrationViewModel gymUserRegistrationViewModel)
        {
            List<SelectListItem> genderList = new List<SelectListItem>();
            //genderList.Add(new SelectListItem { Text = "------Select Gender------", Value = "" });
            genderList.Add(new SelectListItem { Text = "Male", Value = "1" });
            genderList.Add(new SelectListItem { Text = "Female", Value = "0" });
            ViewData["GenderList"] = genderList;

            gymUserRegistrationViewModel.AllPaymentTypeList = _gymUserRegistrationBA.GetAllGymPaymentTypes();
        }
        #endregion
    }
}