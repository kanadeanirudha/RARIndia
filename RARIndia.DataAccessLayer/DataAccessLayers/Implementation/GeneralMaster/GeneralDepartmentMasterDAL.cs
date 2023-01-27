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
    public class GeneralDepartmentMasterDAL
    {
        private readonly IRARIndiaRepository<GeneralDepartmentMaster> _generalDepartmentMasterRepository;
        public GeneralDepartmentMasterDAL()
        {
            _generalDepartmentMasterRepository = new RARIndiaRepository<GeneralDepartmentMaster>();
        }

        //public GeneralDepartmentListModel GetDepartmentList(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        //{
        //    //Bind the Filter, sorts & Paging details.
        //    PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
        //    RARIndiaViewRepository<GeneralDepartmentModel> objStoredProc = new RARIndiaViewRepository<GeneralDepartmentModel>();
        //    objStoredProc.SetParameter("@WhereClause", pageListModel.SPWhereClause, ParameterDirection.Input, DbType.String);
        //    objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
        //    objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
        //    objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
        //    objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
        //    List<GeneralDepartmentModel> countryList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetDepartmentList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
        //    GeneralDepartmentListModel listModel = new GeneralDepartmentListModel();

        //    listModel.GeneralDepartmentList = countryList?.Count > 0 ? countryList : new List<GeneralDepartmentModel>();
        //    listModel.BindPageListModel(pageListModel);
        //    return listModel;
        //}

        ////Create Department.
        //public GeneralDepartmentModel CreateDepartment(GeneralDepartmentModel GeneralDepartmentModel)
        //{
        //    if (RARIndiaHelperUtility.IsNull(GeneralDepartmentModel))
        //        throw new RARIndiaException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

        //    if (IsCodeAlreadyExist(GeneralDepartmentModel.DepartmentCode))
        //    {
        //        throw new RARIndiaException(ErrorCodes.AlreadyExist, GeneralResources.ErrorDepartmentCodeExists);
        //    }
        //    GeneralDepartmentMaster a = GeneralDepartmentModel.FromModelToEntity<GeneralDepartmentMaster>();
        //    //Create new Department and return it.
        //    GeneralDepartmentMaster countryData = _GeneralDepartmentMasterRepository.Insert(a);
        //    if (countryData?.ID > 0)
        //    {
        //        GeneralDepartmentModel.DepartmentId = countryData.ID;
        //    }
        //    else
        //    {
        //        GeneralDepartmentModel.HasError = true;
        //        GeneralDepartmentModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
        //    }
        //    return GeneralDepartmentModel;
        //}

        ////Get Department by Department id.
        //public GeneralDepartmentModel GetDepartment(int countryId)
        //{
        //    if (countryId <= 0)
        //        throw new RARIndiaException(ErrorCodes.IdLessThanOne, GeneralResources.ErrorDepartmentIdLessThanOne);

        //    //Get the Department Details based on id.
        //    GeneralDepartmentMaster countryData = _GeneralDepartmentMasterRepository.Table.FirstOrDefault(x => x.ID == countryId);
        //    GeneralDepartmentModel GeneralDepartmentModel = countryData.FromEntityToModel<GeneralDepartmentModel>();
        //    return GeneralDepartmentModel;
        //}

        ////Update Department.
        //public GeneralDepartmentModel UpdateDepartment(GeneralDepartmentModel GeneralDepartmentModel)
        //{
        //    bool isDepartmentUpdated = false;
        //    if (RARIndiaHelperUtility.IsNull(GeneralDepartmentModel))
        //        throw new RARIndiaException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

        //    if (GeneralDepartmentModel.DepartmentId < 1)
        //        throw new RARIndiaException(ErrorCodes.IdLessThanOne, GeneralResources.IdCanNotBeLessThanOne);

        //    //Update Department
        //    isDepartmentUpdated = _GeneralDepartmentMasterRepository.Update(GeneralDepartmentModel.FromModelToEntity<GeneralDepartmentMaster>());
        //    if (!isDepartmentUpdated)
        //    {
        //        GeneralDepartmentModel.HasError = true;
        //        GeneralDepartmentModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
        //    }
        //    return GeneralDepartmentModel;
        //}

        ////Delete Department.
        //public bool DeleteDepartment(ParameterModel parameterModel)
        //{
        //    if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
        //        throw new RARIndiaException(ErrorCodes.IdLessThanOne, GeneralResources.ErrorDepartmentIdLessThanOne);

        //    RARIndiaViewRepository<View_ReturnBoolean> objStoredProc = new RARIndiaViewRepository<View_ReturnBoolean>();
        //    objStoredProc.SetParameter(RARIndiaDepartmentEnum.DepartmentId.ToString(), parameterModel.Ids, ParameterDirection.Input, DbType.String);
        //    objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
        //    int status = 0;
        //    objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteDepartment @DepartmentId,  @Status OUT", 1, out status);

        //    return status == 1 ? true : false;
        //}


        //Get department list.
        public GeneralDepartmentListModel GetDepartmentsByCentreCode(string centreCode)
        {
            GeneralDepartmentListModel list = new GeneralDepartmentListModel();
            list.GeneralDepartmentList = (from a in _generalDepartmentMasterRepository.Table
                        join b in new RARIndiaRepository<OrganisationCentrewiseDepartment>().Table
                        on a.ID equals b.DepartmentID
                        where (b.CentreCode == centreCode || centreCode == null) && a.IsDeleted == false
                        select new GeneralDepartmentModel()
                        {
                            ID = a.ID,
                            DepartmentName = a.DepartmentName,
                            DeptShortCode= a.DeptShortCode,
                            PrintShortDesc=a.PrintShortDesc
                        })?.ToList();
            return list;
        }
        #region Private Method

        //Check if Department code is already present or not.
        //private bool IsCodeAlreadyExist(string countryCode)
        // => _GeneralDepartmentMasterRepository.Table.Any(x => x.ContryCode == countryCode);
        #endregion
    }
}
