using System;

namespace RARIndia.Model
{
    public class AdminSnPostsModel : BaseModel
    {
        public Int16 AdminSactionPostId { get; set; }
        public Int16 DesignationId { get; set; }
        public Int16 DepartmentId { get; set; }
        public string CentreCode { get; set; }
        public string SactionPostCode { get; set; }
        public string SactionedPostDescription { get; set; }
        public Int16 NoOfPost { get; set; }
        public string PostType { get; set; }
        public string DesignationType { get; set; }
        public bool IsActive { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string CentreName { get; set; }
    }
}
