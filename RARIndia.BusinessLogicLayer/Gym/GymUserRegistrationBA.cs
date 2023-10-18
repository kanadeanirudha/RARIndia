using RARIndia.DataAccessLayer;
using RARIndia.DataAccessLayer.DataEntity;
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

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.BusinessLogicLayer
{
    public class GymUserRegistrationBA : BaseBusinessLogic
    {
        GymUserRegistrationDAL _gymUserRegistrationDAL = null;
        public GymUserRegistrationBA()
        {
            _gymUserRegistrationDAL = new GymUserRegistrationDAL();
        }

        public GymUserRegistrationListViewModel GetGymUserRegistrationList(DataTableModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ContactNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            NameValueCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GymUserRegistrationListModel gymUserRegistrationList = _gymUserRegistrationDAL.GetGymUserRegistrationList(filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GymUserRegistrationListViewModel listViewModel = new GymUserRegistrationListViewModel { GymUserRegistrationList = gymUserRegistrationList?.GymUserRegistrationList?.ToViewModel<GymUserRegistrationViewModel>().ToList() };

            SetListPagingData(listViewModel.PageListViewModel, gymUserRegistrationList, dataTableModel, listViewModel.GymUserRegistrationList.Count);

            return listViewModel;
        }

        //Create gymUserRegistration.
        public GymUserRegistrationViewModel CreateGymUserRegistration(GymUserRegistrationViewModel gymUserRegistrationViewModel)
        {
            try
            {
                gymUserRegistrationViewModel.Gender = gymUserRegistrationViewModel.GenderListId == "1" ? true : false;
                gymUserRegistrationViewModel.CreatedBy = LoginUserId();
                GymUserRegistrationModel gymUserRegistrationModel = _gymUserRegistrationDAL.CreateGymUserRegistration(gymUserRegistrationViewModel.ToModel<GymUserRegistrationModel>());
                return IsNotNull(gymUserRegistrationModel) ? gymUserRegistrationModel.ToViewModel<GymUserRegistrationViewModel>() : new GymUserRegistrationViewModel();
            }
            catch (RARIndiaException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GymUserRegistrationViewModel)GetViewModelWithErrorMessage(gymUserRegistrationViewModel, ex.ErrorMessage);
                    default:
                        return (GymUserRegistrationViewModel)GetViewModelWithErrorMessage(gymUserRegistrationViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.GymUserRegistration.ToString());
                return (GymUserRegistrationViewModel)GetViewModelWithErrorMessage(gymUserRegistrationViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get gymUserRegistration by gymUserRegistration id.
        public GymUserRegistrationViewModel GetGymUserRegistration(int gymUserRegistrationId)
            => _gymUserRegistrationDAL.GetGymUserRegistration(gymUserRegistrationId).ToViewModel<GymUserRegistrationViewModel>();

        //Update gymUserRegistration.
        public GymUserRegistrationViewModel UpdateGymUserRegistration(GymUserRegistrationViewModel gymUserRegistrationViewModel)
        {
            try
            {
                gymUserRegistrationViewModel.ModifiedBy = LoginUserId();
                GymUserRegistrationModel gymUserRegistrationModel = _gymUserRegistrationDAL.UpdateGymUserRegistration(gymUserRegistrationViewModel.ToModel<GymUserRegistrationModel>());
                return IsNotNull(gymUserRegistrationModel) ? gymUserRegistrationModel.ToViewModel<GymUserRegistrationViewModel>() : (GymUserRegistrationViewModel)GetViewModelWithErrorMessage(new GymUserRegistrationListViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.GymUserRegistration.ToString());
                return (GymUserRegistrationViewModel)GetViewModelWithErrorMessage(gymUserRegistrationViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        ////Delete gymUserRegistration.
        //public bool DeleteGymUserRegistration(string gymUserRegistrationIds, out string errorMessage)
        //{
        //    errorMessage = GeneralResources.ErrorFailedToDelete;
        //    try
        //    {
        //        return _gymUserRegistrationDAL.DeleteGymUserRegistration(new ParameterModel() { Ids = gymUserRegistrationIds });
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
        //        RARIndiaFileLogging.LogMessage(ex.Message, RARIndiaComponents.Components.GymUserRegistration.ToString());
        //        errorMessage = GeneralResources.ErrorFailedToDelete;
        //        return false;
        //    }
        //}

        public virtual List<GymPaymentTypeModel> GetAllGymPaymentTypes()
        {
            return _gymUserRegistrationDAL.GetAllGymPaymentTypes();
        }

        public virtual List<GymMembershipPlanModel> GetAllGymMembershipPlan()
        {
            return _gymUserRegistrationDAL.GetAllGymMembershipPlan();
        }

        public virtual List<GymPlanDurationModel> GetAllGymPlanDuration()
        {
            return _gymUserRegistrationDAL.GetAllGymPlanDuration();
        }
        public int GetMembershipPlanDurationAmount(int gymMembershipPlanMasterId, int gymPlanDurationId)
        {
            return _gymUserRegistrationDAL.GetMembershipPlanDurationAmount(gymMembershipPlanMasterId, gymPlanDurationId);
        }

        public GymUserRegistrationPrintModel GymPrintUserRegistration(int gymUserRegistrationId)
        {
            return _gymUserRegistrationDAL.GymPrintUserRegistration(gymUserRegistrationId);
        }
    }
}
