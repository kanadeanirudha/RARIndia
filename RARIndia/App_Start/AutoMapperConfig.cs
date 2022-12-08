using AutoMapper;

using RARIndia.Model;
using RARIndia.ViewModel;

namespace RARIndia
{
    public static class AutoMapperConfig
    {
        public static void Execute()
        {
            Mapper.CreateMap<GeneralCountryMasterModel, GeneralCountryMasterViewModel>().ReverseMap();
        }
    }
}
