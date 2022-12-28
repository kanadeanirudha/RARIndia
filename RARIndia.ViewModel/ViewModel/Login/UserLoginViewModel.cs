
using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [MaxLength(100, ErrorMessage = "Email Address can not exceed 100 characters.")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Please enter a valid password.")]
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}