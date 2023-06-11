using RARIndia.BusinessLogicLayer;
using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.Filters;
using RARIndia.Model.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Constant;
using RARIndia.ViewModel;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class OrganisationCentreMasterController : BaseController
    {
        RARIndiaEntities db = new RARIndiaEntities();

        OrganisationCentreMasterBA _organisationCentreMasterBA = null;
        private const string createEdit = "~/Views/Organisation/OrganisationCentreMaster/CreateEdit.cshtml";
        private const string OrganisationCentrePrintingFormat = "~/Views/Organisation/OrganisationCentreMaster/OrganisationCentrePrintingFormat.cshtml";
        private readonly string organisationCentrePrintingFormatViewModel;

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

        //Get: Organisation Centre Printing Format.
        [HttpGet]
        public ActionResult PrintingFormat(string centreCode)
        {
            return View(OrganisationCentrePrintingFormat, new OrganisationCentrePrintingFormatViewModel());
        }

        //Post: Organisation Centre Printing Format .
        [HttpPost]
        public virtual ActionResult PrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentrePrintingFormatViewModel = _organisationCentreMasterBA.GetPrintingFormat(organisationCentrePrintingFormatViewModel);
                if (!organisationCentrePrintingFormatViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
                    TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
                    return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrePrintingFormatViewModel.ErrorMessage));
            return View(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }
        ////centreName

        //[HttpGet]
        //public ActionResult OrganisationPrintingFormat(string centreName )
        //{
        //    return View(OrganisationCentrePrintingFormat, new OrganisationCentrePrintingFormatViewModel());
        //}

        //[HttpPost]
        //public virtual ActionResult OrganisationPrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        organisationCentrePrintingFormatViewModel = _organisationCentreMasterBA.GetOrganisationPrintingFormat(organisationCentrePrintingFormatViewModel);
        //        if (!organisationCentrePrintingFormatViewModel.HasError)
        //        {
        //            SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordCreationSuccessMessage));
        //            TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
        //            return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
        //        }
        //    }
        //    SetNotificationMessage(GetErrorNotificationMessage(organisationCentrePrintingFormatViewModel.ErrorMessage));
        //    return View(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        //}

        #region
        //Logo :Printing Format
        [HttpGet]
        public ActionResult Index()
        {
            return View(OrganisationCentrePrintingFormat, new OrganisationCentrePrintingFormatViewModel());
        }

        [HttpPost]
        public ActionResult Index(tbl_data d, HttpPostedFileBase imgfile)
        {
            tbl_data di = new tbl_data();
            string path = uploadimage(imgfile);
            if (path.Equals("-1"))
            {

            }
            else
            {
                di.Logo = path;
                db.SaveChanges();
            }
            return View(OrganisationCentrePrintingFormat, new OrganisationCentrePrintingFormatViewModel());
        }
        public string uploadimage(HttpPostedFileBase imgfile)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (imgfile != null && imgfile.ContentLength > 0)
            {
                string extension = Path.GetExtension(imgfile.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Images/organisationCentrePrintingFormat"), random + Path.GetFileName(imgfile.FileName));
                        imgfile.SaveAs(path);
                        path = "~/Images/organisationCentrePrintingFormat/" + random + Path.GetFileName(imgfile.FileName);
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
        }
    }
    #endregion
    public class tbl_data
    {
        internal string Logo;
    }
}
















