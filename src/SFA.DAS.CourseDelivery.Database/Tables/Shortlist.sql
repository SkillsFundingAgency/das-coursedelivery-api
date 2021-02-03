CREATE TABLE [dbo].[Shortlist]
(
	[Id] UniqueIdentifier NOT NULL PRIMARY KEY, 
    [ShortlistUserId] UNIQUEIDENTIFIER NOT NULL, 
    [ProviderUkprn] INT NOT NULL, 
    [CourseId] INT NOT NULL, 
    [LocationId] INT NULL
)
