CREATE TABLE [dbo].[Provider_Import]
(
	[Id] BIGINT PRIMARY KEY,
	[UkPrn] INT NOT NULL,
	[Name] VARCHAR(1000) NOT NULL,
	[TradingName] VARCHAR(1000) NULL,
	[NationalProvider] BIT NOT NULL DEFAULT 0,
	[EmployerSatisfaction] DECIMAL NULL,
	[LearnerSatisfaction] DECIMAL NULL,
	[Email] VARCHAR(256) NULL,
	[Phone] VARCHAR(50) NULL,
	[Website] VARCHAR(500) NULL,
	[Postcode] VARCHAR(20) NULL,
	[Lat] FLOAT NOT NULL DEFAULT 0,
	[Long] FLOAT NOT NULL DEFAULT 0,
)
GO