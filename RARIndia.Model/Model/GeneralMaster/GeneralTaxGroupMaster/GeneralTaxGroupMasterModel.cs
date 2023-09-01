using System.Collections.Generic;
namespace RARIndia.Model
{
    public class GeneralTaxGroupMasterModel : BaseModel
    {
        public byte GeneralTaxGroupMasterId { get; set; }
        public string TaxGroupName { get; set; }
        public decimal TaxGroupRate { get; set; }
        public List<string> GeneralTaxMasterIds { get; set; }
        public bool IsOtherState { get; set; }
    }
}

