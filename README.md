# das-coursedelivery-api

[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/das-coursedelivery-api?branchName=master)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=2243&branchName=master)


## Requirements

- DotNet Core 3.1 and any supported IDE for DEV running.
- SQL Server database.
- Azure Storage Account

## About

das-coursedelivery-api represents the inner api definition for provider course delivery and RoATP, with data taken from Course Directory. The API creates a local copy of the data for querying over.

## Local running

**Do not run in IISExpress**

The solution will run in a **DEV** mode, however due to the use of SQL statements in the data context not all of the operations on the swagger page will be available.

### SQL Server database
You are able to run the API by doing the following:

* Run the database deployment publish command to create the database ```SFA.DAS.CourseDelivery``` or create the database manually and run in the table creation scripts
* Request APIM key and URL from Course Directory
* Request managed identity access to RoATP service api

 *note if you do not have access to Course Directory or RoATP then use the* ```"UseLocalData": "true"``` *setting in appsettings.json to provide a subset of data*   
* In your Azure Storage Account, create a table called Configuration and Add the following
```
ParitionKey: LOCAL
RowKey: SFA.DAS.CourseDelivery.Api_1.0
Data: {"CourseDeliveryConfiguration":{"ConnectionString":"DBCONNECTIONSTRING"},"CourseDirectoryConfiguration":{"Url":"CourseDirectoryBulkUrl","Key":"ApimKey"}}
```

* Start the api project ```SFA.DAS.CourseDelivery.Api```

Sending the following to the API

```POST /ops/dataload```

will load data from Course Directory into the local database.

Starting the API will then show the swagger definition with the available operations.