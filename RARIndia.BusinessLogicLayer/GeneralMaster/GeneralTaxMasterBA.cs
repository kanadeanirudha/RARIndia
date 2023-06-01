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
    public class GeneralTaxMasterBA : BaseBusinessLogic
    {
        GeneralTaxMasterDAL _generalTaxMasterDAL = null;
        public GeneralTaxMasterBA()
        {
            _generalTaxMasterDAL = new GeneralTaxMasterDAL();
        }

        public GeneralTaxMasterListViewModel GetTaxMasterList(DataTableModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("TaxName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TaxRate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralTaxMasterListModel TaxMasterList = _generalTaxMasterDAL.GetTaxMasterList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralTaxMasterListViewModel listViewModel = new GeneralTaxMasterListViewModel { GeneralTaxMasterList = TaxMasterList?.GeneralTaxMasterList?.ToViewModel<GeneralTaxMasterViewModel>().ToList() };
            SetListPagingData(listViewModel.PageListViewModel, TaxMasterList, dataTableModel, listViewModel.GeneralTaxMasterList.Count);
            return listViewModel;
        }

        //Create Tax Master.
        public GeneralTaxMasterViewModel CreateTaxMaster(GeneralTaxMasterViewModel generalTaxMasterViewModel)
        {
            try
            {
                generalTaxMasterViewModel.CreatedBy = LoginUserId();
                GeneralTaxMasterModel generalTaxMasterModel = _generalTaxMasterDAL.CreateTaxMaster(generalTaxMasterViewModel.ToModel<GeneralTaxMasterModel>());
                return IsNotNull(generalTaxMasterModel) ? generalTaxMasterModel.ToViewModel<GeneralTaxMasterViewModel>() : new GeneralTaxMasterViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(generalTaxMasterViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(generalTaxMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.TaxMaster.ToString());
                return (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(generalTaxMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        // Get Tax Master by GeneralTaxMasterId.
        public GeneralTaxMasterViewModel GetTaxMaster(int TaxMasterId)
          => _generalTaxMasterDAL.GetTaxMaster(TaxMasterId).ToViewModel<GeneralTaxMasterViewModel>();

        //Update Tax Master.
        public GeneralTaxMasterViewModel UpdateTaxMaster(GeneralTaxMasterViewModel generalTaxMasterViewModel)
        {
            try
            {
                generalTaxMasterViewModel.ModifiedBy = LoginUserId();
                GeneralTaxMasterModel generalTaxMasterModel = _generalTaxMasterDAL.UpdateTaxMaster(generalTaxMasterViewModel.ToModel<GeneralTaxMasterModel>());
                return IsNotNull(generalTaxMasterModel) ? generalTaxMasterModel.ToViewModel<GeneralTaxMasterViewModel>() : (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(new GeneralTaxMasterListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.TaxMaster.ToString());
                return (GeneralTaxMasterViewModel)GetViewModelWithErrorMessage(generalTaxMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Tax Master.
        public bool DeleteTaxMaster(string TaxMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;
            try
            {
                return _generalTaxMasterDAL.DeleteTaxMaster(new ParameterModel() { Ids = TaxMasterId });
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
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.TaxMaster.ToString());
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
    }
}
