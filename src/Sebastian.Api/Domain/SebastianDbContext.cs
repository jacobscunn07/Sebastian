using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sebastian.Api.Domain.Maps;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain
{
    public class SebastianDbContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;
        
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

        public async Task<T> RunTransaction<T>(Func<T> action)
        {
            try
            {
                BeginTransaction();
                var result = action();
                await EndTransaction();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await EndTransaction(e);
                throw;
            }
        }

        public void RunTransaction(Action action)
        {
            try
            {
                BeginTransaction();
                action();
                EndTransaction();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                EndTransaction(e);
                throw;
            }
        }

        private void BeginTransaction()
        {
            if (_currentTransaction != null)
                return;

            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        private Task EndTransaction(Exception exception = null)
        {
            try
            {
                if (exception != null)
                {
                    _currentTransaction.Rollback();
                    return Task.CompletedTask;
                }

                SaveChanges();

                _currentTransaction.Commit();
            }
            catch (Exception ex)
            {
                _currentTransaction.Rollback();
                throw;
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }

            return Task.CompletedTask;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseMap());
            modelBuilder.ApplyConfiguration(new ExerciseTypeAttributeMap());
            modelBuilder.ApplyConfiguration(new ExerciseTypeExerciseTypeAttributeMap());
            modelBuilder.ApplyConfiguration(new ExerciseTypeMap());
            modelBuilder.ApplyConfiguration(new WorkoutMap());
            modelBuilder.ApplyConfiguration(new WorkoutSupersetMap());
            modelBuilder.ApplyConfiguration(new WorkoutSupersetExerciseMap());
            modelBuilder.ApplyConfiguration(new WorkoutSupersetExerciseSetMap());
            modelBuilder.ApplyConfiguration(new WorkoutSupersetExerciseSetAttributeMap());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
