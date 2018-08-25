CREATE TABLE [User] (
  Id UNIQUEIDENTIFIER NOT NULL
    CONSTRAINT PK_User_Id PRIMARY KEY
    CONSTRAINT DF_User_Id DEFAULT NEWID()
  , GivenName VARCHAR(128) NOT NULL 
  , Surname VARCHAR(128) NOT NULL 
)

CREATE TABLE Workout (
	Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_Workout_Id PRIMARY KEY
		CONSTRAINT DF_Workout_Id DEFAULT NEWID()
  , UserId UNIQUEIDENTIFIER NOT NULL
	, [Name] VARCHAR(128) NOT NULL
	, DateTimeBegan DATETIME2 NOT NULL
	  CONSTRAINT DF_Workout_DateTimeBegan DEFAULT CURRENT_TIMESTAMP 
	, DateTimeFinished DATETIME2
	, CONSTRAINT FK_UserWorkout_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

CREATE TABLE WorkoutSuperset (
	Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_WorkoutSuperset_Id PRIMARY KEY
		CONSTRAINT DF_WorkoutSuperset_Id DEFAULT NEWID()
  , WorkoutId UNIQUEIDENTIFIER NOT NULL
  , Sequence SMALLINT NOT NULL
  , CONSTRAINT FK_WorkoutSuperset_WorkoutId FOREIGN KEY (WorkoutId) REFERENCES Workout(Id)
)

CREATE TABLE WorkoutSupersetExercise (
	Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_WorkoutSupersetExercise_Id PRIMARY KEY
		CONSTRAINT DF_WorkoutSupersetExercise_Id DEFAULT NEWID()
	, ExerciseId UNIQUEIDENTIFIER NOT NULL 
  , WorkoutSupersetId UNIQUEIDENTIFIER NOT NULL
  , Sequence SMALLINT NOT NULL
  , CONSTRAINT FK_WorkoutSupersetExercise_ExerciseId FOREIGN KEY (ExerciseId) REFERENCES Exercise(Id)
  , CONSTRAINT FK_WorkoutSupersetExercise_WorkoutSupersetId FOREIGN KEY (WorkoutSupersetId) REFERENCES WorkoutSuperset(Id)
)

CREATE TABLE WorkoutSupersetExerciseSet (
	Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_WorkoutSupersetExerciseSet_Id PRIMARY KEY
		CONSTRAINT DF_WorkoutSupersetExerciseSet_Id DEFAULT NEWID()
	, WorkoutSupersetExerciseId UNIQUEIDENTIFIER NOT NULL 
  , Sequence SMALLINT NOT NULL
  , CONSTRAINT FK_WorkoutSupersetExerciseSet_WorkoutSupersetExerciseId FOREIGN KEY (WorkoutSupersetExerciseId) REFERENCES WorkoutSupersetExercise(Id)
)

CREATE TABLE WorkoutSupersetExerciseSetAttribute (
	Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_WorkoutExerciseSetAttribute_Id PRIMARY KEY
		CONSTRAINT DF_WorkoutExerciseSetAttribute_Id DEFAULT NEWID()
  , WorkoutSupersetExerciseSetId UNIQUEIDENTIFIER NOT NULL
    FOREIGN KEY REFERENCES WorkoutSupersetExerciseSet(Id)
  , [Name] VARCHAR(128) NOT NULL
  , Value VARCHAR(128)
  , CONSTRAINT FK_WorkoutSupersetExerciseSetAttribute_WorkoutSupersetExerciseSetId FOREIGN KEY (WorkoutSupersetExerciseSetId) REFERENCES WorkoutSupersetExerciseSet(Id)
)
