using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.Model;
using RARIndia.Model.Model;
using RARIndia.Utilities.Constant;
using RARIndia.Utilities.Helper;

using System;
using System.Collections.Specialized;
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
    }
}

