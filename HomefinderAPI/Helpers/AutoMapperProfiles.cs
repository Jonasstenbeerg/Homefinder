using AutoMapper;
using HomefinderAPI.Models;
using HomefinderAPI.ViewModels.Advertisement;

namespace HomefinderAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Från --> Till
            CreateMap<Advertisement,AdvertisementViewModel>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.ListPrice, options => options.MapFrom(src => src.Price))
            .ForMember(dest => dest.Area, options => options.MapFrom(src => src.Property.Area))
            .ForMember(dest => dest.LeaseType, options => options.MapFrom(src => src.Property.LeaseType!.Name))
            .ForMember(dest => dest.PropertyType, options => options.MapFrom(src => src.Property.PropertyType!.Name))
            .ForMember(dest => dest.City, options => options.MapFrom(src => src.Property.Address!.City))
            .ForMember(dest => dest.StreetAddress, options => options.MapFrom(src => src.Property.Address!.StreetAddress));
            CreateMap<PostAdvertisementViewModel, Advertisement>()
            .ForPath(dest => dest.Property.Address!.City, options => options.MapFrom(src => src.City))
            .ForPath(dest => dest.Property.Address!.PostalCode, options => options.MapFrom(src => src.PostalCode))
            .ForPath(dest => dest.Property.Address!.StreetAddress, options => options.MapFrom(src => src.StreetAddress))
            .ForPath(dest => dest.Property.Area, options => options.MapFrom(src => src.Area))
            .ForPath(dest => dest.Price, options => options.MapFrom(src => src.ListPrice))
            .ForPath(dest => dest.Property.PropertyTypeId, options => options.MapFrom(src => src.PropertyTypeId))
            .ForPath(dest => dest.Property.LeaseTypeId, options => options.MapFrom(src => src.LeaseTypeId));

        }
    }
}