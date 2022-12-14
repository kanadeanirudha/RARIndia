using RARIndia.DataAccessLayer;
using RARIndia.Model;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace RARIndia.BusinessLogicLayer
{
    public class GeneralCountryMasterBA : BaseBusinessLogic
    {
        GeneralCountryMasterDAL generalCountryMasterDAL = null;
        public GeneralCountryMasterBA()
        {
            generalCountryMasterDAL = new GeneralCountryMasterDAL();
        }

        public GeneralCountryListViewModel GetCountryList(FilterCollection filters, string sort, string sortBy, int pageIndex, int pageSize)
        {
            NameValueCollection sortlist = SortingData(sort, sortBy);

            GeneralCountryListModel countryList = generalCountryMasterDAL.GetCountryList(filters, sortlist, pageIndex, pageSize);
            GeneralCountryListViewModel listViewModel = new GeneralCountryListViewModel { GeneralCountryList = countryList?.GeneralCountryList?.ToViewModel<GeneralCountryViewModel>().ToList() };
            SetListPagingData(listViewModel, countryList);

            return countryList?.GeneralCountryList?.Count > 0 ? listViewModel : new GeneralCountryListViewModel() { GeneralCountryList = new List<GeneralCountryViewModel>() };
        }
    }
}
