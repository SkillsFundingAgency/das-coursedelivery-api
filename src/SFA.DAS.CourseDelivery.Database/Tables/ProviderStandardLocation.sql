CREATE TABLE [dbo].[ProviderStandardLocation]
(
	[StandardId] INT NOT NULL,
	[UkPrn] INT NOT NULL,
	[LocationId] INT NOT NULL,
	[DeliveryModes] VARCHAR(256) NOT NULL,
	[Radius] DECIMAL NOT NULL DEFAULT 0,
	[National] BIT NOT NULL DEFAULT 0,
	CONSTRAINT PK_ProviderStandardLocation PRIMARY KEY CLUSTERED ([StandardId], [Ukprn], [LocationId])
)
GO

CREATE NONCLUSTERED INDEX [IDX_ProviderStandardLocation_Ukprn_StandardId] ON [dbo].[ProviderStandardLocation] (StandardId, UkPrn) 
INCLUDE (LocationId, DeliveryModes, Radius, [National]) WITH (ONLINE = ON) 
GO 

CREATE NONCLUSTERED INDEX [IDX_ProviderStandardLocation_Location] ON [dbo].[ProviderStandardLocation] (LocationId) 
INCLUDE (StandardId, UkPrn, DeliveryModes, Radius, [National]) WITH (ONLINE = ON) 
GO 