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
    
    public partial class GeneralLocationMaster : RARIndiaEntityBaseModel
    {
        public int ID { get; set; }
        public Nullable<bool> DefaultFlag { get; set; }
        public Nullable<int> RegionID { get; set; }
        public string LocationAddress { get; set; }
        public string PostCode { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<bool> IsUserDefined { get; set; }
        public Nullable<bool> IsProviance { get; set; }
        public Nullable<bool> IsTahsil { get; set; }
        public Nullable<bool> Accuracy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
