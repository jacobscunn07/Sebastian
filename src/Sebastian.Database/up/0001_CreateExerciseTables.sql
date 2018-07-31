-- Reps
-- Pounds
CREATE TABLE ExerciseTypeAttribute (
  Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_ExerciseTypeAttribute_Id PRIMARY KEY
		CONSTRAINT DF_ExerciseTypeAttribute_Id DEFAULT NEWID()
  , [Name] VARCHAR(128) NOT NULL
)

-- Weighted Reps
-- Assisted Reps
-- Distance
CREATE TABLE ExerciseType (
  Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_ExerciseType_Id PRIMARY KEY
		CONSTRAINT DF_ExerciseType_Id DEFAULT NEWID()
  , [Name] VARCHAR(128) NOT NULL
)

CREATE TABLE ExerciseType_ExerciseTypeAttribute (
  Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_ExerciseTypeExerciseTypeAttribute_Id PRIMARY KEY
		CONSTRAINT DF_ExerciseTypeExerciseTypeAttributes_Id DEFAULT NEWID()
  , ExerciseTypeId UNIQUEIDENTIFIER NOT NULL 
  , ExerciseTypeAttributeId UNIQUEIDENTIFIER NOT NULL 
  , CONSTRAINT FK_ExerciseTypeExerciseTypeAttribute_ExerciseTypeId FOREIGN KEY (ExerciseTypeId) REFERENCES ExerciseType(Id)
  , CONSTRAINT FK_ExerciseTypeExerciseTypeAttribute_ExerciseTypeAttributeId FOREIGN KEY (ExerciseTypeAttributeId) REFERENCES ExerciseTypeAttribute(Id)
  , CONSTRAINT UC_ExerciseType_ExerciseTypeAttribute_ExerciseTypeId_ExerciseTypeAttributeId UNIQUE (ExerciseTypeId, ExerciseTypeAttributeId)
)

-- Bench Press
CREATE TABLE Exercise (
  Id UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT PK_Exercise_Id PRIMARY KEY
		CONSTRAINT DF_Exercise_Id DEFAULT NEWID()
  , ExerciseTypeId UNIQUEIDENTIFIER NOT NULL 
  , [Name] VARCHAR(128) NOT NULL
  , CONSTRAINT FK_Exercise_ExerciseTypeId FOREIGN KEY (ExerciseTypeId) REFERENCES ExerciseType(Id)
)
