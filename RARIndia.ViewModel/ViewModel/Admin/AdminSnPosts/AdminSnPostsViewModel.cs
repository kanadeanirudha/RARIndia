using RARIndia.Resources;

using System;
using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class AdminSnPostsViewModel : BaseViewModel
    {
        public AdminSnPostsViewModel()
        {

        }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(GeneralResources))]
        public string SelectedCentreCode { get; set; }
        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(GeneralResources))]
        public string SelectedDepartmentID { get; set; }
        public Int16 AdminSactionPostId { get; set; }
        public Int16 DesignationID { get; set; }
        public Int16 DepartmentID { get; set; }
        public string CentreCode { get; set; }
        public string SactionPostCode { get; set; }
        public string SactionedPostDescription { get; set; }
        public Int16 NoOfPost { get; set; } = 1;

        [Required(ErrorMessage = "Post Type Required")]
        [Display(Name = "Post Type")]
        public string PostType { get; set; }

        [Required(ErrorMessage = "Designation Type Required")]
        [Display(Name = "Designation Type")]
        public string DesignationType { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "LabelDepartments", ResourceType = typeof(GeneralResources))]
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        [Display(Name = "LabelCentre", ResourceType = typeof(GeneralResources))]
        public string CentreName { get; set; }
    }
}
