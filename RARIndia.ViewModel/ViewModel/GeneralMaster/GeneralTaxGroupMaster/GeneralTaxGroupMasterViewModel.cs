using RARIndia.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RARIndia.ViewModel
{
    public class GeneralTaxGroupMasterViewModel : BaseViewModel
    {
        public short GeneralTaxGroupMasterId { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Tax Group Name")]
        public string TaxGroupName { get; set; }
        public decimal TaxGroupRate { get; set; }

        [Required]
        [Display(Name = "Taxes")]
        public List<String> GeneralTaxMasterIds { get; set; }
        public List<GeneralTaxMasterModel> AllTaxList { get; set; }
        public bool IsOtherState { get; set; }
    }
}
