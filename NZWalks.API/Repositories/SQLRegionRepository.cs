using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _dBContext;
        private readonly IMapper mapper;

        public SQLRegionRepository(NZWalksDBContext dBContext,IMapper mapper)
        {
            this._dBContext = dBContext;
            this.mapper = mapper;
        }


        //create
        public async Task<RegionDto> AddRegionAsync(AddRegionReqDto addRegionReqDto)
        {
            //Map AddDto To model
            var regionDomainModel = mapper.Map<Region>(addRegionReqDto);

            //use domain model to create region
            await _dBContext.Regions.AddAsync(regionDomainModel);
            await _dBContext.SaveChangesAsync();

            //mapping domain model to dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return regionDto;
        }


        //delete
        public async Task<Region?> DeleteRegionById(Guid id)
        {
            var regionDomainModel = await _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return null;
            }

            _dBContext.Regions.Remove(regionDomainModel);
            await _dBContext.SaveChangesAsync();

            return regionDomainModel;
        }

        //get all
        public async Task<List<RegionDto>> GetAllRegionsAsync()
        {
            var regions = await _dBContext.Regions.ToListAsync();

            //Map domain to dtos
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                //mapping domain model to dto
                var regionDto = mapper.Map<RegionDto>(region);
                regionsDto.Add(regionDto);
            }

            return regionsDto;
        }

        //get by id
        public async Task<Region?> GetRegionById(Guid regionId)
        {
            var region = await _dBContext.Regions.FirstOrDefaultAsync(r => r.Id == regionId);
            if (region == null)
            {
                return null;
            }
            return region;
           
        }

        //update
        public async Task<RegionDto?> UpdateRegion(Guid id, UpdateRegionReqDto updateRegionReqDto)
        {
            // check if region exists
            var region = await _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }

            //map dto to domain model
            region.Code = updateRegionReqDto.Code;
            region.Name = updateRegionReqDto.Name;
            region.RegionImageUrl = updateRegionReqDto.RegionImageUrl;
            await _dBContext.SaveChangesAsync();

            //convert domain model to dto
            var regionDto = mapper.Map<RegionDto>(region);
            return regionDto;
        }
    }
}
