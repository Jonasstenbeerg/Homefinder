using AutoMapper;
using HomefinderAPI.Models;
using HomefinderAPI.ViewModels.Advertisement;
using HomefinderAPI.ViewModels.LeaseType;
using HomefinderAPI.ViewModels.PropertyType;

namespace HomefinderAPI.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      //From --> To
      CreateMap<Advertisement,AdvertisementViewModel>()
      .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
      .ForMember(dest => dest.ListPrice, options => options.MapFrom(src => src.Price))
      .ForMember(dest => dest.Area, options => options.MapFrom(src => src.Property.Area))
      .ForMember(dest => dest.LeaseType, options => options.MapFrom(src => src.Property.LeaseType!.Name))
      .ForMember(dest => dest.PropertyType, options => options.MapFrom(src => src.Property.PropertyType!.Name))
      .ForMember(dest => dest.City, options => options.MapFrom(src => src.Property.Address!.City))
      .ForMember(dest => dest.StreetName, options => options.MapFrom(src => src.Property.Address!.StreetName))
      .ForMember(dest => dest.StreetNumber, options => options.MapFrom(src => src.Property.Address!.StreetNumber));
      CreateMap<PostAdvertisementViewModel, Advertisement>()
      .ForPath(dest => dest.Property.Address!.City, options => options.MapFrom(src => src.City))
      .ForPath(dest => dest.Property.Address!.PostalCode, options => options.MapFrom(src => src.PostalCode))
      .ForPath(dest => dest.Property.Address!.StreetName, options => options.MapFrom(src => src.StreetName))
      .ForPath(dest => dest.Property.Address!.StreetNumber, options => options.MapFrom(src => src.StreetNumber))
      .ForPath(dest => dest.Property.Area, options => options.MapFrom(src => src.Area))
      .ForPath(dest => dest.Price, options => options.MapFrom(src => src.ListPrice));
      CreateMap<PostLeaseTypeViewModel, LeaseType>();
      CreateMap<LeaseType, LeaseTypeViewModel>();
      CreateMap<PostPropertyTypeViewModel, PropertyType>();
    }
  }
}