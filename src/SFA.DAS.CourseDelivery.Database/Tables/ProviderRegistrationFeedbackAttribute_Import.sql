CREATE TABLE [dbo].[ProviderRegistrationFeedbackAttribute_Import]
(
	[UkPrn] INT PRIMARY KEY,
	[AttributeName] VARCHAR(100) NOT NULL,
	[Weakness] INT NOT NULL DEFAULT 0,
	[Strength] INT NOT NULL DEFAULT 0,
)
GO
 