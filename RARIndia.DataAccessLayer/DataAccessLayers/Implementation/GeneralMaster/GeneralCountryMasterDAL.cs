using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Helper;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.Model;
using RARIndia.Utilities.Helper;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

namespace RARIndia.DataAccessLayer
{
    public class GeneralCountryMasterDAL
    {
        private readonly IRARIndiaRepository<GeneralCountryMaster> _generalCountryMasterRepository;
        public GeneralCountryMasterDAL()
        {
            _generalCountryMasterRepository = new RARIndiaRepository<GeneralCountryMaster>();
        }

        public virtual GeneralCountryListModel GetCountryList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<GeneralCountryModel> objStoredProc = new RARIndiaViewRepository<GeneralCountryModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralCountryModel> countryList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetCountryList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount).ToList();
            GeneralCountryListModel listModel = new GeneralCountryListModel();

            listModel.GeneralCountryList = countryList?.Count > 0 ? countryList : new List<GeneralCountryModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
    }
}
