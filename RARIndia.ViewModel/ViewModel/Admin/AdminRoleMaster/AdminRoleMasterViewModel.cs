using RARIndia.Model;
using RARIndia.Resources;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RARIndia.ViewModel
{
    public class AdminRoleMasterViewModel : BaseViewModel
    {
        public AdminRoleMasterViewModel()
        {
            GeneralDepartmentList = new GeneralDepartmentListModel();
        }
        public GeneralDepartmentListModel GeneralDepartmentList { get; set; }

        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(GeneralResources))]
        public string SelectedCentreCode { get; set; }
        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(GeneralResources))]
        public string SelectedDepartmentID { get; set; }
        public Int16 AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        [Display(Name = "Role Description")]
        public string SactionedPostDescription { get; set; }

        public List<SelectListItem> MonitoringLevelList { get; set; }
        [Display(Name = "Monitoring Level")]
        public string MonitoringLevel { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Is Login Allow From Out side")]
        public bool IsLoginAllowFromOutside { get; set; }
        [Display(Name = "Is Attendace Allow From Out side")]
        public bool IsAttendaceAllowFromOutside { get; set; }

        [Display(Name = "Accesible Centre")]
        public List<string> SelectedRoleWiseCentres { get; set; }
        public string SelectedCentreCodeForSelf { get; set; }
        public string SelectedCentreNameForSelf { get; set; }
        public List<UserAccessibleCentreModel> AllCentreList { get; set; }
    }
}
