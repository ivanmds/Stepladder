using App.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLoadApplicationSettings();
await builder.AddHttpClientAuthenticationAsync();
builder.AddApiSecuret();



var app = builder.Build();

app.UseAuthentication();

app.Run();