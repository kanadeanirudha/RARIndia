using System.Collections.Generic;
namespace RARIndia.ViewModel
{
    public class OrganisationCentreListViewModel : BaseViewModel
    {
        public List<OrganisationCentreViewModel> OrganisationCentreList { get; set; }

        public OrganisationCentreListViewModel()
        {
            OrganisationCentreList = new List<OrganisationCentreViewModel>();
        }
    }
}