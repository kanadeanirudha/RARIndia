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
    public class GeneralTaxGroupMasterBA : BaseBusinessLogic
    {
        GeneralTaxGroupMasterDAL _generalTaxGroupMasterDAL = null;
        public GeneralTaxGroupMasterBA()
        {
            _generalTaxGroupMasterDAL = new GeneralTaxGroupMasterDAL();
        }

        public GeneralTaxGroupMasterListViewModel GetTaxGroupMasterList(DataTableModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TaxGroupName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TaxGroupRate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralTaxGroupMasterListModel taxGroupMasterList = _generalTaxGroupMasterDAL.GetTaxGroupMasterList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralTaxGroupMasterListViewModel listViewModel = new GeneralTaxGroupMasterListViewModel { GeneralTaxGroupMasterList = taxGroupMasterList?.GeneralTaxGroupMasterList?.ToViewModel<GeneralTaxGroupMasterViewModel>().ToList() };
            SetListPagingData(listViewModel.PageListViewModel, taxGroupMasterList, dataTableModel, listViewModel.GeneralTaxGroupMasterList.Count);
            return listViewModel;
        }

        //Create Tax Group Master.
        public GeneralTaxGroupMasterViewModel CreateTaxGroupMaster(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel)
        {
            try
            {
                generalTaxGroupMasterViewModel.CreatedBy = LoginUserId();
                GeneralTaxGroupMasterModel generalTaxGroupMasterModel = _generalTaxGroupMasterDAL.CreateTaxGroupMaster(generalTaxGroupMasterViewModel.ToModel<GeneralTaxGroupMasterModel>());
                return IsNotNull(generalTaxGroupMasterModel) ? generalTaxGroupMasterModel.ToViewModel<GeneralTaxGroupMasterViewModel>() : new GeneralTaxGroupMasterViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(generalTaxGroupMasterViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(generalTaxGroupMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.TaxGroupMaster.ToString());
                return (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(generalTaxGroupMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        // Get Tax Group Master by GeneralTaxGroupMasterId.
        public GeneralTaxGroupMasterViewModel GetTaxGroupMaster(int taxGroupMasterId)
          => _generalTaxGroupMasterDAL.GetTaxGroupMaster(taxGroupMasterId).ToViewModel<GeneralTaxGroupMasterViewModel>();

        //Update Tax Group Master.
        public GeneralTaxGroupMasterViewModel UpdateTaxGroupMaster(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel)
        {
            try
            {
                generalTaxGroupMasterViewModel.ModifiedBy = LoginUserId();
                GeneralTaxGroupMasterModel generalTaxGroupMasterModel = _generalTaxGroupMasterDAL.UpdateTaxGroupMaster(generalTaxGroupMasterViewModel.ToModel<GeneralTaxGroupMasterModel>());
                return IsNotNull(generalTaxGroupMasterModel) ? generalTaxGroupMasterModel.ToViewModel<GeneralTaxGroupMasterViewModel>() : (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(new GeneralTaxGroupMasterListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.TaxGroupMaster.ToString());
                return (GeneralTaxGroupMasterViewModel)GetViewModelWithErrorMessage(generalTaxGroupMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Tax Group Master.
        public bool DeleteTaxGroupMaster(string taxGroupMasterIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;
            try
            {
                return _generalTaxGroupMasterDAL.DeleteTaxGroupMaster(new ParameterModel() { Ids = taxGroupMasterIds });
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
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.TaxGroupMaster.ToString());
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
    }
}






