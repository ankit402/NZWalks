using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public SQLRegionRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await applicationDbContext.regions.AddAsync(region);
            await applicationDbContext.SaveChangesAsync();
            return region;// throw new NotImplementedException();
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var existingRegion = await applicationDbContext.regions.FindAsync(id);
            if(existingRegion == null)
            {
                return null;
            }
            applicationDbContext.regions.Remove(existingRegion);
            applicationDbContext.SaveChangesAsync();
            return existingRegion;
            // throw new NotImplementedException();
        }
        //Filter & Sorting
        public async Task<List<Region>> GetAllRegionsAsync(string? Filteron =null  , string FilterQuery= null, string? SortBy= null, bool IsAscending = true)
        {
            var region = applicationDbContext.regions.AsQueryable();
            // Filtering
            if(string.IsNullOrWhiteSpace(Filteron) == false && string.IsNullOrWhiteSpace(FilterQuery) == false)
            {
                if(Filteron.Equals("Name", StringComparison.OrdinalIgnoreCase)){

                    region = region.Where(x => x.Name.Contains(FilterQuery));
                    return await region.ToListAsync();
                }
            }

            //Sorting 
            if (string.IsNullOrWhiteSpace(SortBy) == false)
            {
                if (Filteron.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {

                    region = IsAscending ? region.OrderBy(x=> x.Name) : region.OrderByDescending(x=> x.Name);
                    return await region.ToListAsync();
                }
            }


            return await applicationDbContext.regions.ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            return await applicationDbContext.regions.FirstOrDefaultAsync(x => x.Guid == id);
            //throw new NotImplementedException();
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var existingregion = await applicationDbContext.regions.FirstOrDefaultAsync(x => x.Guid == id);
            if (existingregion != null)
            {
                return null;
            }
            existingregion.Name = region.Name;
            existingregion.Code = region.Code;
            existingregion.RegionImageurl = region.RegionImageurl;
            await applicationDbContext.SaveChangesAsync();
            return existingregion;
            //throw new NotImplementedException();
        }
    }
}
