CREATE TABLE [dbo].[ProviderRegistrationFeedbackAttribute]
(
	[UkPrn] INT NOT NULL,
	[AttributeName] VARCHAR(100) NOT NULL,
	[Weakness] INT NOT NULL DEFAULT 0,
	[Strength] INT NOT NULL DEFAULT 0,
	CONSTRAINT PK_ProviderRegistrationFeedbackAttribute PRIMARY KEY CLUSTERED ([UkPrn], [AttributeName])
)
GO
 
 
CREATE NONCLUSTERED INDEX [IDX_ProviderRegistrationFeedbackAttribute_Ukprn] ON [dbo].[ProviderRegistrationFeedbackAttribute] (Ukprn) 
INCLUDE (AttributeName, Weakness, Strength) WITH (ONLINE = ON) 
GO 