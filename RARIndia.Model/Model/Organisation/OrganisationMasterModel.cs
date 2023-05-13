using System;

namespace RARIndia.Model
{
    public class OrganisationMasterModel : BaseModel
    {
        public byte OrganisationMasterId { get; set; }
        public string EstablishmentCode { get; set; }
        public string OrganisationName { get; set; }
        public DateTime FoundationDatetime { get; set; }
        public string FounderMember { get; set; }
        public string Address1 { get; set; }
        public int GeneralCityMasterId { get; set; }
        public string Pincode { get; set; }
        public string EmailId { get; set; }
        public string Url { get; set; }
        public string OfficeComment { get; set; }
        public string MissionStatement { get; set; }
        public string MobileNumber { get; set; }
        public string FaxNumber { get; set; }
        public string OfficePhone1 { get; set; }
        public string OfficePhone2 { get; set; }
        public string OrganisationCode { get; set; }
        public string PFNumber { get; set; }
        public string ESICNumber { get; set; }
    }
}
