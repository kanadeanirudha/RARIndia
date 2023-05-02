
using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class GeneralCountryViewModel : BaseViewModel
    {
        public short GeneralCountryMasterId { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        [Display(Name = "Country Code")]
        [Required]
        public string CountryCode { get; set; }
        [Display(Name = "Is Default")]
        public bool DefaultFlag { get; set; }

        [Display(Name = "Seq Number")]
        [Required]
        public short SeqNo { get; set; }
    }
}
