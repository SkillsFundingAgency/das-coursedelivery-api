CREATE TABLE [dbo].[ProviderRegistration_Import]
(
	[UkPrn] INT PRIMARY KEY,
	[StatusDate] [datetime] NOT NULL,
    [StatusId] INT NOT NULL, 
	[ProviderTypeId] INT NOT NULL,
	[OrganisationTypeId] int NOT NULL,
	[FeedbackTotal] INT NOT NULL DEFAULT 0,
	[Address1] VARCHAR(500) NULL,
	[Address2] VARCHAR(500) NULL,
	[Address3] VARCHAR(500) NULL,
	[Address4] VARCHAR(500) NULL,
	[Town] VARCHAR(500) NULL,
	[Postcode] VARCHAR(20) NULL,
	[Lat] FLOAT NOT NULL DEFAULT 0,
	[Long] FLOAT NOT NULL DEFAULT 0,
	[LegalName] VARCHAR(1000) NULL
)
GO
 