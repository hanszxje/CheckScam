using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CheckScam.Models
{
    public class CheckScamDbContext : IdentityDbContext<IdentityUser>
    {
        public CheckScamDbContext(DbContextOptions<CheckScamDbContext> options)
            : base(options)
        {
        }

        public DbSet<ScamPost> ScamPosts { get; set; }
        public DbSet<ScamImage> ScamImages { get; set; }
    }
}