using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{
    public class NZAuthDBContext  : IdentityDbContext
    {
        public NZAuthDBContext(DbContextOptions<NZAuthDBContext> dbcontext ) : base( dbcontext )
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "a72f5152-5cb7-47db-9e13-c8a9ff536029";
            var WriterRoleId = "a82e5172-5cb8-47eb-9d13-c0532af536029";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name= "Reader",
                    NormalizedName = "READER"
                },
                 new IdentityRole
                {
                    Id = WriterRoleId,
                    ConcurrencyStamp=WriterRoleId,
                    Name= "Writer",
                    NormalizedName = "WRITER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
