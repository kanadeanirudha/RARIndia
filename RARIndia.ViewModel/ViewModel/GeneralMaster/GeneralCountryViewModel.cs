using RARIndia.ViewModels;

using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class GeneralCountryViewModel : BaseViewModel
    {
        public short CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string CountryCode { get; set; }
        public bool DefaultFlag { get; set; }
        [Required]
        public short SeqNo { get; set; }
    }
}
