using RARIndia.DataAccessLayer;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace RARIndia.BusinessLogicLayer
{
    public class GeneralCountryMasterBA : BaseBusinessLogic
    {
        GeneralCountryMasterDAL _generalCountryMasterDAL = null;
        public GeneralCountryMasterBA()
        {
            _generalCountryMasterDAL = new GeneralCountryMasterDAL();
        }

        public GeneralCountryListViewModel GetCountryList(FilterCollection filters, string sort, string sortBy, int pageIndex, int pageSize)
        {
            NameValueCollection sortlist = SortingData(sort, sortBy);

            GeneralCountryListModel countryList = _generalCountryMasterDAL.GetCountryList(filters, sortlist, pageIndex, pageSize);
            GeneralCountryListViewModel listViewModel = new GeneralCountryListViewModel { GeneralCountryList = countryList?.GeneralCountryList?.ToViewModel<GeneralCountryViewModel>().ToList() };
            SetListPagingData(listViewModel, countryList);

            return countryList?.GeneralCountryList?.Count > 0 ? listViewModel : new GeneralCountryListViewModel() { GeneralCountryList = new List<GeneralCountryViewModel>() };
        }

        //Create country.
        public GeneralCountryViewModel CreateCountry(GeneralCountryViewModel generalCountryViewModel)
        {
            try
            {
                GeneralCountryModel generalCountryModel = _generalCountryMasterDAL.CreateCountry(generalCountryViewModel.ToModel<GeneralCountryModel>());
                return RARIndiaHelperUtility.IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralCountryViewModel>() : new GeneralCountryViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.AlreadyExistCode);
                    default:
                        return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Country.ToString());
                return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get country by country id.
        public GeneralCountryViewModel GetCountry(int countryId)
            => _generalCountryMasterDAL.GetCountry(countryId).ToViewModel<GeneralCountryViewModel>();

        //Update country.
        public GeneralCountryViewModel UpdateCountry(GeneralCountryViewModel generalCountryViewModel)
        {
            try
            {
                GeneralCountryModel generalCountryModel = _generalCountryMasterDAL.UpdateCountry(generalCountryViewModel.ToModel<GeneralCountryModel>());
                return RARIndiaHelperUtility.IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralCountryViewModel>() : (GeneralCountryViewModel)GetViewModelWithErrorMessage(new GeneralCountryListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Country.ToString());
                return (GeneralCountryViewModel)GetViewModelWithErrorMessage(generalCountryViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete country.
        public bool DeleteCountry(string countryIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;
            try
            {
                return _generalCountryMasterDAL.DeleteCountry(new ParameterModel() { Ids = countryIds });
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
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Country.ToString());
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

    }
}
