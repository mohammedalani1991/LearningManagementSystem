IF COL_LENGTH('PracticalQuestion','Main') IS NULL
BEGIN
ALTER TABLE  [dbo].[PracticalQuestion] add  [Main] [bit] NULL;
END

IF COL_LENGTH('PracticalQuestion','Description') IS NULL
BEGIN
ALTER TABLE  [dbo].[PracticalQuestion] add  [Description] [nvarchar](1024) NULL;
END

IF COL_LENGTH('PracticalQuestionTranslation','Description') IS NULL
BEGIN
ALTER TABLE  [dbo].[PracticalQuestionTranslation] add  [Description] [nvarchar](1024) NULL;
END
