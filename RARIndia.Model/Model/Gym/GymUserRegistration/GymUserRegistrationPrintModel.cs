namespace RARIndia.Model
{
    public class GymUserRegistrationPrintModel : BaseModel
    {
        public int GymUserRegistrationId { get; set; }
        public string FullName { get; set; }
        public string MembershipPlanName { get; set; }
        public string PlanDuration { get; set; }
        public int AmountPaid { get; set; }
        public int PreLaunchSpecialOfferAmount { get; set; }
        public int TotalAmountPaid { get; set; }
        public string TransactionDate { get; set; }
        public string ContactNumber { get; set; }
        public string RecieptPreparedBy { get; set; }
    }
}
