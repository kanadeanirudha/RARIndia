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
    using System.Collections.Generic;

    public partial class AdminSnPost : RARIndiaEntityBaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdminSnPost()
        {
            this.AdminRoleMasters = new HashSet<AdminRoleMaster>();
        }
    
        public short ID { get; set; }
        public short DesignationID { get; set; }
        public short NoOfPosts { get; set; }
        public short DepartmentID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CentreCode { get; set; }
        public string DesignationType { get; set; }
        public string NomenAdminRoleCode { get; set; }
        public string PostType { get; set; }
        public string SactionedPostDescription { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminRoleMaster> AdminRoleMasters { get; set; }
        public virtual GeneralDepartmentMaster GeneralDepartmentMaster { get; set; }
        public virtual EmployeeDesignationMaster EmployeeDesignationMaster { get; set; }
    }
}
