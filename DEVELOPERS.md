# User API Dev Guide


### Table of contents 
1. Controller Layer
2. Service Layer
3. Logging System
4. Database
5. Repository Layer 
6. Testing
7. Future improvements


## 1. Controller Layer
The endpoints are exposed in this layer. <br>
User controller and Account controller have both been implemented for their respective purposes.

## 2. Service Layer
This layer contains bulk of validation logic and talking to the repository layer to retrieve or insert into the database.
UserService and AccountService have both been implemented for their respective purposes.

## 3. Logging System
A simple logging system has been implemented to log errors into a text file. <br> 
For future work, we could implement Serilog or `Microsoft.Extensions.Logging.ApplicationInsights` to push the logs to Azure/ the cloud. 

## 4. Database
It might be obvious that I'm not very confident in the database area. <br>
Database is a mssql database which is set up with a lot of help from `https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0` and other various online guides <br>
With the Db Context I was thinking of implementing multiple contextes to split things up however I then came across `https://stackoverflow.com/questions/11197754/entity-framework-one-database-multiple-dbcontexts-is-this-a-bad-idea` which led me to change my mind and go ahead with one db context.


## 5. Repository Layer
Repository layer utilizes EF Core as an ORM to read from and update the database.
At this moment, only the service layer will call the repository layer. <br>
The repository folder also contains the models because since they're used very closely with this layer/ DB

## 6. Testing
I've implemented some simple unit tests for the UserController and the UserService

## 7. Future improvements
Some places for future improves are: 
<br> 1. More testing could be implemented e.g unit tests for AccountController + AccountService, integration tests and post deployment checks<br> 2. Database improvements to utilize EF Core's auto generated migration scripts for the future <br> 3. Logging system improvement to be something more powerful and maybe cloud based <br> 4. Caching system potentially? Might need to have a discussion around business requirements to see if caching could be helpful or not.<br>5. Potentially split the Database logic into another project so the WebApi project doesn't get too cluttered
