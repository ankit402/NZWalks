
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        // Dbset 
        public DbSet<Region> regions { get; set; }
        public DbSet<Difficulty> difficulty { get; set; }
        public DbSet<Walk> walks { get; set; }
    }
}
