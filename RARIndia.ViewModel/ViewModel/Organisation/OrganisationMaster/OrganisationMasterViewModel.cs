using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class OrganisationMasterViewModel : BaseViewModel
    {
        public byte OrganisationMasterId { get; set; }
        public string EstablishmentCode { get; set; }
        [Required]
        public string OrganisationName { get; set; }
        public System.DateTime FoundationDatetime { get; set; }
        [Required]
        public string FounderMember { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PlotNumber { get; set; }
        public string StreetNumber { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Required]
        public string EmailId { get; set; }
        public string Url { get; set; }
        public string OfficeComment { get; set; }
        public string MissionStatement { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string FaxNumber { get; set; }
        public string OfficePhone1 { get; set; }
        public string OfficePhone2 { get; set; }
        [Required]
        public string OrganisationCode { get; set; }
        public string PFNumber { get; set; }
        public string ESICNumber { get; set; }
    }
}
