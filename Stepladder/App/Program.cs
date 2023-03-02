using App.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLoadApplicationSettings();


var app = builder.Build();



app.Run();