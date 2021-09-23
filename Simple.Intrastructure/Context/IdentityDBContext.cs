using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Simple.Intrastructure.Entities;

namespace Simple.Intrastructure.Context
{

    public class IdentityDBContext : IdentityDbContext<AppUser>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
