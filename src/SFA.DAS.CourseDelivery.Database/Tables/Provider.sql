CREATE TABLE [dbo].[Provider]
(
	[Id] BIGINT PRIMARY KEY,
	[UkPrn] INT NOT NULL,
	[Name] VARCHAR(1000) NOT NULL,
	[TradingName] VARCHAR(1000) NULL,
	[NationalProvider] BIT NOT NULL DEFAULT 0,
	[EmployerSatisfaction] DECIMAL NOT NULL DEFAULT 0,
	[LearnerSatisfaction] DECIMAL NOT NULL DEFAULT 0,
	[Email] VARCHAR(256) NULL,
	[Phone] VARCHAR(50) NULL,
	[Website] VARCHAR(500) NULL,
)
GO