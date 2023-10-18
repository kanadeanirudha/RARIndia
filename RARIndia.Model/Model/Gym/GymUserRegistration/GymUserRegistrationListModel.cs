using System.Collections.Generic;

namespace RARIndia.Model
{
    public class GymUserRegistrationListModel : BaseListModel
    {
        public List<GymUserRegistrationModel> GymUserRegistrationList { get; set; }
        public GymUserRegistrationListModel()
        {
            GymUserRegistrationList = new List<GymUserRegistrationModel>();
        }
    }
}
