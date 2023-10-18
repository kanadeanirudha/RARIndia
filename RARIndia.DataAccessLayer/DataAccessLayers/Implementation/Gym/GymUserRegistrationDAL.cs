using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Helper;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Helper;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.DataAccessLayer
{
    public class GymUserRegistrationDAL
    {
        private readonly IRARIndiaRepository<GymUserRegistration> _gymUserRegistrationRepository;
        public GymUserRegistrationDAL()
        {
            _gymUserRegistrationRepository = new RARIndiaRepository<GymUserRegistration>();
        }

        public GymUserRegistrationListModel GetGymUserRegistrationList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<GymUserRegistrationModel> objStoredProc = new RARIndiaViewRepository<GymUserRegistrationModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymUserRegistrationModel> gymUserRegistrationList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetGymUserRegistrationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GymUserRegistrationListModel listModel = new GymUserRegistrationListModel();

            listModel.GymUserRegistrationList = gymUserRegistrationList?.Count > 0 ? gymUserRegistrationList : new List<GymUserRegistrationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create gymUserRegistration.
        public GymUserRegistrationModel CreateGymUserRegistration(GymUserRegistrationModel gymUserRegistrationModel)
        {
            if (IsNull(gymUserRegistrationModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsCodeAlreadyExist(gymUserRegistrationModel.ContactNumber))
            //{
            //    throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "GymUserRegistration code"));
            //}
            GymUserRegistration gymUserRegistration = gymUserRegistrationModel.FromModelToEntity<GymUserRegistration>();
            //Create new gymUserRegistration and return it.
            GymUserRegistration gymUserRegistrationData = _gymUserRegistrationRepository.Insert(gymUserRegistration);
            if (gymUserRegistrationData?.GymUserRegistrationId > 0)
            {
                gymUserRegistrationModel.GymUserRegistrationId = gymUserRegistrationData.GymUserRegistrationId;
                int amountPaid = GetMembershipPlanDurationAmount(gymUserRegistrationModel.GymMembershipPlanMasterId, gymUserRegistrationModel.GymPlanDurationMasterId);

                GymUserTransactionDetail gymUserTransactionDetails = new GymUserTransactionDetail()
                {
                    GymUserRegistrationId = gymUserRegistrationData.GymUserRegistrationId,
                    GymMembershipPlanMasterId = gymUserRegistrationModel.GymMembershipPlanMasterId,
                    GymPlanDurationMasterId = gymUserRegistrationModel.GymPlanDurationMasterId,
                    AmountPaid = amountPaid,
                    PreLaunchSpecialOfferAmount = gymUserRegistrationModel.PreLaunchSpecialOfferAmount,
                    TotalAmountPaid = amountPaid - gymUserRegistrationModel.PreLaunchSpecialOfferAmount,
                    TransactionDate = DateTime.Now,
                    PaymentTypeMasterId = gymUserRegistrationModel.PaymentTypeMasterId,
                    TransactionReference = gymUserRegistrationModel.TransactionReference,
                    CreatedBy = gymUserRegistrationModel.CreatedBy,
                    ModifiedBy = gymUserRegistrationModel.ModifiedBy,
                };
                new RARIndiaRepository<GymUserTransactionDetail>().Insert(gymUserTransactionDetails);
            }
            else
            {
                gymUserRegistrationModel.HasError = true;
                gymUserRegistrationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return gymUserRegistrationModel;
        }

        //Get gymUserRegistration by gymUserRegistration id.
        public GymUserRegistrationModel GetGymUserRegistration(int gymUserRegistrationId)
        {
            if (gymUserRegistrationId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymUserRegistrationID"));

            //Get the gymUserRegistration Details based on id.
            GymUserRegistration gymUserRegistrationData = _gymUserRegistrationRepository.Table.FirstOrDefault(x => x.GymUserRegistrationId == gymUserRegistrationId);
            GymUserRegistrationModel gymUserRegistrationModel = gymUserRegistrationData.FromEntityToModel<GymUserRegistrationModel>();
            return gymUserRegistrationModel;
        }

        //Update gymUserRegistration.
        public GymUserRegistrationModel UpdateGymUserRegistration(GymUserRegistrationModel gymUserRegistrationModel)
        {
            bool isGymUserRegistrationUpdated = false;
            if (IsNull(gymUserRegistrationModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (gymUserRegistrationModel.GymUserRegistrationId < 1)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymUserRegistrationID"));

            //Update gymUserRegistration
            isGymUserRegistrationUpdated = _gymUserRegistrationRepository.Update(gymUserRegistrationModel.FromModelToEntity<GymUserRegistration>());
            if (!isGymUserRegistrationUpdated)
            {
                gymUserRegistrationModel.HasError = true;
                gymUserRegistrationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return gymUserRegistrationModel;
        }

        ////Delete gymUserRegistration.
        //public bool DeleteGymUserRegistration(ParameterModel parameterModel)
        //{
        //    if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
        //        throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymUserRegistrationID"));

        //    RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
        //    objStoredProc.SetParameter(RARIndiaGymUserRegistrationEnum.GymUserRegistrationId.ToString(), parameterModel.Ids, ParameterDirection.Input, DbType.String);
        //    objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
        //    int status = 0;
        //    objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteGymUserRegistration @GymUserRegistrationId,  @Status OUT", 1, out status);

        //    return status == 1 ? true : false;
        //}

        public virtual List<GymPaymentTypeModel> GetAllGymPaymentTypes()
        {
            List<GymPaymentTypeModel> paymentTypeList = new List<GymPaymentTypeModel>();
            List<GymPaymentTypeMaster> data = new RARIndiaRepository<GymPaymentTypeMaster>().Table?.ToList();
            foreach (GymPaymentTypeMaster model in data)
            {
                paymentTypeList.Add(new GymPaymentTypeModel()
                {
                    PaymentTypeMasterId = model.PaymentTypeMasterId,
                    PaymentType = model.PaymentType
                });
            }
            return paymentTypeList;
        }

        public virtual List<GymMembershipPlanModel> GetAllGymMembershipPlan()
        {
            List<GymMembershipPlanModel> paymentTypeList = new List<GymMembershipPlanModel>();
            List<GymMembershipPlanMaster> data = new RARIndiaRepository<GymMembershipPlanMaster>().Table.Where(x => x.IsActive)?.ToList();
            foreach (GymMembershipPlanMaster model in data)
            {
                paymentTypeList.Add(new GymMembershipPlanModel()
                {
                    GymMembershipPlanMasterId = model.GymMembershipPlanMasterId,
                    MembershipPlanName = model.MembershipPlanName
                });
            }
            return paymentTypeList;
        }

        public virtual List<GymPlanDurationModel> GetAllGymPlanDuration()
        {
            List<GymPlanDurationModel> paymentTypeList = new List<GymPlanDurationModel>();
            List<GymPlanDurationMaster> data = new RARIndiaRepository<GymPlanDurationMaster>().Table.Where(x => x.IsActive)?.ToList();
            foreach (GymPlanDurationMaster model in data)
            {
                paymentTypeList.Add(new GymPlanDurationModel()
                {
                    GymPlanDurationMasterId = model.GymPlanDurationMasterId,
                    PlanDuration = model.PlanDuration
                });
            }
            return paymentTypeList;
        }

        public int GetMembershipPlanDurationAmount(int gymMembershipPlanMasterId, int gymPlanDurationId)
        {
            int amount = 0;
            if (gymMembershipPlanMasterId > 0 && gymPlanDurationId > 0)
                amount = Convert.ToInt32(new RARIndiaRepository<GymMembershipPlanDurationDetail>().Table.FirstOrDefault(x => x.GymMembershipPlanMasterId == gymMembershipPlanMasterId && x.GymPlanDurationMasterId == gymPlanDurationId)?.Amount);
            return amount;
        }

        public GymUserRegistrationPrintModel GymPrintUserRegistration(int gymUserRegistrationId)
        {
            GymUserRegistration gymUserRegistrationData = _gymUserRegistrationRepository.Table.FirstOrDefault(x => x.GymUserRegistrationId == gymUserRegistrationId);
            GymUserTransactionDetail gymUserTransactionDetail = new RARIndiaRepository<GymUserTransactionDetail>().Table.FirstOrDefault(x => x.GymUserRegistrationId == gymUserRegistrationId);
            UserMaster userMaster = new RARIndiaRepository<UserMaster>().Table.FirstOrDefault(x => x.UserMasterId == gymUserTransactionDetail.CreatedBy);
            string recieptPreparedBy = $"{userMaster.FirstName} {userMaster.LastName}";

            GymUserRegistrationPrintModel gymUserRegistrationPrintModel = new GymUserRegistrationPrintModel()
            {
                FullName = $"{gymUserRegistrationData.FirstName} {gymUserRegistrationData.LastName}",
                ContactNumber = gymUserRegistrationData.ContactNumber,
                MembershipPlanName = new RARIndiaRepository<GymMembershipPlanMaster>().Table.FirstOrDefault(x => x.GymMembershipPlanMasterId == gymUserTransactionDetail.GymMembershipPlanMasterId)?.MembershipPlanName,
                PlanDuration = new RARIndiaRepository<GymPlanDurationMaster>().Table.FirstOrDefault(x => x.GymPlanDurationMasterId == gymUserTransactionDetail.GymPlanDurationMasterId)?.PlanDuration,
                AmountPaid = Convert.ToInt32(gymUserTransactionDetail.AmountPaid),
                PreLaunchSpecialOfferAmount = Convert.ToInt32(gymUserTransactionDetail.PreLaunchSpecialOfferAmount),
                TotalAmountPaid = Convert.ToInt32(gymUserTransactionDetail.TotalAmountPaid),
                TransactionDate = gymUserTransactionDetail.TransactionDate.ToString("dd-MMM-yyyy"),
                RecieptPreparedBy = recieptPreparedBy
            };
            return gymUserRegistrationPrintModel;
        }
        #region Private Method

        ////Check if gymUserRegistration code is already present or not.
        //private bool IsCodeAlreadyExist(string contactNumber)
        // => _gymUserRegistrationRepository.Table.Any(x => x.AadhaarCardNumber == contactNumber);
        #endregion
    }
}
