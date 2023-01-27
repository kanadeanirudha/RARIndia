using System.Collections.Generic;

namespace RARIndia.ViewModel
{
    public class GeneralCountryListViewModel : BaseViewModel
    {

        public List<GeneralCountryViewModel> GeneralCountryList { get; set; }

        public GeneralCountryListViewModel()
        {
            GeneralCountryList = new List<GeneralCountryViewModel>();
        }
    }
}
