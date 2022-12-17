using RARIndia.BusinessLogicLayer;
using RARIndia.Resources;
using RARIndia.Utilities.Helper;
using RARIndia.ViewModel;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    public class GeneralCountryMasterController : BaseController
    {
        GeneralCountryMasterBA _generalCountryMasterBA = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralCountryMaster/CreateEdit.cshtml";
        public GeneralCountryMasterController()
        {
            _generalCountryMasterBA = new GeneralCountryMasterBA();
        }

        [HttpGet]
        public ActionResult List(string sortByColumn = null, string sortBy = null)
        {
            GeneralCountryListViewModel list = new GeneralCountryListViewModel();
            list = _generalCountryMasterBA.GetCountryList(null, sortByColumn, sortBy, 1, 10);
            return View("~/Views/GeneralMaster/GeneralCountryMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralCountryViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralCountryViewModel generalCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                generalCountryViewModel = _generalCountryMasterBA.CreateCountry(generalCountryViewModel);
                if (!generalCountryViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    return RedirectToAction<GeneralCountryMasterController>(x => x.List(SortKeys.CreatedDate, RARIndiaConstant.DESCKey));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalCountryViewModel.ErrorMessage));
            return View(createEdit, generalCountryViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int countryId)
        {
            GeneralCountryViewModel generalCountryViewModel = _generalCountryMasterBA.GetCountry(countryId);
            return ActionView(createEdit, generalCountryViewModel);
        }

        //Post:Edit country.
        [HttpPost]
        public virtual ActionResult Edit(GeneralCountryViewModel generalCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _generalCountryMasterBA.UpdateCountry(generalCountryViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                    return RedirectToAction<GeneralCountryMasterController>(x => x.List(SortKeys.ModifiedDate, RARIndiaConstant.DESCKey));
            }
            return View(createEdit, generalCountryViewModel);
        }

        //Delete country.
        public virtual ActionResult Delete(string countryIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(countryIds))
            {
                status = _generalCountryMasterBA.DeleteCountry(countryIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralCountryMasterController>(x => x.List(SortKeys.CountryName, RARIndiaConstant.ASCKey));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralCountryMasterController>(x => x.List(SortKeys.CountryName, RARIndiaConstant.ASCKey));
        }

    }
}