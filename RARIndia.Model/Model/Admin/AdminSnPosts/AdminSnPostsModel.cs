using System;

namespace RARIndia.Model
{
    public class AdminSnPostsModel : BaseModel
    {
        public Int16 AdminSnPostsId { get; set; }
        public Int16 DesignationID { get; set; }
        public Int16 DepartmentID { get; set; }
        public string CentreCode { get; set; }
        public string NomenAdminRoleCode { get; set; }
        public string SactionedPostDescription { get; set; }
        public Int16 NoOfPosts { get; set; }
        public string PostType { get; set; }
        public string DesignationType { get; set; }
        public bool IsActive { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string CentreName { get; set; }

    }
}
