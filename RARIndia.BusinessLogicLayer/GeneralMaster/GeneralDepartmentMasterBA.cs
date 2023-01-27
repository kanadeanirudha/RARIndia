using RARIndia.DataAccessLayer;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace RARIndia.BusinessLogicLayer
{
    public class GeneralDepartmentMasterBA : BaseBusinessLogic
    {
        GeneralDepartmentMasterDAL _generalDepartmentMasterDAL = null;
        public GeneralDepartmentMasterBA()
        {
            _generalDepartmentMasterDAL = new GeneralDepartmentMasterDAL();
        }

        public GeneralDepartmentListViewModel GetDepartmentList(DataTableModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("DepartmentName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ContryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralDepartmentListModel DepartmentList = _generalDepartmentMasterDAL.GetDepartmentList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralDepartmentListViewModel listViewModel = new GeneralDepartmentListViewModel { GeneralDepartmentList = DepartmentList?.GeneralDepartmentList?.ToViewModel<GeneralDepartmentViewModel>().ToList() };
            if (listViewModel?.GeneralDepartmentList?.Count > 0)
                SetListPagingData(listViewModel.PageListViewModel, DepartmentList, dataTableModel, listViewModel.GeneralDepartmentList.Count);

            return DepartmentList?.GeneralDepartmentList?.Count > 0 ? listViewModel : new GeneralDepartmentListViewModel() { GeneralDepartmentList = new List<GeneralDepartmentViewModel>() };
        }

        //Create Department.
        public GeneralDepartmentViewModel CreateDepartment(GeneralDepartmentViewModel generalDepartmentViewModel)
        {
            try
            {
                GeneralDepartmentModel generalDepartmentModel = _generalDepartmentMasterDAL.CreateDepartment(generalDepartmentViewModel.ToModel<GeneralDepartmentModel>());
                return RARIndiaHelperUtility.IsNotNull(generalDepartmentModel) ? generalDepartmentModel.ToViewModel<GeneralDepartmentViewModel>() : new GeneralDepartmentViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalDepartmentViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalDepartmentViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Department.ToString());
                return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalDepartmentViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Department by Department id.
        public GeneralDepartmentViewModel GetDepartment(int DepartmentId)
            => _generalDepartmentMasterDAL.GetDepartment(DepartmentId).ToViewModel<GeneralDepartmentViewModel>();

        //Update Department.
        public GeneralDepartmentViewModel UpdateDepartment(GeneralDepartmentViewModel generalDepartmentViewModel)
        {
            try
            {
                GeneralDepartmentModel generalDepartmentModel = _generalDepartmentMasterDAL.UpdateDepartment(generalDepartmentViewModel.ToModel<GeneralDepartmentModel>());
                return RARIndiaHelperUtility.IsNotNull(generalDepartmentModel) ? generalDepartmentModel.ToViewModel<GeneralDepartmentViewModel>() : (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(new GeneralDepartmentListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Department.ToString());
                return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalDepartmentViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Department.
        public bool DeleteDepartment(string DepartmentIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;
            try
            {
                return _generalDepartmentMasterDAL.DeleteDepartment(new ParameterModel() { Ids = DepartmentIds });
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Department.ToString());
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        public GeneralDepartmentListModel GetDepartmentsByCentreCode(string centreCode, int departmentID = 0)
        {
            centreCode = SpiltCentreCode(centreCode);
            GeneralDepartmentListModel list = _generalDepartmentMasterDAL.GetDepartmentsByCentreCode(centreCode);
            list.SelectedDepartmentID = departmentID;
            return list;
        }

    }
}
