using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public Task<RegionDto> AddRegionAsync(AddRegionReqDto addRegionReqDto)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> DeleteRegionById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RegionDto>> GetAllRegionsAsync()
        {
            return new List<RegionDto>{
                new RegionDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "aaaa",
                    Code = "AS",
                    RegionImageUrl = "wewewew/.wewewee"

                } }; 
            }

        public Task<Region?> GetRegionById(Guid regionId)
        {
            throw new NotImplementedException();
        }

        public Task<RegionDto?> UpdateRegion(Guid id, UpdateRegionReqDto updateRegionReqDto)
        {
            throw new NotImplementedException();
        }
    }
    
}
