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
    public class OrganisationCentreMasterBA : BaseBusinessLogic
    {
        OrganisationCentreMasterDAL _organisationCentreMasterDAL = null;
        public OrganisationCentreMasterBA()
        {
            _organisationCentreMasterDAL = new OrganisationCentreMasterDAL();
        }
        public OrganisationCentreListViewModel GetOrganisationCentreList(DataTableModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CentreName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CentreCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            OrganisationCentreListModel organisationCentreList = _organisationCentreMasterDAL.GetOrganisationCentreList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            OrganisationCentreListViewModel listViewModel = new OrganisationCentreListViewModel { OrganisationCentreList = organisationCentreList?.OrganisationCentreList?.ToViewModel<OrganisationCentreViewModel>().ToList() };

            SetListPagingData(listViewModel.PageListViewModel, organisationCentreList, dataTableModel, listViewModel.OrganisationCentreList.Count);

            return listViewModel;
        }

        //Create Organisation Centre.
        public OrganisationCentreViewModel CreateOrganisationCentre(OrganisationCentreViewModel organisationCentreViewModel)
        {
            try
            {
                organisationCentreViewModel.CreatedBy = LoginUserId();
                OrganisationCentreModel organisationCentreModel = _organisationCentreMasterDAL.CreateOrganisationCentre(organisationCentreViewModel.ToModel<OrganisationCentreModel>());
                return IsNotNull(organisationCentreModel) ? organisationCentreModel.ToViewModel<OrganisationCentreViewModel>() : new OrganisationCentreViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (OrganisationCentreViewModel)GetViewModelWithErrorMessage(organisationCentreViewModel, ex.ErrorMessage);
                    default:
                        return (OrganisationCentreViewModel)GetViewModelWithErrorMessage(organisationCentreViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.OrganisationCentre.ToString());
                return (OrganisationCentreViewModel)GetViewModelWithErrorMessage(organisationCentreViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Organisation Centre by organisationCentreId.
        public OrganisationCentreViewModel GetOrganisationCentre(int organisationCentreId)
            => _organisationCentreMasterDAL.GetOrganisationCentre(organisationCentreId).ToViewModel<OrganisationCentreViewModel>();

        //Update Organisation Centre.
        public OrganisationCentreViewModel UpdateOrganisationCentre(OrganisationCentreViewModel organisationCentreViewModel)
        {
            try
            {
                organisationCentreViewModel.ModifiedBy = LoginUserId();
                OrganisationCentreModel organisationCentreModel = _organisationCentreMasterDAL.UpdateOrganisationCentre(organisationCentreViewModel.ToModel<OrganisationCentreModel>());
                return IsNotNull(organisationCentreModel) ? organisationCentreModel.ToViewModel<OrganisationCentreViewModel>() : (OrganisationCentreViewModel)GetViewModelWithErrorMessage(new OrganisationCentreListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.OrganisationCentre.ToString());
                return (OrganisationCentreViewModel)GetViewModelWithErrorMessage(organisationCentreViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Organisation Centre.
        public bool DeleteCentre(string organisationCentreId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;
            try
            {
                return _organisationCentreMasterDAL.DeleteCentre(new ParameterModel() { Ids = organisationCentreId });
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
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.OrganisationCentre.ToString());
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        //Create Organisation Printing Format CentreCode.
        public OrganisationCentrePrintingFormatViewModel GetPrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        {
            try
            {
                organisationCentrePrintingFormatViewModel.CreatedBy = LoginUserId();
                OrganisationCentrePrintingFormatModel organisationCentrePrintingFormatModel = _organisationCentreMasterDAL.GetPrintingFormat(organisationCentrePrintingFormatViewModel.ToModel<OrganisationCentrePrintingFormatModel>());
                return IsNotNull(organisationCentrePrintingFormatModel) ? organisationCentrePrintingFormatModel.ToViewModel<OrganisationCentrePrintingFormatViewModel>() : new OrganisationCentrePrintingFormatViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (OrganisationCentrePrintingFormatViewModel)GetViewModelWithErrorMessage(organisationCentrePrintingFormatViewModel, ex.ErrorMessage);
                    default:
                        return (OrganisationCentrePrintingFormatViewModel)GetViewModelWithErrorMessage(organisationCentrePrintingFormatViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.OrganisationCentrePrintingFormat.ToString());
                return (OrganisationCentrePrintingFormatViewModel)GetViewModelWithErrorMessage(organisationCentrePrintingFormatViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Create Organisation Centre.
        //public OrganisationCentrePrintingFormatViewModel GetOrganisationPrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        //{
        //    try
        //    {
        //        organisationCentrePrintingFormatViewModel.CreatedBy = LoginUserId();
        //        OrganisationCentrePrintingFormatModel organisationCentrePrintingFormatModel = _organisationCentreMasterDAL.GetOrganisationPrintingFormat(organisationCentrePrintingFormatViewModel.ToModel<OrganisationCentrePrintingFormatModel>());
        //        return IsNotNull(organisationCentrePrintingFormatModel) ? organisationCentrePrintingFormatModel.ToViewModel<OrganisationCentrePrintingFormatViewModel>() : new OrganisationCentrePrintingFormatViewModel();
        //    }
        //    catch (RARIndiaException ex)
        //    {
        //        switch (ex.ErrorCode)
        //        {
        //            case ErrorCodes.AlreadyExist:
        //                return (OrganisationCentrePrintingFormatViewModel)GetViewModelWithErrorMessage(organisationCentrePrintingFormatViewModel, ex.ErrorMessage);
        //            default:
        //                return (OrganisationCentrePrintingFormatViewModel)GetViewModelWithErrorMessage(organisationCentrePrintingFormatViewModel, GeneralResources.ErrorFailedToCreate);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.OrganisationCentrePrintingFormat.ToString());
        //        return (OrganisationCentrePrintingFormatViewModel)GetViewModelWithErrorMessage(organisationCentrePrintingFormatViewModel, GeneralResources.ErrorFailedToCreate);
        //    }
        //}
    }
}












