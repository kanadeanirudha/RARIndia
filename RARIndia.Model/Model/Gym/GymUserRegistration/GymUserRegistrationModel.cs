namespace RARIndia.Model
{
    public class GymUserRegistrationModel : BaseModel
    {
        public int GymUserRegistrationId { get; set; }
        public string RegistrationCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AadhaarCardNumber { get; set; }
        public bool Gender { get; set; }
        public short Age { get; set; }
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string PastInjuries { get; set; }
        public string MedicalHistory { get; set; }
        public bool PreLaunchOrSpecialOffer { get; set; }
        public short GymMembershipPlanMasterId { get; set; }
        public short GymPlanDurationMasterId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal PreLaunchSpecialOfferAmount { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public byte PaymentTypeMasterId { get; set; }
        public string TransactionReference { get; set; }
        public string PlanDuration { get; set; }
    }
}
