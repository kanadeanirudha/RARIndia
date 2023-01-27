using RARIndia.BusinessLogicLayer;
using RARIndia.Filters;
using RARIndia.Model.Model;
using RARIndia.ViewModel;

using System.Web.Mvc;

namespace RARIndia.Controllers
{
    [SessionTimeoutAttribute]
    public class AdminSnPostsController : BaseController
    {
        readonly GeneralDepartmentMasterBA _generalDepartmentMasterBA = null;
        readonly AdminSnPostsBA _adminSnPostsBA = null;
        
        public AdminSnPostsController()
        {
            _generalDepartmentMasterBA = new GeneralDepartmentMasterBA();
            _adminSnPostsBA = new AdminSnPostsBA();
        }

        public ActionResult List(DataTableModel dataTableModel)
        {
            dataTableModel = dataTableModel ?? new DataTableModel();

            AdminSnPostsListViewModel viewModel = new AdminSnPostsListViewModel();
            
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentID > 0)
            {
                viewModel = _adminSnPostsBA.GetAdminSnPostsList(dataTableModel, dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentID);
                if (!Request.IsAjaxRequest())
                {
                    viewModel.GeneralDepartmentList = _generalDepartmentMasterBA.GetDepartmentsByCentreCode(dataTableModel.SelectedCentreCode);
                }
            }

            viewModel.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            viewModel.SelectedDepartmentID = dataTableModel.SelectedDepartmentID;

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Admin/AdminSnPosts/_List.cshtml", viewModel);
            }
            return View("~/Views/Admin/AdminSnPosts/List.cshtml", viewModel);
        }
    }
}