# Project Title

Directory Watcher Application using ASP.NET Web REST API.

# How this project is build?

This project is build using ASP.NET Core and EntityFramework Core.

# Database

* This application database schema is in Schema_Design.pdf file.
* All the tables are handled using SQLite database file named FileDetail.db.

# Directory Location

* You can specify the path location you required to watch in PathLocation.txt.

## Prerequirements

* Visual Studio 2019 or above
* .NET Core SDK version 5.0


## Visual Studio Framework Requirementds

* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.EntityFrameworkCore.Sqlite
* Microsoft.Data.Sqlite.Core
* SQLite


## How To Run

* Open solution in Visual Studio
* Set .Web project as Startup Project and build the project.
* Run the application(IIS).


## List of API

# GET
* /api/FileDetails/taskName
* /api/FileDetails/taskStatus
* /api/FileDetails/changeType
* /api/FileDetails/taskDetails
* /api/FileDetails/fileDetails
* /api/FileDetails/logging

# PUT
* /api/FileDetails/{id}

# POST
* /api/FileDetails/insertNewTaskDetails


## Step By Step Execution

* After started running the application it will direct us to the swagger API tool which is used for the API developers.
* You can see all the API listed in the swagger tool.
* You can add the new task using the post method mentioned above to start or stop the application.
* Once the application is started it will watch for the specified path location in interrupt mode which will triggers the event trigger.
* It will be captured in the database and can be viewed in get api call.
* All the log related to this application also logged in the database.










