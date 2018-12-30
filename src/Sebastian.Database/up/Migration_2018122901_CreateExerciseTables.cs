using FluentMigrator;

namespace Sebastian.Database.up
{
    [Migration(2018122901)]
    public class Migration_2018122901_CreateExerciseTables : Migration
    {
        public override void Up()
        {
            Create.Table("ExerciseTypeAttribute")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_ExerciseTypeAttribute_Id").NotNullable()
                .WithColumn("Name").AsAnsiString(128).NotNullable();
            
            Create.Table("ExerciseType")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_ExerciseType_Id").NotNullable()
                .WithColumn("Name").AsAnsiString(128).NotNullable();

            Create.Table("ExerciseType_ExerciseTypeAttribute")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_ExerciseTypeExerciseTypeAttribute_Id").NotNullable()
                .WithColumn("ExerciseTypeId").AsGuid().NotNullable().ForeignKey(
                    "FK_ExerciseTypeExerciseTypeAttribute_ExerciseTypeId", "ExerciseType", "Id")
                .WithColumn("ExerciseTypeAttributeId").AsGuid().NotNullable().ForeignKey(
                    "FK_ExerciseTypeExerciseTypeAttribute_ExerciseTypeAttributeId", "ExerciseTypeAttribute", "Id");
            
            Create.UniqueConstraint("UC_ExerciseType_ExerciseTypeAttribute_ExerciseTypeId_ExerciseTypeAttributeId")
                .OnTable("ExerciseType_ExerciseTypeAttribute")
                .Columns("ExerciseTypeId", "ExerciseTypeAttributeId");

            Create.Table("Exercise")
                .WithColumn("Id").AsGuid().PrimaryKey("PK_Exercise_Id").NotNullable()
                .WithColumn("ExerciseTypeId").AsGuid().NotNullable().ForeignKey(
                    "FK_Exercise_ExerciseTypeId", "ExerciseType", "Id")
                .WithColumn("Name").AsAnsiString(128).NotNullable();
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}