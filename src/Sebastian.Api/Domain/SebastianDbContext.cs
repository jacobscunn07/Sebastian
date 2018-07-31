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

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<ExerciseTypeAttribute> ExerciseTypeAttributes { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutSuperset> WorkoutSupersets { get; set; }
        public DbSet<WorkoutSupersetExercise> WorkoutSupersetExercises { get; set; }
        public DbSet<WorkoutSupersetExerciseSet> WorkoutSupersetExerciseSets { get; set; }
        public DbSet<WorkoutSupersetExerciseSetAttribute> WorkoutSupersetExerciseSetAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new ExerciseMap());
            modelBuilder.ApplyConfiguration(new ExerciseTypeAttributeMap());
            modelBuilder.ApplyConfiguration(new ExerciseTypeExerciseTypeAttributeMap());
            modelBuilder.ApplyConfiguration(new ExerciseTypeMap());
            modelBuilder.ApplyConfiguration(new WorkoutMap());
            modelBuilder.ApplyConfiguration(new WorkoutSupersetMap());
            modelBuilder.ApplyConfiguration(new WorkoutSupersetExerciseMap());
            modelBuilder.ApplyConfiguration(new WorkoutSupersetExerciseSetMap());
            modelBuilder.ApplyConfiguration(new WorkoutSupersetExerciseSetAttributeMap());
        }
    }
}
