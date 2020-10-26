﻿CREATE TABLE [dbo].[Provider]
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

CREATE NONCLUSTERED INDEX [IDX_Provider_Ukprn] ON [dbo].[Provider] (Ukprn) 
INCLUDE (Id, [Name], TradingName,  NationalProvider, EmployerSatisfaction, LearnerSatisfaction, Email, Phone, Website, PostCode, Lat, Long) WITH (ONLINE = ON) 
GO 