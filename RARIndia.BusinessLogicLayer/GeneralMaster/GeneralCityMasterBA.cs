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
    public class GeneralCityMasterBA : BaseBusinessLogic
    {
        GeneralCityMasterDAL _generalCityMasterDAL = null;
        public GeneralCityMasterBA()
        {
            _generalCityMasterDAL = new GeneralCityMasterDAL();
        }

        public GeneralCityListViewModel GetCityList(DataTableModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CityName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("RegionName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralCityListModel cityList = _generalCityMasterDAL.GetCityList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralCityListViewModel listViewModel = new GeneralCityListViewModel { GeneralCityList = cityList?.GeneralCityList?.ToViewModel<GeneralCityViewModel>().ToList() };
            
            SetListPagingData(listViewModel.PageListViewModel, cityList, dataTableModel, listViewModel.GeneralCityList.Count);

            return listViewModel;
        }

        //Create city.
        public GeneralCityViewModel CreateCity(GeneralCityViewModel generalCityViewModel)
        {
            try
            {
                generalCityViewModel.CreatedBy = LoginUserId();
                GeneralCityModel generalCityModel = _generalCityMasterDAL.CreateCity(generalCityViewModel.ToModel<GeneralCityModel>());
                return IsNotNull(generalCityModel) ? generalCityModel.ToViewModel<GeneralCityViewModel>() : new GeneralCityViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralCityViewModel)GetViewModelWithErrorMessage(generalCityViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralCityViewModel)GetViewModelWithErrorMessage(generalCityViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.City.ToString());
                return (GeneralCityViewModel)GetViewModelWithErrorMessage(generalCityViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get city by city id.
        public GeneralCityViewModel GetCity(int cityId)
            => _generalCityMasterDAL.GetCity(cityId).ToViewModel<GeneralCityViewModel>();

        //Update city.
        public GeneralCityViewModel UpdateCity(GeneralCityViewModel generalCityViewModel)
        {
            try
            {
                generalCityViewModel.ModifiedBy = LoginUserId();
                GeneralCityModel generalCityModel = _generalCityMasterDAL.UpdateCity(generalCityViewModel.ToModel<GeneralCityModel>());
                return IsNotNull(generalCityModel) ? generalCityModel.ToViewModel<GeneralCityViewModel>() : (GeneralCityViewModel)GetViewModelWithErrorMessage(new GeneralCityListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.City.ToString());
                return (GeneralCityViewModel)GetViewModelWithErrorMessage(generalCityViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete city.
        public bool DeleteCity(string cityIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;
            try
            {
                return _generalCityMasterDAL.DeleteCity(new ParameterModel() { Ids = cityIds });
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
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.City.ToString());
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

    }
}
