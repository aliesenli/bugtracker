[![Build Status](https://dev.azure.com/alirizaesenli/Bugtracker/_apis/build/status/AliEsenli.Bugtracker?branchName=master)](https://dev.azure.com/alirizaesenli/Bugtracker/_build/latest?definitionId=1&branchName=master)

<img align="left" width="40" height="40" src="https://github.com/AliEsenli/bugtracker/blob/master/Bugtracker/ClientApp/public/logo.png" alt="Bugtracker icon">

# Bugtracker
 
#### Simple Project Management tool for tracking bugs/issues in Projects.
<img src="https://github.com/AliEsenli/bugtracker/blob/master/Bugtracker/ClientApp/public/BT-tickets.png">
<img src="https://github.com/AliEsenli/bugtracker/blob/master/Bugtracker/ClientApp/public/BT-ticket.png">
<img src="https://github.com/AliEsenli/bugtracker/blob/master/Bugtracker/ClientApp/public/BT-swagger-api.png">

### Built with:
Backend:
- <a href="https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-3.1">Asp.Net Core WebApi</a>
- <a href="https://docs.microsoft.com/en-us/ef/core/">EF Core for basic ORM</a>
- <a href="https://www.microsoft.com/de-ch/sql-server/sql-server-downloads">MsSQL Database</a>

Frontend:
- <a href="https://vuejs.org/">Vue JS</a>
- <a href="https://vuex.vuejs.org/">Vuex state Management</a>
- <a href="https://bootstrap-vue.org/">Bootstrap-Vue</a>

### Installation Instructions:
#### Backend:
##### Database setup
Open Project in Visual Studio, then go to Package Manager Console and enter this:
```
update-database
```
##### Run server
Go to Package Manager Console and make sure you are in Bugtracker Project(cd ./Bugtracker) then type this:
```
dotnet run
```
#### Frontend:
##### Download dependencies & run
```
npm install
```
```
npm run serve
```
### What’s missing?
  <h6>Overall User Experience</h6>
  <ul>
    <li>Form Validation</li>
    <li>Generall Notifications (e.g. Ticket successfully created)</li>
  </ul>
  <h6>Application Security</h6>
  <ul>
    <li>JWT Token is stored in LocalStorage (<a href="https://dev.to/rdegges/please-stop-using-local-storage-1i04">Learn more</a> why storing JWT in LocalStorage is a bad idea.)</li>
  </ul>
