var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BackendAssignment_Web>("web");

builder.Build().Run();
