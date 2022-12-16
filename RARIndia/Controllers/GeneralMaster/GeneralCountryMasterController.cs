using RARIndia.BusinessLogicLayer;
using RARIndia.Resources;
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
        public ActionResult List()
        {
            GeneralCountryListViewModel list = new GeneralCountryListViewModel();
            list = _generalCountryMasterBA.GetCountryList(null, null, null, 1, 10);
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
                    return RedirectToAction<GeneralCountryMasterController>(x => x.Edit(generalCountryViewModel.ID));
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
                SetNotificationMessage(_generalCountryMasterBA.UpdateCountry(generalCountryViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                return RedirectToAction<GeneralCountryMasterController>(x => x.Edit(generalCountryViewModel.ID));
            }
            return View(createEdit, generalCountryViewModel);
        }

        //Delete country.
        public virtual JsonResult Delete(string countryIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(countryIds))
            {
                status = _generalCountryMasterBA.DeleteCountry(countryIds, out message);

                return Json(new { status = status, message = status ? GeneralResources.DeleteMessage : message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = GeneralResources.DeleteErrorMessage }, JsonRequestBehavior.AllowGet);
        }

    }
}