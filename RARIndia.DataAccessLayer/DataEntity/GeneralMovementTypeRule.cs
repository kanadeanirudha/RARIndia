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
    
    public partial class GeneralMovementTypeRule
    {
        public byte ID { get; set; }
        public Nullable<byte> MovementTypeID { get; set; }
        public Nullable<int> TransactionType { get; set; }
        public Nullable<int> Direction { get; set; }
        public string RequisitionBehaviour { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<byte> Action { get; set; }
    
        public virtual GeneralMovementType GeneralMovementType { get; set; }
    }
}
