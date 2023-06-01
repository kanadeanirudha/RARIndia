using System.ComponentModel.DataAnnotations;

namespace RARIndia.ViewModel
{
    public class GeneralTaxMasterViewModel : BaseViewModel
    {
        public short GeneralTaxMasterId { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Tax Name")]
        public string TaxName { get; set; }

        [Display(Name = "Tax Rate ")]
        [Required]
        public decimal TaxRate { get; set; }
        public int? SalesGLAccount { get; set; }
        public int? PurchasingGLAccount { get; set; }
        public bool IsCompoundTax { get; set; }
        [Display(Name = "Is Other State ")]
        public bool IsOtherState { get; set; }
    }
}
