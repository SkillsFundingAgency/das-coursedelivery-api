CREATE TABLE [dbo].[ProviderRegistrationFeedbackRating]
(
	[UkPrn] INT NOT NULL,
	[FeedbackName] VARCHAR(100) NOT NULL,
	[FeedbackCount] INT NOT NULL DEFAULT 0,
	CONSTRAINT PK_ProviderRegistrationFeedbackRating PRIMARY KEY CLUSTERED ([UkPrn], [FeedbackName])
)
GO
 
  
CREATE NONCLUSTERED INDEX [IDX_ProviderRegistrationFeedbackRating_Ukprn] ON [dbo].[ProviderRegistrationFeedbackRating] (Ukprn) 
INCLUDE (FeedbackName, FeedbackCount) WITH (ONLINE = ON) 
GO 