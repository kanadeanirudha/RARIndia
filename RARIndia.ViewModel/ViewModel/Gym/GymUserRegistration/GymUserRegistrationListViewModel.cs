using System.Collections.Generic;

namespace RARIndia.ViewModel
{
    public class GymUserRegistrationListViewModel : BaseViewModel
    {

        public List<GymUserRegistrationViewModel> GymUserRegistrationList { get; set; }

        public GymUserRegistrationListViewModel()
        {
            GymUserRegistrationList = new List<GymUserRegistrationViewModel>();
        }
    }
}
