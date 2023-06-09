using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.DataAccessLayer.Helper;
using RARIndia.DataAccessLayer.Repository;
using RARIndia.ExceptionManager;
using RARIndia.Model;
using RARIndia.Resources;
using RARIndia.Utilities.Helper;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using static RARIndia.Utilities.Helper.RARIndiaHelperUtility;
namespace RARIndia.DataAccessLayer
{
    public class OrganisationCentreMasterDAL : BaseDataAccessLogic
    {
        private readonly IRARIndiaRepository<OrganisationCentreMaster> _organisationCentreMasterRepository;
        private readonly IRARIndiaRepository<OrganisationCentrePrintingFormat> _organisationCentrePrintingFormatRepository;
        public OrganisationCentreMasterDAL()
        {
            _organisationCentreMasterRepository = new RARIndiaRepository<OrganisationCentreMaster>();
            _organisationCentrePrintingFormatRepository = new RARIndiaRepository<OrganisationCentrePrintingFormat>();
        }
        public OrganisationCentreListModel GetOrganisationCentreList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            RARIndiaViewRepository<OrganisationCentreModel> objStoredProc = new RARIndiaViewRepository<OrganisationCentreModel>();
            objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<OrganisationCentreModel> organisationCentreList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetOrganisationCentreList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            OrganisationCentreListModel listModel = new OrganisationCentreListModel();

            listModel.OrganisationCentreList = organisationCentreList?.Count > 0 ? organisationCentreList : new List<OrganisationCentreModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Organisation Centre.

        public OrganisationCentreModel CreateOrganisationCentre(OrganisationCentreModel organisationCentreModel)
        {
            if (IsNull(organisationCentreModel))
                throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCodeAlreadyExist(organisationCentreModel.CentreCode))
            {
                throw new RARIndiaException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre code"));
            }
            OrganisationCentreMaster organisationCentreMaster = organisationCentreModel.FromModelToEntity<OrganisationCentreMaster>();

            //Create new Organisation Centre  and return it.
            OrganisationCentreMaster organisationData = _organisationCentreMasterRepository.Insert(organisationCentreMaster);
            if (organisationData?.OrganisationCentreMasterId > 0)
            {
                organisationCentreModel.OrganisationCentreMasterId = organisationData.OrganisationCentreMasterId;
            }
            else
            {
                organisationCentreModel.HasError = true;
                organisationCentreModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return organisationCentreModel;
        }

        //Get Organisation Centre by organisationCentreId.
        public OrganisationCentreModel GetOrganisationCentre(int organisationCentreId)
        {
            if (organisationCentreId <= 0)
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));
            //Get the organisation Details based on id.
            OrganisationCentreMaster organisationData = _organisationCentreMasterRepository.Table.FirstOrDefault(x => x.OrganisationCentreMasterId == organisationCentreId);
            OrganisationCentreModel organisationCentreModel = organisationData.FromEntityToModel<OrganisationCentreModel>();
            return organisationCentreModel;
        }

        //Update Organisation Centre.
        public OrganisationCentreModel UpdateOrganisationCentre(OrganisationCentreModel organisationCentreModel)
        {
            if (IsNull(organisationCentreModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);
            if (organisationCentreModel.OrganisationCentreMasterId < 1)

                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));
            bool isOrganisationCentreUpdated = _organisationCentreMasterRepository.Update(organisationCentreModel.FromModelToEntity<OrganisationCentreMaster>());
            if (!isOrganisationCentreUpdated)
            {
                organisationCentreModel.HasError = true;
                organisationCentreModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return organisationCentreModel;
        }

        //Delete Organisation Centre.
        public bool DeleteCentre(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new RARIndiaException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
            objStoredProc.SetParameter(RARIndiaCentreEnum.organisationId.ToString(), parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteOrganisationCentre @organisationCentreId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
        #region
        //Get Organisation Centre Printing Format by PrintingFormatId.
        public OrganisationCentrePrintingFormatModel GetPrintingFormat(string centreCode)
        {
            if (IsNull(centreCode))
                throw new RARIndiaException(ErrorCodes.InvalidData, string.Format(GeneralResources.ErrorCodeExists, "centreCode"));
            //Get the organisation Centre Printing Format Details based on id.
            OrganisationCentrePrintingFormat organisationCentrePrintingFormatData = _organisationCentrePrintingFormatRepository.Table.FirstOrDefault(x => x.CentreCode == centreCode);
            OrganisationCentrePrintingFormatModel organisationCentrePrintingFormatModel = organisationCentrePrintingFormatData.FromEntityToModel<OrganisationCentrePrintingFormatModel>();
            return organisationCentrePrintingFormatModel;
        }

        //Update Organisation Centre printing Format.
        public OrganisationCentrePrintingFormatModel UpdatePrintingFormat(OrganisationCentrePrintingFormatModel organisationCentrePrintingFormatModel)
        {
            if (IsNull(organisationCentrePrintingFormatModel))
                throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ErrorCodeExists);
            if (organisationCentrePrintingFormatModel.OrganisationCentrePrintingFormatId > 0)

                throw new RARIndiaException(ErrorCodes.NullModel, string.Format(GeneralResources.ErrorCodeExists, "centreCode"));
            bool isOrganisationCentrePrintingFormatUpdated = _organisationCentrePrintingFormatRepository.Update(organisationCentrePrintingFormatModel.FromModelToEntity<OrganisationCentrePrintingFormat>());
            if (!isOrganisationCentrePrintingFormatUpdated)
            {
                organisationCentrePrintingFormatModel.HasError = true;
                organisationCentrePrintingFormatModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return organisationCentrePrintingFormatModel;
        }
        #endregion

        #region Private Method
        //Check if Centre code is already present or not.
        private bool IsCodeAlreadyExist(string centreCode)
         => _organisationCentreMasterRepository.Table.Any(x => x.CentreCode == centreCode);
        #endregion
    }
}
