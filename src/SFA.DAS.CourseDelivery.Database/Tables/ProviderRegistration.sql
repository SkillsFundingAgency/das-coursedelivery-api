CREATE TABLE [dbo].[ProviderRegistration]
(
	[UkPrn] INT NOT NULL,
	[StatusDate] [datetime] NOT NULL,
    [StatusId] INT NOT NULL, 
	[ProviderTypeId] INT NOT NULL,
	[OrganisationTypeId] int NOT NULL
)
GO

CREATE NONCLUSTERED INDEX [IDX_ProviderRegistration_Ukprn] ON [dbo].[ProviderRegistration] (UkPrn) 
INCLUDE (StatusDate, StatusId, ProviderTypeId, OrganisationTypeId) WITH (ONLINE = ON) 
GO 
