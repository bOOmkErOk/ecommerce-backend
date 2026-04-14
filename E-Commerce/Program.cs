using E_Commerce.Extensions.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices(builder.Configuration);

var app = builder.Build();

app.UseCoreApplication();

app.Run();