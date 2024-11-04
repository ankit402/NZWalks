using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.DTO;
using NZWalksAPI.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // WebAPI Tutorial Section 1 to Section 3 completed till CRUD function 
        public readonly ApplicationDbContext applicationDbContext;
        public RegionsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        /// <summary>
        /// GetAllRegions : Get All data by  calling this method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var dbGetAllRegion = applicationDbContext.regions.ToList();
            var regionDTO = new List<RegionsDTO>();
            foreach (var region in dbGetAllRegion)
            {
                regionDTO.Add(new RegionsDTO()
                {

                    Guid = region.Guid,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageurl = region.RegionImageurl,
                });
            }
            return Ok(dbGetAllRegion);
        }
        /// <summary>
        /// GetRegionbyId will populate data which is matching the reqeust ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetRegionbyId([FromRoute] Guid id)
        {
            //var dbGetRegionbyId = applicationDbContext.regions.Find(id);
            var dbGetRegionbyfirstDefault = applicationDbContext.regions.FirstOrDefault(x => x.Guid == id);
            if (dbGetRegionbyfirstDefault == null)
            {
                return NotFound();
            }
            //return Ok(dbGetRegionbyId); ;
            return Ok(dbGetRegionbyfirstDefault);
        }
        /// <summary>
        /// AddRegion : Add Region data which is  coming from the Request Body
        /// </summary>
        /// <param name="addRegionRequestDTO"></param>
        /// <returns></returns>
        [HttpPost("AddRegions")]
        public IActionResult AddRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or convert the RegiondDTO into  Domain Model
            var regionDM = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageurl = addRegionRequestDTO?.RegionImageurl,
            };
            // Use DBcontext to add DomainModel
            applicationDbContext.Add(regionDM);
            // commit  into  the database
            applicationDbContext.SaveChanges();
            // map domainmodel  again into DTO show to Client only 
            var mapDTO = new RegionsDTO
            {
                Guid = regionDM.Guid,
                Code = regionDM.Code,
                Name = regionDM.Name,
                RegionImageurl = regionDM.RegionImageurl
            };
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
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionmodelid = applicationDbContext.regions.FirstOrDefault(x => x.Guid == id);
            if (regionmodelid == null) { return NotFound(); }
            // DTO to Domain model 
            regionmodelid.Name = updateRegionRequestDTO.Name;
            regionmodelid.Code = updateRegionRequestDTO.Code;
            regionmodelid.RegionImageurl = updateRegionRequestDTO.RegionImageurl;
            applicationDbContext.SaveChanges();
            // Domain model to DTO
            var RegionDTo = new RegionsDTO
            {
                Guid = regionmodelid.Guid,
                Code = regionmodelid.Code,
                Name = regionmodelid.Name,
                RegionImageurl = regionmodelid.RegionImageurl
            };
            // Returning DTO to the client view
            return Ok(RegionDTo);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regiondata = applicationDbContext.regions.FirstOrDefault(x => x.Guid == id);
            if (regiondata == null)
            {
                return NotFound();
            }
            applicationDbContext.regions.Remove(regiondata);
            applicationDbContext.SaveChanges();

            // in case we need to show deleted region from dto 
            var RegionDTo = new RegionsDTO
            {
                Guid = regiondata.Guid,
                Code = regiondata.Code,
                Name = regiondata.Name,
                RegionImageurl = regiondata.RegionImageurl
            };
            return Ok(RegionDTo);
        }
    }
}

