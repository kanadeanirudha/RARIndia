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
    
    public partial class SaleContractJobWorkData
    {
        public int ID { get; set; }
        public Nullable<int> CustomerMasterID { get; set; }
        public Nullable<int> CustomerBranchMasterID { get; set; }
        public Nullable<int> SaleContractJobWorkItemID { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<long> SaleContractMasterID { get; set; }
        public Nullable<long> SaleContractBillingSpanID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
