using RARIndia.DataAccessLayer;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System;
using System.Collections.Specialized;
using System.Linq;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.BusinessLogicLayer
{
    public class AdminRoleMasterBA : BaseBusinessLogic
    {
        AdminRoleMasterDAL _adminRoleMasterDAL = null;
        public AdminRoleMasterBA()
        {
            _adminRoleMasterDAL = new AdminRoleMasterDAL();
        }

        public AdminRoleMasterListViewModel GetAdminRoleMasterList(DataTableModel dataTableModel, string centreCode, int departmentId)
        {
            FilterCollection filters = null;
            centreCode = SpiltCentreCode(centreCode);
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("AdminRoleCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("SanctPostName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MonitoringLevel", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            AdminRoleMasterListModel adminRoleMasterList = _adminRoleMasterDAL.GetAdminRoleMasterList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize, centreCode, departmentId);
            AdminRoleMasterListViewModel listViewModel = new AdminRoleMasterListViewModel { AdminRoleMasterList = adminRoleMasterList?.AdminRoleMasterList?.ToViewModel<AdminRoleMasterViewModel>().ToList() };
            SetListPagingData(listViewModel.PageListViewModel, adminRoleMasterList, dataTableModel, listViewModel.AdminRoleMasterList.Count);

            return listViewModel;
        }

        //Get AdminRoleMaster by AdminRoleMaster id.
        public AdminRoleMasterViewModel GetAdminRoleMasterDetailsById(int adminRoleMasterId)
            => _adminRoleMasterDAL.GetAdminRoleMasterDetailsById(adminRoleMasterId).ToViewModel<AdminRoleMasterViewModel>();

        //Update AdminRoleMaster.
        public AdminRoleMasterViewModel UpdateAdminRoleMaster(AdminRoleMasterViewModel adminRoleMasterViewModel)
        {
            try
            {
                adminRoleMasterViewModel.CreatedBy = LoginUserId();
                adminRoleMasterViewModel.ModifiedBy = LoginUserId();
                AdminRoleMasterModel adminRoleMasterModel = _adminRoleMasterDAL.UpdateAdminRoleMaster(adminRoleMasterViewModel.ToModel<AdminRoleMasterModel>());
                return IsNotNull(adminRoleMasterModel) ? adminRoleMasterModel.ToViewModel<AdminRoleMasterViewModel>() : (AdminRoleMasterViewModel)GetViewModelWithErrorMessage(new AdminRoleMasterListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.AdminRoleMaster.ToString());
                return (AdminRoleMasterViewModel)GetViewModelWithErrorMessage(adminRoleMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        ////Delete AdminRoleMaster.
        //public bool DeleteAdminRoleMaster(string AdminRoleMasterIds, out string errorMessage)
        //{
        //    errorMessage = Resources.ErrorFailedToDelete;
        //    try
        //    {
        //        return _adminRoleMasterDAL.DeleteAdminRoleMaster(new ParameterModel() { Ids = AdminRoleMasterIds });
        //    }
        //    catch (RARIndiaException ex)
        //    {
        //        switch (ex.ErrorCode)
        //        {
        //            default:
        //                errorMessage = Resources.ErrorFailedToDelete;
        //                return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.AdminRoleMaster.ToString());
        //        errorMessage = Resources.ErrorFailedToDelete;
        //        return false;
        //    }
        //}

        //public AdminRoleMasterListModel GetAdminRoleMastersByCentreCode(string centreCode, int departmentID = 0)
        //{
        //    centreCode = !string.IsNullOrEmpty(centreCode) && centreCode.Contains(":") ? centreCode.Split(':')[0] : centreCode;
        //    AdminRoleMasterListModel list = _adminRoleMasterDAL.GetAdminRoleMastersByCentreCode(centreCode);
        //    list.SelectedAdminRoleMasterID = departmentID;
        //    return list;
        //}

    }
}
