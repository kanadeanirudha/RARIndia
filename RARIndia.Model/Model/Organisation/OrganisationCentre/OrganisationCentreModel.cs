using System;
using System.ComponentModel.DataAnnotations;

namespace RARIndia.Model
{
    public class OrganisationCentreModel : BaseModel
    {
        public short OrganisationCentreMasterId { get; set; }
        [MaxLength(15)]
        [Required]
        public string CentreCode { get; set; }
        [MaxLength(100)]
        [Required]
        public string CentreName { get; set; }
        [MaxLength(5)]
        public string HoCoRoScFlag { get; set; }
        public int? HoId { get; set; }
        public int? CoId { get; set; }
        public int? RoId { get; set; }
        [MaxLength(100)]
        public string CentreSpecialization { get; set; }
        public string CentreAddress { get; set; }
        public string PlotNo { get; set; }
        public string StreetName { get; set; }
        [Required]
        public int GeneralCityMasterId { get; set; }
        [MaxLength(50)]
        [Required]
        public string Pincode { get; set; }
        [MaxLength(70)]
        [Required]
        public string EmailId { get; set; }
        [MaxLength(30)]
        public string Url { get; set; }
        [MaxLength(50)]
        [Required]
        public string CellPhone { get; set; }
        [MaxLength(50)]
        public string FaxNumber { get; set; }
        [MaxLength(50)]
        public string PhoneNumberOffice { get; set; }
        public DateTime? CentreEstablishmentDatetime { get; set; }
        [Required]
        public byte OrganisationId { get; set; }
        public int? CentreLoginNumber { get; set; }
        [MaxLength(50)]
        public string InstituteCode { get; set; }
        [MaxLength(32)]
        public string TimeZone { get; set; }
        public decimal? Latitude { get; set; }
        [MaxLength(9)]
        public decimal? Longitude { get; set; }
        public double? CampusArea { get; set; }

        [MaxLength(25)]
        public string CINNumber { get; set; }

        [MaxLength(25)]
        public string GSTINNumber { get; set; }

        [MaxLength(25)]
        public string PanNumber { get; set; }

        [MaxLength(35)]
        public string PFNumber { get; set; }

        [MaxLength(35)]
        public string ESICNumber { get; set; }

        [MaxLength(35)]
        public string WaterMark { get; set; }
    }
}


