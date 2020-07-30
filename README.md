# das-coursedelivery-api

[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/das-coursedelivery-api?branchName=master)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=2243&branchName=master)


## Requirements

- DotNet Core 3.1 and any supported IDE for DEV running.
- *If you are not wishing to run the in memory database then*
- SQL Server database.
- Azure Storage Account

## About

das-coursedelivery-api represents the inner api definition for provider course delivery, with data taken from Course Directory. The API creates a local copy of the data for querying over.

## Local running

**Do not run in IISExpress**

### In memory database
It is possible to run the whole of the API using the InMemory database. To do this the environment variable in appsettings.json should be set to **DEV**. 
Once done, start the application as normal, then run the ```ops/dataload``` operation in swagger. You will then be able to query the API
as per the operations listed in swagger. Running in this mode will only have a subset of data as shown in the ```courses.json``` file

### SQL Server database
You are able to run the API by doing the following:

* Run the database deployment publish command to create the database ```SFA.DAS.CourseDelivery``` or create the database manually and run in the table creation scripts
* Request APIM key and URL from Course Directory
* In your Azure Storage Account, create a table called Configuration and Add the following
```
ParitionKey: LOCAL
RowKey: SFA.DAS.CourseDelivery.Api_1.0
data: {"CourseDeliveryConfiguration":{"ConnectionString":"DBCONNECTIONSTRING","CourseDirectoryConfiguration":{"Url":"CourseDirectoryBulkUrl","Key":"ApimKey"}}}
```

* Start the api project ```SFA.DAS.CourseDelivery.Api```

Sending the following to the API

```POST /ops/dataload```

will load data from Course Directory into the local database.

Starting the API will then show the swagger definition with the available operations.