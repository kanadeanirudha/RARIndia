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
    
    public partial class GeneralPolicyMaster
    {
        public int ID { get; set; }
        public string PolicyCode { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public string PolicyRelatedToModuleCode { get; set; }
        public string PolicyApplicableStatus { get; set; }
        public Nullable<bool> IsPolicyActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
