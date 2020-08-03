CREATE TABLE [dbo].[ProviderStandardLocation]
(
	[StandardId] INT NOT NULL,
	[UkPrn] INT NOT NULL,
	[LocationId] INT NOT NULL,
	[DeliveryModes] VARCHAR(256) NOT NULL,
	[Radius] DECIMAL NOT NULL DEFAULT 0,
	CONSTRAINT PK_ProviderStandardLocation PRIMARY KEY CLUSTERED ([StandardId], [Ukprn], [LocationId])
)
GO

