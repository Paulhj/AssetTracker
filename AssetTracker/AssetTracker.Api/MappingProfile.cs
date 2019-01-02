using AssetTracker.Core.Entities;
using AutoMapper;
using System.Linq;

namespace AssetTracker.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, Model.Organization>()
                .ReverseMap();

            CreateMap<User, Model.User>()
                .ReverseMap();

            CreateMap<AssetLocation, Model.AssetLocation>()
                .ForMember(dest => dest.LocationNm,
                           opts => opts.MapFrom(src => src.Location.Name));

            CreateMap<Asset, Model.Asset>()
                .ForMember(dest => dest.TypeNm,
                           opts => opts.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.StatusNm,
                           opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CurrentLocation,
                           opts => opts.MapFrom(src => src.AssetLocations
                                .OrderByDescending(m => m.CreateDt)
                                .First()))
                .ForMember(dest => dest.AssetId,
                           opts => opts.MapFrom(src => src.Id));

        }
    }
}
