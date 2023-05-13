using System.Collections.Generic;

namespace RARIndia.ViewModel
{
    public class GeneralCityListViewModel : BaseViewModel
    {

        public List<GeneralCityViewModel> GeneralCityList { get; set; }

        public GeneralCityListViewModel()
        {
            GeneralCityList = new List<GeneralCityViewModel>();
        }
    }
}
