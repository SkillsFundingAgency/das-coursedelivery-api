CREATE TABLE [dbo].[ProviderRegistration]
(
	[UkPrn] INT PRIMARY KEY,
	[StatusDate] [datetime] NOT NULL,
    [StatusId] INT NOT NULL, 
	[ProviderTypeId] INT NOT NULL,
	[OrganisationTypeId] int NOT NULL
)
GO

