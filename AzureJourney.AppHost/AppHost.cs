var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AzureWebApp>("azurewebapp");

builder.Build().Run();
