using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class GeneralNationalityViewModel : BaseViewModel
    {
        public int NationalityId { get; set; }
        [Display(Name = "Nationality")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Is Default")]
        public bool DefaultFlag { get; set; }
    }
}
