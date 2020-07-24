CREATE TABLE [dbo].[Provider_Import]
(
	[Id] BIGINT PRIMARY KEY,
	[UkPrn] INT NOT NULL,
	[Name] VARCHAR(1000) NOT NULL,
	[TradingName] VARCHAR(1000) NOT NULL,
	[NationalProvider] BIT NOT NULL DEFAULT 0,
	[MaxEmployerLevyCap] INT NOT NULL,
	[EmployerSatisfaction] DECIMAL NOT NULL DEFAULT 0,
	[LearnerSatisfaction] DECIMAL NOT NULL DEFAULT 0,
	[Email] VARCHAR(256) NOT NULL,
	[Phone] VARCHAR(50) NOT NULL,
	[Website] VARCHAR(500) NOT NULL,
)
GO