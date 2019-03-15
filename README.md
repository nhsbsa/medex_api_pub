
# Medical Examiner API

## Testing and using the API in Development

The following is just WIP while we integrate OKTA:

1. Open the project and launch using IIS Express
1. Navigate to `/swagger` if it doesn't do it by default
1. Find the `/auth/authenticate` method and execute it to retrieve your bearer token; copy it.
1. Locate the *Authorise* button at the top of the swagger page with the open lock
1. Enter `bearer ` followed by your token.
1. You should now be able to execute the other API endpoints

## Setting up your Development Environment

### Setting up the API Keys

Before you can use the API we need to add a key to the `appsettings.json` file; If you're doing this locally on your development machine you should add it to `appsettings.Development.json`;

```json
{
    "Authentication": {
        "Secret": "EXAMPLE SECRET KEY USED FOR DEVELOPMENT"
    },
}
```

##Loading locations data into a CosmosDB

Locations data needs to be loaded into a CosmosDB database.

There is a json file in the Source_Data folder that contains a list of locations to be loaded.

Loading can be facilitated by using the MedicalExaminers.ReferenceDataLoader. To do this:

Navigate to [Project directory]\MedicalExaminer.ReferenceDataLoader\bin\Release\netcoreapp2.1\publish

In this folder there is a file RunMedicalExaminerReferenceDataLoader.bat

Edit this file to set the following:
endpoint of Cosmos DB 
primary Key
databaseID
json file to import (with full path)
containerId