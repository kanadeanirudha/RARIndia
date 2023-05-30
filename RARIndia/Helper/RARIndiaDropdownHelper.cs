﻿using RARIndia.BusinessLogicLayer;
using RARIndia.Model;
using RARIndia.Model.Model;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RARIndia.DropdownHelper
{
    public static class RARIndiaDropdownHelper
    {
        public static List<UserAccessibleCentreModel> AccessibleCentreList()
        {
            return RARIndiaSessionHelper.GetDataFromSession<UserModel>(RARIndiaConstant.UserDataSession)?.AccessibleCentreList;
        }

        public static DropdownViewModel GeneralDropdownList(DropdownViewModel dropdownViewModel)
        {
            List<SelectListItem> dropdownList = new List<SelectListItem>();
            if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.City.ToString()))
            {
                GeneralCityListViewModel list = new GeneralCityMasterBA().GetCityList(new DataTableModel() { PageSize = int.MaxValue });
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select City-------" });
                foreach (var item in list.GeneralCityList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = string.Concat(item.CityName, " (", item.RegionName, ")"),
                        Value = Convert.ToString(item.GeneralCityMasterId),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralCityMasterId)
                    });
                }
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.AccessibleCentre.ToString()))
            {
                List<UserAccessibleCentreModel> accessibleCentreList = AccessibleCentreList();
                if (accessibleCentreList?.Count > 1)
                    dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Centre-------" });
                foreach (var item in accessibleCentreList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.CentreName,
                        Value = item.CentreCode,
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.CentreCode)
                    });
                }
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CentrewiseDepartment.ToString()))
            {
                if (AccessibleCentreList()?.Count == 1 && string.IsNullOrEmpty(dropdownViewModel.Parameter))
                {
                    dropdownViewModel.Parameter = RARIndiaSessionHelper.GetDataFromSession<UserModel>(RARIndiaConstant.UserDataSession).SelectedCentreCode;
                }
                GeneralDepartmentListModel list = new GeneralDepartmentMasterBA().GetDepartmentsByCentreCode(dropdownViewModel.Parameter);
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Department-------" });
                foreach (var item in list?.GeneralDepartmentList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.DepartmentName,
                        Value = item.GeneralDepartmentMasterId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralDepartmentMasterId)
                    });
                }
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Designation.ToString()))
            {
                GeneralDesignationListModel list = new GeneralDesignationMasterBA().GetDesignations();
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Designation-------" });
                foreach (var item in list.GeneralDesignationList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.Description,
                        Value = item.EmployeeDesignationMasterId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.EmployeeDesignationMasterId)
                    });
                }
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Department.ToString()))
            {
                GeneralDepartmentListViewModel list = new GeneralDepartmentMasterBA().GetDepartmentList(new DataTableModel() { PageSize = int.MaxValue });
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Department-------" });
                foreach (var item in list?.GeneralDepartmentList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.DepartmentName,
                        Value = item.GeneralDepartmentMasterId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralDepartmentMasterId)
                    });
                }
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Organisation.ToString()))
            {
                OrganisationMasterViewModel item
                    = new OrganisationMasterBA().GetOrganisationDetails();
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.OrganisationName,
                    Value = item.OrganisationMasterId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.OrganisationMasterId)
                });
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.RegionalOffice.ToString()))
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = "Centre",
                    Value = "CO",
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(dropdownViewModel.Parameter)
                });
                dropdownList.Add(new SelectListItem()
                {
                    Text = "Head Office",
                    Value = "HO",
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(dropdownViewModel.Parameter)
                });
                dropdownList.Add(new SelectListItem()
                {
                    Text = "Regional Office",
                    Value = "RO",
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(dropdownViewModel.Parameter)
                });

            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Centre.ToString()))
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = "Centre",
                    Value = "CO",
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(dropdownViewModel.Parameter)
                });
            }
            dropdownViewModel.DropdownList = dropdownList;
            return dropdownViewModel;
        }

    }
}