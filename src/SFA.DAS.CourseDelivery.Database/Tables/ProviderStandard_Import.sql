CREATE TABLE [dbo].[ProviderStandard_Import]
(
	[StandardId] INT NOT NULL,
	[UkPrn] INT NOT NULL,
	[StandardInfoUrl] VARCHAR(1000) NULL,
	[Email] VARCHAR(256) NULL,
	[Phone] VARCHAR(50) NULL,
	[ContactUrl] VARCHAR(500) NULL,
	CONSTRAINT PK_ProviderStandardImport PRIMARY KEY CLUSTERED ([StandardId], [Ukprn])
)
GO

