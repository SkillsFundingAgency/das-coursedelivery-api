CREATE TABLE [dbo].[ProviderStandard_Import]
(
	[StandardId] INT NOT NULL,
	[UkPrn] INT NOT NULL,
	[StandardInfoUrl] VARCHAR(1000) NOT NULL,
	[Email] VARCHAR(256) NOT NULL,
	[Phone] VARCHAR(50) NOT NULL,
	[ContactUrl] VARCHAR(500) NOT NULL,
	CONSTRAINT PK_ProviderStandardImport PRIMARY KEY CLUSTERED ([StandardId], [Ukprn])
)
GO

