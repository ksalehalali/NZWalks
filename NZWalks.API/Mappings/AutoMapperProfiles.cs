﻿

using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionReqDto, Region>().ReverseMap();
            CreateMap<UpdateRegionReqDto, Region>().ReverseMap();
            CreateMap<AddWalkReqDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

            CreateMap<UpdateWalkReqDto, Walk>().ReverseMap();

        }
    }
}
