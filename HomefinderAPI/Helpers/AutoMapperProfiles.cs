using AutoMapper;
using HomefinderAPI.Models;
using HomefinderAPI.ViewModels;

namespace HomefinderAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Från --> Till
            CreateMap<Advertisement,AdvertisementViewModel>()
            .ForMember(dest => dest.TrackingNumber, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.ListPrice, options => options.MapFrom(src => src.Price));
        }
    }
}