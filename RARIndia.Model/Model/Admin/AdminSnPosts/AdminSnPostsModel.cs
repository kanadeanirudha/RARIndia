using System;

namespace RARIndia.Model
{
    public class AdminSnPostsModel : BaseModel
    {
        public Int16 AdminSnPostsId { get; set; }
        public string NomenAdminRoleCode { get; set; }
        public string SactionedPostDescription { get; set; }
        public Int16 NoOfPosts { get; set; }
        public bool IsActive { get; set; }
    }
}
