using System.Collections.Generic;

namespace RARIndia.ViewModel
{
    public class GeneralTaxMasterListViewModel : BaseViewModel
    {

        public List<GeneralTaxMasterViewModel> GeneralTaxMasterList { get; set; }

        public GeneralTaxMasterListViewModel()
        {
            GeneralTaxMasterList = new List<GeneralTaxMasterViewModel>();
        }
    }
}