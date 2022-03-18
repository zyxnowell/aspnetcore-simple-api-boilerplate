# aspnetcore-simple-api-boilerplate
Simple aspnetcore 5.0 api clean architecture with EF, identity, swagger, and jwt token

Based on Microsoft's [Common web application architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)

# Getting Started
## Installation
1. Clone this project
2. Change ConnString in appsettings.json and point to your DB
3. Run these commands targeting `Simple.Infrastructure` to update the local DB

`Add-Migration "Initial Migration"`

`Update-Database -Context AppDBContext`

4. Run app and access swagger by adding `/swagger` to the base url (example: `http://localhost:3333/swagger`)

5. Add necessary entities

You're good to use the API!
