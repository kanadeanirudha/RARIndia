using AutoMapper;

using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.Model;
using RARIndia.Utilities.Filters;
using RARIndia.ViewModel;

namespace RARIndia
{
    public static class AutoMapperConfig
    {
        public static void Execute()
        {
            Mapper.CreateMap<FilterTuple, FilterDataTuple>();

            #region Admin
            Mapper.CreateMap<AdminSnPostsModel, AdminSnPostsViewModel>().ReverseMap();
            Mapper.CreateMap<AdminSnPostsListModel, AdminSnPostsListViewModel>().ReverseMap();
            Mapper.CreateMap<AdminSnPostsModel, AdminSactionPost>().ReverseMap();
            Mapper.CreateMap<AdminRoleMasterModel, AdminRoleMasterViewModel>().ReverseMap();
            Mapper.CreateMap<AdminRoleMasterListModel, AdminRoleMasterListViewModel>().ReverseMap();
            Mapper.CreateMap<AdminRoleMasterModel, AdminRoleMaster>().ReverseMap();
            #endregion

            #region General Master
            Mapper.CreateMap<GeneralCountryModel, GeneralCountryViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralCountryListModel, GeneralCountryListViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralCountryModel, GeneralCountryMaster>().ReverseMap();

            Mapper.CreateMap<GeneralCityModel, GeneralCityViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralCityListModel, GeneralCityListViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralCityModel, GeneralCityMaster>().ReverseMap();

            Mapper.CreateMap<GeneralDepartmentModel, GeneralDepartmentViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralDepartmentListModel, GeneralDepartmentListViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralDepartmentModel, GeneralDepartmentMaster>().ReverseMap();

            Mapper.CreateMap<GeneralDesignationModel, GeneralDesignationViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralDesignationListModel, GeneralDesignationListViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralDesignationModel, EmployeeDesignationMaster>().ReverseMap();

            Mapper.CreateMap<GeneralNationalityModel, GeneralNationalityViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralNationalityListModel, GeneralNationalityListViewModel>().ReverseMap();
            #endregion

            Mapper.CreateMap<UserModel, UserLoginViewModel>().ReverseMap();
            Mapper.CreateMap<UserModel, UserMaster>().ReverseMap();
            Mapper.CreateMap<UserModuleModel, UserModuleMaster>().ReverseMap();

            #region Organisation
            Mapper.CreateMap<OrganisationMasterModel, OrganisationMasterViewModel>().ReverseMap();
            Mapper.CreateMap<OrganisationMasterModel, OrganisationMaster>().ReverseMap();
            #endregion
        }
    }
}

