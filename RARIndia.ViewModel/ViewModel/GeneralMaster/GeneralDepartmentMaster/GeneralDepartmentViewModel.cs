
using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class GeneralDepartmentViewModel : BaseViewModel
    {
        public short DepartmentId { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Required]
        [Display(Name = "Short Code")]
        public string DeptShortCode { get; set; }
        [Required]
        [Display(Name = "Print Short Desc")]
        public string PrintShortDesc { get; set; }

        [Display(Name = "Work Activity")]
        public bool WorkActivity { get; set; }
    }
}
