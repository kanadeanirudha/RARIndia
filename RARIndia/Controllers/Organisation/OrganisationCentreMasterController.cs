using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.ViewModel;
using System.IO;
using System.Web;
using System.Web.Mvc;
namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class OrganisationCentreMasterController : BaseController
    {
        OrganisationCentreMasterBA _organisationCentreMasterBA = null;
        private const string createEdit = "~/Views/Organisation/OrganisationCentreMaster/CreateEdit.cshtml";
        private const string OrganisationCentrePrintingFormat = "~/Views/Organisation/OrganisationCentreMaster/OrganisationCentrePrintingFormat.cshtml";
        public OrganisationCentreMasterController()
        {
            _organisationCentreMasterBA = new OrganisationCentreMasterBA();
        }
        public ActionResult List(DataTableModel dataTableModel)
        {
            DataTableModel tempDataTable = TempData[RARIndiaConstant.DataTableModel] as DataTableModel;
            dataTableModel = tempDataTable == null ? dataTableModel ?? new DataTableModel() : tempDataTable;

            OrganisationCentreListViewModel list = _organisationCentreMasterBA.GetOrganisationCentreList(dataTableModel);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Organisation/OrganisationCentreMaster/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentreMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new OrganisationCentreViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(OrganisationCentreViewModel organisationCentreViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentreViewModel = _organisationCentreMasterBA.CreateOrganisationCentre(organisationCentreViewModel);
                if (!organisationCentreViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
                    return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentreViewModel.ErrorMessage));
            return View(createEdit, organisationCentreViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int organisationCentreId)
        {
            OrganisationCentreViewModel organisationCentreViewModel = _organisationCentreMasterBA.GetOrganisationCentre(organisationCentreId);
            return ActionView(createEdit, organisationCentreViewModel);
        }

        //Post:Edit OrganisationCentre.
        [HttpPost]
        public virtual ActionResult Edit(OrganisationCentreViewModel organisationCentreViewModel)
        {
            if (ModelState.IsValid)
            {

                bool status = _organisationCentreMasterBA.UpdateOrganisationCentre(organisationCentreViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable();
                    return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
                }
            }
            return View(createEdit, organisationCentreViewModel);
        }

        //Delete OrganisationCentre.
        public virtual ActionResult Delete(string organisationCentreId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(organisationCentreId))
            {
                status = _organisationCentreMasterBA.DeleteCentre(organisationCentreId, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
        }

        #region
        //Get: Printing Format
        [HttpGet]
        public virtual ActionResult PrintingFormat(string centreCode)
        {
            OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel = _organisationCentreMasterBA.GetPrintingFormat(centreCode);
            return ActionView(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
            return ActionView(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }

        //Post: Organisation Centre Printing Format .
        [HttpPost]
        public virtual ActionResult PrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        {
            if (ModelState.IsValid)
            {

                bool status = _organisationCentreMasterBA.UpdatePrintingFormat(organisationCentrePrintingFormatViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    TempData[RARIndiaConstant.DataTableModel] = UpdateActionDataTable();
                    return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
                }
            }
            return View(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }
        #endregion
        #region
        //Logo :Printing Format
        [HttpGet]
        public ActionResult Index()
        {
            return View(OrganisationCentrePrintingFormat, new OrganisationCentrePrintingFormatViewModel());
        }

        [HttpPost]
        public virtual ActionResult Index(OrganisationCentrePrintingFormatViewModel file)
        {
            string path = Server.MapPath("~/Images");
            string fileName = Path.GetFileName(file.LogoFilename);
            string fullPath = Path.Combine(path + fileName);
            file.SaveAs(fullPath);
            return View();
        }
        #endregion
    }
}














