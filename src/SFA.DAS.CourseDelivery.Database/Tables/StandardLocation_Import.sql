﻿CREATE TABLE [dbo].[StandardLocation_Import]
(
	[LocationId] INT PRIMARY KEY,
	[Name] VARCHAR(250) NOT NULL,
	[Email] VARCHAR(256) NULL,
	[Website] VARCHAR(256) NULL,
	[Phone] VARCHAR(50) NULL,
	[Address1] VARCHAR(250) NOT NULL,
	[Address2] VARCHAR(250) NULL,
	[Town] VARCHAR(250) NULL,
	[Postcode] VARCHAR(25) NULL,
	[County] VARCHAR(250) NULL,
	[Lat] FLOAT NOT NULL DEFAULT 0,
	[Long] FLOAT NOT NULL DEFAULT 0,
)
GO