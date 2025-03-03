using DaveVentura.WebApiExtendedTemplate.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServicesInAssembly();

var app = builder.Build();
app.ConfigureAppInAssembly();
