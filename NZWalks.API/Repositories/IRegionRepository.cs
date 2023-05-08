using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<RegionDto>> GetAllRegionsAsync();

        Task<Region?> GetRegionById(Guid regionId);

        Task<RegionDto> AddRegionAsync(AddRegionReqDto addRegionReqDto);
        Task<RegionDto?> UpdateRegion(Guid id,UpdateRegionReqDto updateRegionReqDto);
        Task<Region?> DeleteRegionById(Guid id);
    }
}
