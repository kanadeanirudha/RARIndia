using System;
using System.ComponentModel.DataAnnotations;
namespace RARIndia.ViewModel
{
    public class OrganisationCentreViewModel : BaseViewModel
    {
        public short OrganisationCentreMasterId { get; set; }
        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }
        [Display(Name = "Centre Name")]
        [Required]
        public string CentreName { get; set; }
        public string HoCoRoScFlag { get; set; }
        public int? HoId { get; set; }
        public int? CoId { get; set; }
        public int? RoId { get; set; }
        [Display(Name = "Centre Specialization")]
        public string CentreSpecialization { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string CentreAddress { get; set; }
        [Display(Name = "City")]
        public int GeneralCityMasterId { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Display(Name = "Email Address")]
        [Required]
        public string EmailId { get; set; }
        public string Url { get; set; }
        [Display(Name = "Mobile Number")]
        [Required]
        public string CellPhone { get; set; }
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }
        [Display(Name = "Phone Number Office")]
        public string PhoneNumberOffice { get; set; }
        [Display(Name = "Establishment Date")]
        public DateTime? CentreEstablishmentDatetime { get; set; }
        [Display(Name = "Organisation Name")]
        [Required]
        public byte OrganisationId { get; set; }
        [Display(Name = "Centre Login Number")]
        public int? CentreLoginNumber { get; set; }
        [Display(Name = "Institute Code")]
        public string InstituteCode { get; set; }
        public string TimeZone { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        [Display(Name = "Campus Area")]
        public double? CampusArea { get; set; }
        [Display(Name = "CIN Number")]
        public string CINNumber { get; set; }
        [MaxLength(15)]
        [MinLength(15)]
        [Display(Name = "GSTIN Number")]
        public string GSTINNumber { get; set; }
        [MaxLength(15)]
        [MinLength(15)]
        [Display(Name = "Pan Number ")]
        public string PanNumber { get; set; }
        [Display(Name = "PF Number ")]
        public string PFNumber { get; set; }
        [Display(Name = "ESIC Number ")]
        public string ESICNumber { get; set; }
        public string WaterMark { get; set; }
        [Display(Name = "Office Type")]
        public string OfficeType { get; set; }
        [Display(Name = "Office Belong To ")]
        public string OfficeBelongTo { get; set; }
    }
}