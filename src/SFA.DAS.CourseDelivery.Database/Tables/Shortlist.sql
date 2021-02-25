CREATE TABLE [dbo].[Shortlist]
(
    [Id] UniqueIdentifier NOT NULL PRIMARY KEY,
    [ShortlistUserId] UNIQUEIDENTIFIER NOT NULL,
    [UkPrn] INT NOT NULL,
    [StandardId] INT NOT NULL,
    [CourseSector] VARCHAR(1000) NOT NULL,
    [LocationDescription] VARCHAR(1000) NULL,
    [Lat] FLOAT NULL,
    [Long] FLOAT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE()
)
GO

CREATE NONCLUSTERED INDEX [IDX_Shortlist_UserItems] ON [dbo].[Shortlist] (ShortlistUserId) WITH (ONLINE=ON)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IDX_Shortlist_Item] ON [dbo].[Shortlist] (ShortlistUserId,UkPrn,StandardId,lat,long)
    INCLUDE (Id, CourseSector, LocationDescription, CreatedDate) WITH (ONLINE = ON)
GO 