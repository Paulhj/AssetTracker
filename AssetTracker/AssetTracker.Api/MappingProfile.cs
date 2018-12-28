using AssetTracker.Api.Models;
using AssetTracker.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganizationModel>()
                .ReverseMap();

            CreateMap<User, UserModel>()
                .ReverseMap();
        }
    }
}
