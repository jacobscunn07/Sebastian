using FluentMigrator;

namespace Sebastian.Database.up
{
    [Migration(2018122902)]
    public class Migration_2018122902_CreateWorkoutTables : Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_User_Id").NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("GivenName").AsAnsiString(128).NotNullable()
                .WithColumn("Surname").AsAnsiString(128).NotNullable();

            Create.Table("Workout")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_Workout_Id").NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey(
                    "FK_UserWorkout_UserId", "User", "Id")
                .WithColumn("Name").AsAnsiString(128).NotNullable()
                .WithColumn("DateTimeBegan").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("DateTimeFinished").AsDateTime2().Nullable();

            Create.Table("WorkoutSuperset")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_WorkoutSuperset_Id").NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("WorkoutId").AsGuid().NotNullable().ForeignKey(
                    "FK_WorkoutSuperset_WorkoutId", "Workout", "Id")
                .WithColumn("Sequence").AsInt16().NotNullable();
            
            Create.Table("WorkoutSupersetExercise")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_WorkoutSupersetExercise_Id").NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("ExerciseId").AsGuid().NotNullable().ForeignKey(
                    "FK_WorkoutSupersetExercise_ExerciseId", "Exercise", "Id")
                .WithColumn("WorkoutSupersetId").AsGuid().NotNullable().ForeignKey(
                    "FK_WorkoutSupersetExercise_WorkoutSupersetId", "WorkoutSuperset", "Id")
                .WithColumn("Sequence").AsInt16().NotNullable();
            
            Create.Table("WorkoutSupersetExerciseSet")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_WorkoutSupersetExerciseSet_Id").NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("WorkoutSupersetExerciseId").AsGuid().NotNullable().ForeignKey(
                    "FK_WorkoutSupersetExerciseSet_WorkoutSupersetExerciseId", "WorkoutSupersetExercise", "Id")
                .WithColumn("Sequence").AsInt16().NotNullable();
            
            Create.Table("WorkoutSupersetExerciseSetAttribute")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_WorkoutExerciseSetAttribute_Id").NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("WorkoutSupersetExerciseSetId").AsGuid().NotNullable().ForeignKey(
                    "FK_WorkoutSupersetExerciseSetAttribute_WorkoutSupersetExerciseSetId", "WorkoutSupersetExerciseSet", "Id")
                .WithColumn("Name").AsAnsiString(128).NotNullable()
                .WithColumn("Value").AsAnsiString(128);
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}