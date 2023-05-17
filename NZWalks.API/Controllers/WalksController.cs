using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkReqDTO addWalkReqDTO)
        {
           
                //map dto to model
                var walkDomainModel = mapper.Map<Walk>(addWalkReqDTO);
                await walkRepository.CreateAsync(walkDomainModel);


                //map domain to dto
                var walkDtoModel = mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkDtoModel);

        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery]string? filterQuery,
            [FromQuery]string? sortBy, [FromQuery]bool? isAscending,[FromQuery] int pageNumber =1, [FromQuery]int pageSize = 1000 )
        {
          var walkDomainModel = await  walkRepository.GetAllAsync(filterOn,filterQuery,sortBy, isAscending ?? true,
              pageNumber,pageSize);
            //map domain model to dto
            return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));
        }

        //get walk by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walkDomiainModel = await walkRepository.GetWalkByIdAsync(id);
            if (walkDomiainModel == null)
            {
                return NotFound();
            }
            //map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomiainModel));
            
        }

        //update walk by id
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id,UpdateWalkReqDto updateWalkReqDto)
        {
          
                //map dto to domain model
                var walkDomainModel = mapper.Map<Walk>(updateWalkReqDto);
                walkDomainModel = await walkRepository.UpdateWalkAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                //map domain modedl to dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
 
        }

        //delete walk by id

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var deletedWalk =await walkRepository.DeleteByIdAsync(id);
            if(deletedWalk == null)
            {
                return NotFound();
            }

            //map domain model to dto
            return Ok(mapper.Map<WalkDto>(deletedWalk));

        }
    }
}
