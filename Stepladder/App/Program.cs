using App.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLoadApplicationSettings();
await builder.AddLoadHttpClientAuthenticationAsync();

var app = builder.Build();



app.Run();