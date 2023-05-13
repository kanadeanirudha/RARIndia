using RARIndia.BusinessLogicLayer;
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

        public static GeneralDesignationListModel DesignationList(Int16 designationID)
        {
            return new GeneralDesignationMasterBA().GetDesignations(designationID);
        }

        public static GeneralDepartmentListModel CentreWiseDepartmentList(string centreCode)
        {
            GeneralDepartmentListModel list = new GeneralDepartmentMasterBA().GetDepartmentsByCentreCode(centreCode);
            return list;
        }

        public static DropdownViewModel GeneralDropdownList(DropdownViewModel dropdownViewModel)
        {
            List<SelectListItem> dropdownList = new List<SelectListItem>();
            dropdownList.Add(new SelectListItem() { Text = "-------Select-------" });
            if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.City.ToString()))
            {
                GeneralCityListViewModel list = new GeneralCityMasterBA().GetCityList(new DataTableModel() { PageSize= int.MaxValue });
                foreach (var item in list.GeneralCityList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = string.Concat( item.CityName," (",item.RegionName,")"),
                        Value = Convert.ToString(item.GeneralCityMasterId),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralCityMasterId)
                    });
                }
            }

            dropdownViewModel.DropdownList = dropdownList;
            return dropdownViewModel;
        }

    }
}