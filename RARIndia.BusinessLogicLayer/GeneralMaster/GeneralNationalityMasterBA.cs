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
    public class GeneralNationalityMasterBA : BaseBusinessLogic
    {
        GeneralNationalityMasterDAL _generalNationalityMasterDAL = null;
        public GeneralNationalityMasterBA()
        {
            _generalNationalityMasterDAL = new GeneralNationalityMasterDAL();
        }

        public GeneralNationalityListViewModel GetNationalityList(DataTableModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CountryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ContryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralNationalityListModel nationalityList = _generalNationalityMasterDAL.GetNationalityList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralNationalityListViewModel listViewModel = new GeneralNationalityListViewModel { GeneralNationalityList = nationalityList?.GeneralNationalityList?.ToViewModel<GeneralNationalityViewModel>().ToList() };
            SetListPagingData(listViewModel.PageListViewModel, nationalityList, dataTableModel, listViewModel.GeneralNationalityList.Count);

            return listViewModel;
        }

        ////Create country.
        //public GeneralCountryViewModel CreateCountry(GeneralCountryViewModel generalCountryViewModel)
        //{
        //    try
        //    {
        //        GeneralCountryModel generalCountryModel = _generalCountryMasterDAL.CreateCountry(generalCountryViewModel.ToModel<GeneralCountryModel>());
        //        return RARIndiaHelperUtility.IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralCountryViewModel>() : new GeneralCountryViewModel();
        //    }
        //    catch (RARIndiaException ex)
        //    {
        //        switch (ex.ErrorCode)
        //        {
        //            case ErrorCodes.AlreadyExist:
        //                return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, ex.ErrorMessage);
        //            default:
        //                return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Country.ToString());
        //        return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
        //    }
        //}

        ////Get country by country id.
        //public GeneralCountryViewModel GetCountry(int countryId)
        //    => _generalCountryMasterDAL.GetCountry(countryId).ToViewModel<GeneralCountryViewModel>();

        ////Update country.
        //public GeneralCountryViewModel UpdateCountry(GeneralCountryViewModel generalCountryViewModel)
        //{
        //    try
        //    {
        //        GeneralCountryModel generalCountryModel = _generalCountryMasterDAL.UpdateCountry(generalCountryViewModel.ToModel<GeneralCountryModel>());
        //        return RARIndiaHelperUtility.IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralCountryViewModel>() : (GeneralCountryViewModel)GetViewModelWithErrorMessage(new GeneralCountryListViewModel(), GeneralResources.UpdateErrorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Country.ToString());
        //        return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.UpdateErrorMessage);
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

    }
}
