CREATE TABLE [dbo].[Provider]
(
	[UkPrn] INT PRIMARY KEY NOT NULL,
	[Name] VARCHAR(1000) NOT NULL,
	[TradingName] VARCHAR(1000) NULL,
	[EmployerSatisfaction] DECIMAL NULL,
	[LearnerSatisfaction] DECIMAL NULL,
	[Email] VARCHAR(256) NULL,
	[Phone] VARCHAR(50) NULL,
	[Website] VARCHAR(500) NULL,
	[MarketingInfo] VARCHAR(MAX) NULL,	
)
GO
