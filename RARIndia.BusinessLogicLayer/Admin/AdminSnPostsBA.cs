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
    public class AdminSnPostsBA : BaseBusinessLogic
    {
        AdminSnPostsDAL _adminSnPostsDAL = null;
        public AdminSnPostsBA()
        {
            _adminSnPostsDAL = new AdminSnPostsDAL();
        }

        public AdminSnPostsListViewModel GetAdminSnPostsList(DataTableModel dataTableModel, string centreCode, int departmentId)
        {
            FilterCollection filters = null;
            centreCode = SpiltCentreCode(centreCode);
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("NomenAdminRoleCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("SactionedPostDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            AdminSnPostsListModel adminSnPostsList = _adminSnPostsDAL.GetAdminSnPostsList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize, centreCode, departmentId);
            AdminSnPostsListViewModel listViewModel = new AdminSnPostsListViewModel { AdminSnPostsList = adminSnPostsList?.AdminSnPostsList?.ToViewModel<AdminSnPostsViewModel>().ToList() };
            SetListPagingData(listViewModel.PageListViewModel, adminSnPostsList, dataTableModel, listViewModel.AdminSnPostsList.Count);

            return listViewModel;
        }

        //Create AdminSnPosts.
        public AdminSnPostsViewModel CreateAdminSnPosts(AdminSnPostsViewModel adminSnPostsViewModel)
        {
            try
            {
                adminSnPostsViewModel.CentreCode = SpiltCentreCode(adminSnPostsViewModel.SelectedCentreCode);
                adminSnPostsViewModel.DepartmentID = Convert.ToInt16(adminSnPostsViewModel.SelectedDepartmentID);
                adminSnPostsViewModel.IsActive = true;
                adminSnPostsViewModel.CreatedBy = LoginUserId();
                AdminSnPostsModel adminSnPostsModel = _adminSnPostsDAL.CreateAdminSnPosts(adminSnPostsViewModel.ToModel<AdminSnPostsModel>());
                return IsNotNull(adminSnPostsModel) ? adminSnPostsModel.ToViewModel<AdminSnPostsViewModel>() : new AdminSnPostsViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AdminSnPostsViewModel)GetViewModelWithErrorMessage(adminSnPostsViewModel, ex.ErrorMessage);
                    default:
                        return (AdminSnPostsViewModel)GetViewModelWithErrorMessage(adminSnPostsViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.AdminSnPosts.ToString());
                return (AdminSnPostsViewModel)GetViewModelWithErrorMessage(adminSnPostsViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get AdminSnPosts by AdminSnPosts id.
        public AdminSnPostsViewModel GetAdminSnPosts(int adminSnPostsId)
            => _adminSnPostsDAL.GetAdminSnPosts(adminSnPostsId).ToViewModel<AdminSnPostsViewModel>();

        //Update AdminSnPosts.
        public AdminSnPostsViewModel UpdateAdminSnPosts(AdminSnPostsViewModel adminSnPostsViewModel)
        {
            try
            {
                adminSnPostsViewModel.ModifiedBy = LoginUserId();
                AdminSnPostsModel adminSnPostsModel = _adminSnPostsDAL.UpdateAdminSnPosts(adminSnPostsViewModel.ToModel<AdminSnPostsModel>());
                return IsNotNull(adminSnPostsModel) ? adminSnPostsModel.ToViewModel<AdminSnPostsViewModel>() : (AdminSnPostsViewModel)GetViewModelWithErrorMessage(new AdminSnPostsListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.AdminSnPosts.ToString());
                return (AdminSnPostsViewModel)GetViewModelWithErrorMessage(adminSnPostsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        ////Delete AdminSnPosts.
        //public bool DeleteAdminSnPosts(string AdminSnPostsIds, out string errorMessage)
        //{
        //    errorMessage = Resources.ErrorFailedToDelete;
        //    try
        //    {
        //        return _adminSnPostsDAL.DeleteAdminSnPosts(new ParameterModel() { Ids = AdminSnPostsIds });
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
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.AdminSnPosts.ToString());
        //        errorMessage = Resources.ErrorFailedToDelete;
        //        return false;
        //    }
        //}

        //public AdminSnPostsListModel GetAdminSnPostssByCentreCode(string centreCode, int departmentID = 0)
        //{
        //    centreCode = !string.IsNullOrEmpty(centreCode) && centreCode.Contains(":") ? centreCode.Split(':')[0] : centreCode;
        //    AdminSnPostsListModel list = _adminSnPostsDAL.GetAdminSnPostssByCentreCode(centreCode);
        //    list.SelectedAdminSnPostsID = departmentID;
        //    return list;
        //}

    }
}
