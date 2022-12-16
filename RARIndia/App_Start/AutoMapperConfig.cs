using AutoMapper;

using RARIndia.DataAccessLayer.DataEntity;
using RARIndia.Model;
using RARIndia.ViewModel;

namespace RARIndia
{
    public static class AutoMapperConfig
    {
        public static void Execute()
        {
            Mapper.CreateMap<GeneralCountryModel, GeneralCountryViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralCountryListModel, GeneralCountryListViewModel>().ReverseMap();
            Mapper.CreateMap<GeneralCountryModel, GeneralCountryMaster>()
                .ForMember(d => d.ContryCode, opt => opt.MapFrom(src => src.CountryCode));
                //.ForMember(d => d.ID, opt => opt.MapFrom(src => src.CountryId));
            Mapper.CreateMap<GeneralCountryMaster, GeneralCountryModel>()
               .ForMember(d => d.CountryCode, opt => opt.MapFrom(src => src.ContryCode));
               //.ForMember(d => d.CountryId, opt => opt.MapFrom(src => src.ID));
        }
    }
}
