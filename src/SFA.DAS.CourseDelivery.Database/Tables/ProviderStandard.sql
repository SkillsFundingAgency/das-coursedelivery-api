CREATE TABLE [dbo].[ProviderStandard]
(
	[StandardId] INT NOT NULL,
	[UkPrn] INT NOT NULL,
	[StandardInfoUrl] VARCHAR(1000) NULL,
	[Email] VARCHAR(256) NULL,
	[Phone] VARCHAR(50) NULL,
	[ContactUrl] VARCHAR(500) NULL,
	CONSTRAINT PK_ProviderStandard PRIMARY KEY CLUSTERED ([StandardId], [Ukprn])
)
GO

CREATE NONCLUSTERED INDEX [IDX_ProviderStandard_StandardId] ON [dbo].[ProviderStandard] (StandardId) 
INCLUDE (UkPrn, StandardInfoUrl, Email,  Phone, ContactUrl) WITH (ONLINE = ON) 
GO 