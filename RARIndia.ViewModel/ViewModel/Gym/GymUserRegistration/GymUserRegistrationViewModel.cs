using RARIndia.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class GymUserRegistrationViewModel : BaseViewModel
    {
        public GymUserRegistrationViewModel()
        {
            AllPaymentTypeList = new List<GymPaymentTypeModel>();
        }
        public int GymUserRegistrationId { get; set; }
        [Required(ErrorMessage = "Registration Code is required")]
        [Display(Name = "Registration Code")]
        public string RegistrationCode { get; set; } = Guid.NewGuid().ToString();

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Display(Name = "Aadhaar Card Number")]
        public string AadhaarCardNumber { get; set; }

        public bool Gender { get; set; }
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please select Gender")]
        public string GenderListId { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age is required")]
        public byte Age { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact Number is required")]
        public string ContactNumber { get; set; }

        [Display(Name = "Email Id")]
        [EmailAddress]
        public string EmailId { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Past Injuries")]
        public string PastInjuries { get; set; }

        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; }

        [Display(Name = "Pre Launch/special offer")]
        public bool PreLaunchOrSpecialOffer { get; set; }

        [Display(Name = "Membership Plan")]
        public string MembershipPlanName { get; set; }

        [Display(Name = "Membership Plan")]
        [Required(ErrorMessage = "Please select Membership Plan")]
        public short GymMembershipPlanMasterId { get; set; }

        [Display(Name = "Duration")]
        [Required(ErrorMessage = "Please select Duration")]
        public short GymPlanDurationMasterId { get; set; }

        [Display(Name = "Pre Launch/special offer Amount")]
        public decimal PreLaunchSpecialOfferAmount { get; set; }

        [Display(Name = "Mode Of Transaction")]
        [Required(ErrorMessage = "Please select Mode Of Transaction")]
        public short PaymentTypeMasterId { get; set; }
        public List<GymPaymentTypeModel> AllPaymentTypeList { get; set; }
        [Display(Name = "Membership Plan Amount")]
        public string MembershipPlanAmount { get; set; }
        [Display(Name = "Transaction Reference")]
        public string TransactionReference { get; set; }
    }
}
