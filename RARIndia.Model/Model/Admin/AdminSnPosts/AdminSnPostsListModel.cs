using System.Collections.Generic;

namespace RARIndia.Model
{
    public class AdminSnPostsListModel : BaseListModel
    {
        public List<AdminSnPostsModel> AdminSnPostsList { get; set; }
        public AdminSnPostsListModel()
        {
            AdminSnPostsList = new List<AdminSnPostsModel>();
        }
    }
}
