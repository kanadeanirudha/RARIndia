//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RARIndia.DataAccessLayer.DataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class GeneralTaxGroupMasterDetail : RARIndiaEntityBaseModel
    {
        public short GeneralTaxGroupMasterDetailsId { get; set; }
        public byte GeneralTaxGroupMasterId { get; set; }
        public short GeneralTaxMasterId { get; set; }
        public bool IsOtherState { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual GeneralTaxGroupMaster GeneralTaxGroupMaster { get; set; }
        public virtual GeneralTaxMaster GeneralTaxMaster { get; set; }
    }
}
