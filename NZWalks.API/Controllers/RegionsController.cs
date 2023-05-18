using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Data;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext _dBContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDBContext dBContext,IRegionRepository regionRepository,IMapper mapper, ILogger<RegionsController> logger)
        {
            this._dBContext = dBContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        //get all
        [MapToApiVersion("1.0")]
        [HttpGet]
       // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("GetAllRegions action method was invoked");
            var regions = await regionRepository.GetAllRegionsAsync();
            if(regions ==null)
            {

                return NotFound();
            }
            logger.LogInformation($"Finished GetAllRegions data: {JsonSerializer.Serialize(regions)}");

            return Ok(regions);
        }

        //get all v2
        [MapToApiVersion("2.0")]
        [HttpGet]
        // [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetV2()
        {
            logger.LogInformation("GetAllRegions action method was invoked");
            var regions = await regionRepository.GetAllRegionsAsync();
            if (regions == null)
            {

                return NotFound();
            }
            logger.LogInformation($"Finished GetAllRegions data: {JsonSerializer.Serialize(regions)}");

            return Ok(regions[1]);
        }


        //get region by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            //get region domain model
            var region = await regionRepository.GetRegionById(id);

            if(region == null)
            {
                return NotFound();
            }

            //mapping domain model to dto
           var regionDto = mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        //POST to create new region
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]AddRegionReqDto addRegionReqDto)
        {
           

                //use domain model to create region
                var region = await regionRepository.AddRegionAsync(addRegionReqDto);


                //mapping domain model to dto
                var regionDto = mapper.Map<RegionDto>(region);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
           
        }

        //Update region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody]UpdateRegionReqDto updateRegionReqDto)
        {
                var region = await regionRepository.UpdateRegion(id, updateRegionReqDto);
                if (region == null)
                {
                    return NotFound();
                }

                return Ok(region);
          

        }

        //Delete region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteRegionById(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(regionDomainModel);
        }
    }
}
