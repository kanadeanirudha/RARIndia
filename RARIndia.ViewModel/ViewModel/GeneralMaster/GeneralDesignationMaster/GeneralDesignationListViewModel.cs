using System.Collections.Generic;

namespace RARIndia.ViewModel
{
    public class GeneralDesignationListViewModel : BaseViewModel
    {

        public List<GeneralDesignationViewModel> GeneralDesignationList { get; set; }

        public GeneralDesignationListViewModel()
        {
            GeneralDesignationList = new List<GeneralDesignationViewModel>();
        }
    }
}
