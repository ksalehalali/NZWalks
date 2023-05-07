using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext _dBContext;
        public RegionsController(NZWalksDBContext dBContext)
        {
            this._dBContext = dBContext;
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            var regions = _dBContext.Regions.ToList();

            //Map domain to dtos
            var regionsDto = new List<RegionDto>();
            foreach(var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name, 
                     Code = region.Code,
                     RegionImageUrl = region.RegionImageUrl,
                });
            }
            return Ok(regionsDto);
        }


        //get region by id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            //get region domain model
            var region = _dBContext.Regions.FirstOrDefault(a => a.Id==id);

            if(region == null)
            {
                return BadRequest();
            }
            //map region domain model to region dto
            var regionsDto = new RegionDto{
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionsDto);
        }

        //POST to create new region
        [HttpPost]
        public IActionResult Create([FromBody]AddRegionReqDto addRegionReqDto)
        {
            //Map Dto To model
            var regionDomainModel = new Region
            {
                Code = addRegionReqDto.Code,
                Name = addRegionReqDto.Name,
                RegionImageUrl=addRegionReqDto.RegionImageUrl,
            };

            //use domain model to create region
            _dBContext.Regions.Add(regionDomainModel);
            _dBContext.SaveChanges();

            //map domain model back to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };
            return CreatedAtAction(nameof(GetById),new {id = regionDomainModel.Id},regionDto);
        }

        //Update region
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute]Guid id,[FromBody]UpdateRegionReqDto updateRegionReqDto)
        {
            // check if region exists
            var region = _dBContext.Regions.FirstOrDefault(x =>x.Id == id);
            if(region == null)
            {
                return BadRequest();
            }

            //map dto to domain model
            region.Code = updateRegionReqDto.Code;
            region.Name = updateRegionReqDto.Name;
           region.RegionImageUrl = updateRegionReqDto.RegionImageUrl;       
            _dBContext.SaveChanges();

            //convert domain to dto
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,

                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionDto);


        }
    }
}
