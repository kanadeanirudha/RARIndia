using RARIndia.Model;
using RARIndia.Utilities.Helper;
namespace RARIndia.DataAccessLayer.Helper
{
    public static class ServiceHelper
    {
        #region Constants
        public const string DateTimeRange = "DateTimeRange";
        #endregion

        public static void BindPageListModel(this BaseListModel baseListModel, PageListModel pageListModel)
        {
            if (RARIndiaHelperUtility.IsNotNull(pageListModel))
            {
                baseListModel.TotalResults = pageListModel.TotalRowCount;
                baseListModel.PageIndex = pageListModel.PagingStart;
                baseListModel.PageSize = pageListModel.PagingLength;
            }
        }
    }
}
