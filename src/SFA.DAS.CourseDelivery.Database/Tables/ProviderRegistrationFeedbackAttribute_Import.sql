CREATE TABLE [dbo].[ProviderRegistrationFeedbackAttribute_Import]
(
	[UkPrn] INT NOT NULL,
	[AttributeName] VARCHAR(100) NOT NULL,
	[Weakness] INT NOT NULL DEFAULT 0,
	[Strength] INT NOT NULL DEFAULT 0,
	CONSTRAINT PK_ProviderRegistrationFeedbackAttribute_Import PRIMARY KEY CLUSTERED ([UkPrn], [AttributeName])
)
GO
 