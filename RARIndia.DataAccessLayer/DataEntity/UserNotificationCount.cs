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
    
    public partial class UserNotificationCount : RARIndiaEntityBaseModel
    {
        public Nullable<int> PersonID { get; set; }
        public string PersonType { get; set; }
        public int NotificationCount { get; set; }
    }
}
