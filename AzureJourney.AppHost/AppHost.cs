var builder = DistributedApplication.CreateBuilder(args);

var dbserver = builder
    .AddSqlServer("ProductsDBServer")
    .WithLifetime(ContainerLifetime.Persistent);

var db = dbserver.AddDatabase("ProductsDB");

builder.AddProject<Projects.AzureWebApp>("azurewebapp")
    .WithReference(db)
    .WaitFor(db);

builder.AddAzureFunctionsProject<Projects.AzureFunctions>("azurefunctions")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
