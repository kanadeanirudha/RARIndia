using RARIndia.Model;

using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.DataAccessLayer.Helper
{
    public static class ServiceHelper
    {

        public static void BindPageListModel(this BaseListModel baseListModel, PageListModel pageListModel)
        {
            if (IsNotNull(pageListModel))
            {
                baseListModel.TotalResults = pageListModel.TotalRowCount;
                baseListModel.PageIndex = pageListModel.PagingStart;
                baseListModel.PageSize = pageListModel.PagingLength;
            }
        }
    }
}
