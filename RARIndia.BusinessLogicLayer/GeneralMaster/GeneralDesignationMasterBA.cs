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
    public class GeneralDesignationMasterBA : BaseBusinessLogic
    {
        GeneralDesignationMasterDAL _generalDesignationMasterDAL = null;
        public GeneralDesignationMasterBA()
        {
            _generalDesignationMasterDAL = new GeneralDesignationMasterDAL();
        }

        public GeneralDesignationListViewModel GetDesignationList(DataTableModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("Description", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ShortCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralDesignationListModel DesignationList = _generalDesignationMasterDAL.GetDesignationList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralDesignationListViewModel listViewModel = new GeneralDesignationListViewModel { GeneralDesignationList = DesignationList?.GeneralDesignationList?.ToViewModel<GeneralDesignationViewModel>().ToList() };
            
            SetListPagingData(listViewModel.PageListViewModel, DesignationList, dataTableModel, listViewModel.GeneralDesignationList.Count);

            return listViewModel;
        }

        //Create Designation.
        public GeneralDesignationViewModel CreateDesignation(GeneralDesignationViewModel generalDesignationViewModel)
        {
            try
            {
                generalDesignationViewModel.CreatedBy = LoginUserId();
                GeneralDesignationModel generalDesignationModel = _generalDesignationMasterDAL.CreateDesignation(generalDesignationViewModel.ToModel<GeneralDesignationModel>());
                return IsNotNull(generalDesignationModel) ? generalDesignationModel.ToViewModel<GeneralDesignationViewModel>() : new GeneralDesignationViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralDesignationViewModel)GetViewModelWithErrorMessage(generalDesignationViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralDesignationViewModel)GetViewModelWithErrorMessage(generalDesignationViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Designation.ToString());
                return (GeneralDesignationViewModel)GetViewModelWithErrorMessage(generalDesignationViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Designation by Designation id.
        public GeneralDesignationViewModel GetDesignation(int DesignationId)
            => _generalDesignationMasterDAL.GetDesignation(DesignationId).ToViewModel<GeneralDesignationViewModel>();

        //Update Designation.
        public GeneralDesignationViewModel UpdateDesignation(GeneralDesignationViewModel generalDesignationViewModel)
        {
            try
            {
                generalDesignationViewModel.ModifiedBy = LoginUserId();
                GeneralDesignationModel generalDesignationModel = _generalDesignationMasterDAL.UpdateDesignation(generalDesignationViewModel.ToModel<GeneralDesignationModel>());
                return IsNotNull(generalDesignationModel) ? generalDesignationModel.ToViewModel<GeneralDesignationViewModel>() : (GeneralDesignationViewModel)GetViewModelWithErrorMessage(new GeneralDesignationListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Designation.ToString());
                return (GeneralDesignationViewModel)GetViewModelWithErrorMessage(generalDesignationViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Designation.
        public bool DeleteDesignation(string DesignationIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;
            try
            {
                return _generalDesignationMasterDAL.DeleteDesignation(new ParameterModel() { Ids = DesignationIds });
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
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.Designation.ToString());
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        public GeneralDesignationListModel GetDesignations(int designationID)
        {
            GeneralDesignationListModel list = _generalDesignationMasterDAL.GetDesignations();
            list.SelectedDesignationID = designationID;
            return list;
        }
    }
}
