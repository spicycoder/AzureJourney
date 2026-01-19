# Azure Journey

- [x] [Web App](https://az-journey.azurewebsites.net/swagger/index.html)
- [x] Azure SQL
- [x] Application Insights
- [ ] Azure Storage
    - [ ] Blob Storage
    - [ ] Table Storage
    - [ ] Queue
- [ ] Azure Functions
    - [ ] Http Trigger
    - [ ] Queue Trigger
    - [ ] Timer Trigger
    - [ ] Blob Trigger

![Azure Journey](https://breakingmoulds.com/wp-content/uploads/2014/06/bilbo-baggins-adventure-with-quote.jpg)

---

## SQL Migrations

```pwsh
dotnet tool restore
dotnet ef migrations add Init -p .\DataAccess\DataAccess.csproj -s .\AzureWebApp\AzureWebApp.csproj
dotnet ef database update -p .\DataAccess\DataAccess.csproj -s .\AzureWebApp\AzureWebApp.csproj
```
