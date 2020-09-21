CREATE TABLE [dbo].[ProviderRegistrationFeedbackAttribute]
(
	[UkPrn] INT NOT NULL,
	[AttributeName] VARCHAR(100) NOT NULL,
	[Weakness] INT NOT NULL DEFAULT 0,
	[Strength] INT NOT NULL DEFAULT 0,
	CONSTRAINT PK_ProviderRegistrationFeedbackAttribute PRIMARY KEY CLUSTERED ([UkPrn], [AttributeName])
)
GO
 