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
    using RARIndia.Model;

    using System;

    public partial class AdminRoleEntity : RARIndiaEntityBaseModel
    {
        public short ID { get; set; }
        public string EntityName { get; set; }
        public byte AdminRoleDomainId { get; set; }
        public string TableName { get; set; }
        public string PrimayFieldKeyName { get; set; }
        public string DisplayFieldName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
