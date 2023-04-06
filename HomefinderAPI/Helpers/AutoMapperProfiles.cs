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
            .ForMember(dest => dest.SearchNumber, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.ListPrice, options => options.MapFrom(src => src.Price))
            .ForMember(dest => dest.Area, options => options.MapFrom(src => src.Property.Area))
            .ForMember(dest => dest.LeaseType, options => options.MapFrom(src => src.Property.LeaseType!.Name))
            .ForMember(dest => dest.PropertyType, options => options.MapFrom(src => src.Property.PropertyType!.Name))
            .ForMember(dest => dest.City, options => options.MapFrom(src => src.Property.Address!.City))
            .ForMember(dest => dest.StreetAddress, options => options.MapFrom(src => src.Property.Address!.StreetAddress));
            
        }
    }
}