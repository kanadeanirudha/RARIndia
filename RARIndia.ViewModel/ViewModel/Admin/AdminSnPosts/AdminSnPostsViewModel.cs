
using System;
using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class AdminSnPostsViewModel : BaseViewModel
    {
        [Required]
        public string SelectedCentreCode { get; set; }
        [Required]
        public int SelectedDepartmentID { get; set; }

        public Int16 AdminSnPostsId { get; set; }
        public string NomenAdminRoleCode { get; set; }
        public string SactionedPostDescription { get; set; }
        public Int16 NoOfPosts { get; set; }
        public bool IsActive { get; set; }
    }
}
