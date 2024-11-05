using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilter;
using NZWalksAPI.Data;
using NZWalksAPI.DTO;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // WebAPI Tutorial Section 1 to Section 3 completed till CRUD function 
        public readonly ApplicationDbContext applicationDbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(ApplicationDbContext applicationDbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// GetAllRegions : Get All data by  calling this method
        /// API/Region?Filteron=Name&FilterQuery=Track
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRegions([FromQuery] string? Filteron , [FromQuery] string? FilterQuery)
        {
            var dbGetAllRegion = await regionRepository.GetAllRegionsAsync(Filteron, FilterQuery);
            //await applicationDbContext.regions.ToListAsync();         
            //var regionDTO = new List<RegionsDTO>();
            //foreach (var region in dbGetAllRegion)
            //{
            //    regionDTO.Add(new RegionsDTO()
            //    {
            //        Guid = region.Guid,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageurl = region.RegionImageurl,
            //    });
            //}
            // use Automapper
            var regionDTO = mapper.Map<List<RegionsDTO>>(dbGetAllRegion);
            return Ok(regionDTO);
        }
        /// <summary>
        /// GetRegionbyId will populate data which is matching the reqeust ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionbyId([FromRoute] Guid id)
        {
            //var dbGetRegionbyId = applicationDbContext.regions.Find(id);
            var dbGetRegionbyfirstDefault = await regionRepository.GetRegionByIdAsync(id);
            if (dbGetRegionbyfirstDefault == null)
            {
                return NotFound();
            }
            //return Ok(dbGetRegionbyId); ;
            return Ok(mapper.Map<RegionsDTO>(dbGetRegionbyfirstDefault));
        }
        /// <summary>
        /// AddRegion : Add Region data which is  coming from the Request Body
        /// </summary>
        /// <param name="addRegionRequestDTO"></param>
        /// <returns></returns>
        [HttpPost("AddRegions")]
        [ValidateActionFilter]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or convert the RegiondDTO into  Domain Model
            //var regionDM = new Region
            //{
            //    Code = addRegionRequestDTO.Code,
            //    Name = addRegionRequestDTO.Name,
            //    RegionImageurl = addRegionRequestDTO?.RegionImageurl,
            //};
            //User Automapper
            var regionDM = mapper.Map<Region>(addRegionRequestDTO);
            // Use DBcontext to add DomainModel
            await regionRepository.CreateRegionAsync(regionDM);
            // commit  into  the database
            // map domainmodel  again into DTO show to Client only 
            //var mapDTO = new RegionsDTO
            //{
            //    Guid = regionDM.Guid,
            //    Code = regionDM.Code,
            //    Name = regionDM.Name,
            //    RegionImageurl = regionDM.RegionImageurl
            //};
            var mapDTO = mapper.Map<RegionsDTO>(regionDM);
            //201 Created Response
            return CreatedAtAction(nameof(AddRegion), new { id = regionDM.Guid }, mapDTO);
        }
        /// <summary>
        /// UpdateRegion Update  Region data by Update Region body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRegionRequestDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //var RegionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDTO.Code,
            //    Name = updateRegionRequestDTO.Name,
            //    RegionImageurl = updateRegionRequestDTO.RegionImageurl
            //};
            //Add automapper
            var RegionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);
            RegionDomainModel = await regionRepository.UpdateRegionAsync(id, RegionDomainModel);
            if(RegionDomainModel == null)
            {
                return NotFound();
            }
            // Domain model to DTO
            //var RegionDTo = new RegionsDTO
            //{
            //    Guid = RegionDomainModel.Guid,
            //    Code = RegionDomainModel.Code,
            //    Name = RegionDomainModel.Name,
            //    RegionImageurl = RegionDomainModel.RegionImageurl
            //};
            var RegionDTo = mapper.Map<RegionsDTO>(RegionDomainModel);
            // Returning DTO to the client view
            return Ok(RegionDTo);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        [ValidateActionFilter]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
           var regiondata= await regionRepository.DeleteRegionAsync(id);
            if(regiondata == null)
            {
                return NotFound();
            }
            // in case we need to show deleted region from dto 
            //var RegionDTo = new RegionsDTO
            //{
            //    Guid = regiondata.Guid,
            //    Code = regiondata.Code,
            //    Name = regiondata.Name,
            //    RegionImageurl = regiondata.RegionImageurl
            //};
            //var RegionDTo = mapper.Map<RegionsDTO>(regiondata);
            return Ok(mapper.Map<RegionsDTO>(regiondata));
        }
    }
}

