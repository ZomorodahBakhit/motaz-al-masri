using Microsoft.EntityFrameworkCore;
using University.Data.Configurations;
using University.Data.Entities;

namespace University.Data
{
    public class universityDbContext : DbContext
    {
        public universityDbContext(DbContextOptions<universityDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
