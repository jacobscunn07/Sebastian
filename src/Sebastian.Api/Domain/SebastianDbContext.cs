using Microsoft.EntityFrameworkCore;
using Sebastian.Api.Domain.Maps;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain
{
    public class SebastianDbContext : DbContext
    {
        public SebastianDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkoutMap());
        }
    }
}