using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.Model;

using System.Collections.Generic;
using System.Linq;

namespace RARIndia.DataAccessLayer
{
    public abstract class BaseDataAccessLogic
    {
        protected EmployeeDesignationMaster GetDesignationDetails(short designationID)
        {
            EmployeeDesignationMaster employeeDesignationMaster = new RARIndiaRepository<EmployeeDesignationMaster>().GetById(designationID);
            return employeeDesignationMaster;
        }

        protected GeneralDepartmentMaster GetDepartmentDetails(short departmentID)
        {
            GeneralDepartmentMaster generalDepartmentMaster = new RARIndiaRepository<GeneralDepartmentMaster>().GetById(departmentID);
            return generalDepartmentMaster;
        }

        protected OrganisationStudyCentreMaster GetOrganisationCentreDetails(string centreCode)
        {
            OrganisationStudyCentreMaster organisationCentreMaster = new RARIndiaRepository<OrganisationStudyCentreMaster>().Table.FirstOrDefault(x=>x.CentreCode == centreCode);
            return organisationCentreMaster;
        }

        protected List<UserAccessibleCentreModel> OrganisationCentreList()
        {
            List<OrganisationStudyCentreMaster> centreList = new RARIndiaRepository<OrganisationStudyCentreMaster>().Table.ToList();
            List<UserAccessibleCentreModel> organisationCentreList = new List<UserAccessibleCentreModel>();
            foreach (OrganisationStudyCentreMaster item in centreList)
            {
                organisationCentreList.Add(new UserAccessibleCentreModel()
                {
                    CentreCode = item.CentreCode,
                    CentreName = item.CentreName,
                    ScopeIdentity = "Centre"
                });
            }

            return organisationCentreList;
        }
    }
}

