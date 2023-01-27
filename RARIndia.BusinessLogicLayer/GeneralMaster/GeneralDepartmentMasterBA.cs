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
        GeneralDepartmentMasterDAL _generalCountryMasterDAL = null;
        public GeneralDepartmentMasterBA()
        {
            _generalCountryMasterDAL = new GeneralDepartmentMasterDAL();
        }

        //public GeneralDepartmentListViewModel GetCountryList(DataTableModel dataTableModel)
        //{
        //    FilterCollection filters = null;
        //    if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
        //    {
        //        filters = new FilterCollection();
        //        filters.Add("CountryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
        //        filters.Add("ContryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
        //    }

        //    NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
        //    GeneralDepartmentListModel countryList = _generalCountryMasterDAL.GetCountryList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
        //    GeneralDepartmentListViewModel listViewModel = new GeneralDepartmentListViewModel { GeneralDepartmentList = countryList?.GeneralDepartmentList?.ToViewModel<GeneralDepartmentViewModel>().ToList() };
        //    if (listViewModel?.GeneralDepartmentList?.Count > 0)
        //        SetListPagingData(listViewModel.PageListViewModel, countryList, dataTableModel, listViewModel.GeneralDepartmentList.Count);

        //    return countryList?.GeneralDepartmentList?.Count > 0 ? listViewModel : new GeneralDepartmentListViewModel() { GeneralDepartmentList = new List<GeneralDepartmentViewModel>() };
        //}

        ////Create country.
        //public GeneralDepartmentViewModel CreateCountry(GeneralDepartmentViewModel generalCountryViewModel)
        //{
        //    try
        //    {
        //        GeneralDepartmentModel generalCountryModel = _generalCountryMasterDAL.CreateCountry(generalCountryViewModel.ToModel<GeneralDepartmentModel>());
        //        return RARIndiaHelperUtility.IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralDepartmentViewModel>() : new GeneralDepartmentViewModel();
        //    }
        //    catch (RARIndiaException ex)
        //    {
        //        switch (ex.ErrorCode)
        //        {
        //            case ErrorCodes.AlreadyExist:
        //                return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.AlreadyExistCode);
        //            default:
        //                return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Country.ToString());
        //        return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
        //    }
        //}

        ////Get country by country id.
        //public GeneralDepartmentViewModel GetCountry(int countryId)
        //    => _generalCountryMasterDAL.GetCountry(countryId).ToViewModel<GeneralDepartmentViewModel>();

        ////Update country.
        //public GeneralDepartmentViewModel UpdateCountry(GeneralDepartmentViewModel generalCountryViewModel)
        //{
        //    try
        //    {
        //        GeneralDepartmentModel generalCountryModel = _generalCountryMasterDAL.UpdateCountry(generalCountryViewModel.ToModel<GeneralDepartmentModel>());
        //        return RARIndiaHelperUtility.IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralDepartmentViewModel>() : (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(new GeneralDepartmentListViewModel(), GeneralResources.UpdateErrorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Country.ToString());
        //        return (GeneralDepartmentViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.UpdateErrorMessage);
        //    }
        //}

        ////Delete country.
        //public bool DeleteCountry(string countryIds, out string errorMessage)
        //{
        //    errorMessage = GeneralResources.ErrorFailedToDelete;
        //    try
        //    {
        //        return _generalCountryMasterDAL.DeleteCountry(new ParameterModel() { Ids = countryIds });
        //    }
        //    catch (RARIndiaException ex)
        //    {
        //        switch (ex.ErrorCode)
        //        {
        //            default:
        //                errorMessage = GeneralResources.ErrorFailedToDelete;
        //                return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Country.ToString());
        //        errorMessage = GeneralResources.ErrorFailedToDelete;
        //        return false;
        //    }
        //}

        public GeneralDepartmentListModel GetDepartmentsByCentreCode(string centreCode, int departmentID = 0)
        {
            centreCode = !string.IsNullOrEmpty(centreCode) && centreCode.Contains(":") ? centreCode.Split(':')[0] : centreCode;
            GeneralDepartmentListModel list = _generalCountryMasterDAL.GetDepartmentsByCentreCode(centreCode);
            list.SelectedDepartmentID = departmentID;
            return list;
        }

    }
}
