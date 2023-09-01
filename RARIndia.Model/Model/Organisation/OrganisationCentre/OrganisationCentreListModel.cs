using System.Collections.Generic;
namespace RARIndia.Model
{
    public class OrganisationCentreListModel : BaseListModel
    {
        public List<OrganisationCentreModel> OrganisationCentreList { get; set; }
        public OrganisationCentreListModel()
        {
            OrganisationCentreList = new List<OrganisationCentreModel>();
        }
    }
}
