using RARIndia.BusinessLogicLayer;
using RARIndia.Model;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;

using System;
using System.Collections.Generic;

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
    }
}