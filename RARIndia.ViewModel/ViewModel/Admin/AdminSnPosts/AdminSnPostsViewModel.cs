
using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class AdminSnPostsViewModel : BaseViewModel
    {
        [Required]
        public string SelectedCentreCode { get; set; }
        [Required]
        public int SelectedDepartmentID { get; set; }
    }
}
