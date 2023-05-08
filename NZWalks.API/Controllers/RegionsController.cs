using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext _dBContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDBContext dBContext,IRegionRepository regionRepository,IMapper mapper)
        {
            this._dBContext = dBContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        //get all
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var regions = await regionRepository.GetAllRegionsAsync();
            if(regions ==null)
            {
                return NotFound();
            }
           
            return Ok(regions);
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
        public async Task<IActionResult> Create([FromBody]AddRegionReqDto addRegionReqDto)
        {

            //use domain model to create region
          var region = await regionRepository.AddRegionAsync(addRegionReqDto);


            //mapping domain model to dto
            var regionDto = mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetById),new {id = regionDto.Id},regionDto);
        }

        //Update region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody]UpdateRegionReqDto updateRegionReqDto)
        {
           
            var region = await regionRepository.UpdateRegion(id,updateRegionReqDto);
            if(region == null)
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
