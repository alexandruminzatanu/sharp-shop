var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SharpShopApi>("sharpshopapi");

builder.Build().Run();
