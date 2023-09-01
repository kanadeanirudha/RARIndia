using System.Collections.Generic;
namespace RARIndia.ViewModel
{
    public class GeneralTaxGroupMasterListViewModel : BaseViewModel
    {

        public List<GeneralTaxGroupMasterViewModel> GeneralTaxGroupMasterList { get; set; }

        public GeneralTaxGroupMasterListViewModel()
        {
            GeneralTaxGroupMasterList = new List<GeneralTaxGroupMasterViewModel>();
        }
    }
}
