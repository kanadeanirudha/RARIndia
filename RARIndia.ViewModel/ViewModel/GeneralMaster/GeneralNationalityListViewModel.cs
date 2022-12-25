using System.Collections.Generic;

namespace RARIndia.ViewModel
{
    public class GeneralNationalityListViewModel : BaseViewModel
    {
        public List<GeneralNationalityViewModel> GeneralNationalityList { get; set; }
        public PageListViewModel PageListViewModel { get; set; }
        public GeneralNationalityListViewModel()
        {
            GeneralNationalityList = new List<GeneralNationalityViewModel>();
            PageListViewModel = new PageListViewModel();
        }
    }
}
