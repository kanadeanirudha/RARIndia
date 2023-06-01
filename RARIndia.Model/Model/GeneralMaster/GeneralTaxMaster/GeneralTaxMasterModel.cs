using System.ComponentModel.DataAnnotations;

namespace RARIndia.Model
{
    public class GeneralTaxMasterModel : BaseModel
    {
        public short GeneralTaxMasterId { get; set; }
        [MaxLength(50)]
        [Required]
        public string TaxName { get; set; }
        [Required]
        public decimal TaxRate { get; set; }
        public int? SalesGLAccount { get; set; }
        public int? PurchasingGLAccount { get; set; }
        public bool IsCompoundTax { get; set; }
        public bool IsOtherState { get; set; }
    }
}
