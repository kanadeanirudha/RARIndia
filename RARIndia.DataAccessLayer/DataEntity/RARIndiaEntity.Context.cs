﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RARIndiaEntities : DbContext
    {
        public RARIndiaEntities()
            : base("name=RARIndiaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminRoleApplicableDetail> AdminRoleApplicableDetails { get; set; }
        public virtual DbSet<AdminRoleCentreRight> AdminRoleCentreRights { get; set; }
        public virtual DbSet<AdminRoleMaster> AdminRoleMasters { get; set; }
        public virtual DbSet<AdminRoleMenuDetail> AdminRoleMenuDetails { get; set; }
        public virtual DbSet<AdminSactionPost> AdminSactionPosts { get; set; }
        public virtual DbSet<EmployeeDesignationMaster> EmployeeDesignationMasters { get; set; }
        public virtual DbSet<GeneralCity> GeneralCities { get; set; }
        public virtual DbSet<GeneralCountryMaster> GeneralCountryMasters { get; set; }
        public virtual DbSet<GeneralDepartmentMaster> GeneralDepartmentMasters { get; set; }
        public virtual DbSet<GeneralLocationMaster> GeneralLocationMasters { get; set; }
        public virtual DbSet<GeneralNationalityMaster> GeneralNationalityMasters { get; set; }
        public virtual DbSet<GeneralRegionMaster> GeneralRegionMasters { get; set; }
        public virtual DbSet<GeneralTaxGroupMaster> GeneralTaxGroupMasters { get; set; }
        public virtual DbSet<GeneralTaxGroupMasterDetail> GeneralTaxGroupMasterDetails { get; set; }
        public virtual DbSet<GeneralTaxMaster> GeneralTaxMasters { get; set; }
        public virtual DbSet<GeneralTitleMaster> GeneralTitleMasters { get; set; }
        public virtual DbSet<OrganisationCentreMaster> OrganisationCentreMasters { get; set; }
        public virtual DbSet<OrganisationCentrePrintingFormat> OrganisationCentrePrintingFormats { get; set; }
        public virtual DbSet<OrganisationCentrewiseDepartment> OrganisationCentrewiseDepartments { get; set; }
        public virtual DbSet<OrganisationMaster> OrganisationMasters { get; set; }
        public virtual DbSet<UserMainMenuMaster> UserMainMenuMasters { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<UserModuleMaster> UserModuleMasters { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<UserNotificationCount> UserNotificationCounts { get; set; }
    }
}
