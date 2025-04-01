var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.SharpShop_ApiService>("apiservice");

builder.AddProject<Projects.SharpShop_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
