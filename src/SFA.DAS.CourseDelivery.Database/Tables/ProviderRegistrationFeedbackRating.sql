CREATE TABLE [dbo].[ProviderRegistrationFeedbackRating]
(
	[UkPrn] INT PRIMARY KEY,
	[FeedbackName] VARCHAR(100) NOT NULL,
	[FeedbackCount] INT NOT NULL DEFAULT 0,
)
GO
 