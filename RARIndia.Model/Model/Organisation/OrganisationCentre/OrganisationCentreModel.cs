using System;
using System.ComponentModel.DataAnnotations;

namespace RARIndia.Model
{
    public class OrganisationCentreModel : BaseModel
    {
        public short OrganisationCentreMasterId { get; set; }
        [MaxLength(15)]
        public string CentreCode { get; set; }
        public string CentreName { get; set; }
        public string HoCoRoScFlag { get; set; }
        public int? HoId { get; set; }
        public int? CoId { get; set; }
        public int? RoId { get; set; }
        public string CentreSpecialization { get; set; }
        public string CentreAddress { get; set; }
        public string PlotNo { get; set; }
        public string StreetName { get; set; }
        public int GeneralCityMasterId { get; set; }
        public string Pincode { get; set; }
        public string EmailId { get; set; }
        public string Url { get; set; }
        public string CellPhone { get; set; }
        public string FaxNumber { get; set; }
        public string PhoneNumberOffice { get; set; }
        public DateTime? CentreEstablishmentDatetime { get; set; }
        public byte OrganisationId { get; set; }
        public int? CentreLoginNumber { get; set; }
        public string InstituteCode { get; set; }
        public string TimeZone { get; set; }
        public decimal?Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public double?CampusArea { get; set; }
        public string CINNumber { get; set; }
        public string GSTINNumber { get; set; }
        public string PanNumber { get; set; }
        public string PFNumber { get; set; }
        public string ESICNumber { get; set; }
        public string WaterMark { get; set; }
    }
}


