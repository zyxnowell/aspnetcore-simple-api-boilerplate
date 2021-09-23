using Microsoft.EntityFrameworkCore;
using Simple.Intrastructure.Entities;

namespace Simple.Intrastructure.Context
{ 
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }

        public DbSet<SampleEntity> SampleEntity { get; set; }
    }
}
