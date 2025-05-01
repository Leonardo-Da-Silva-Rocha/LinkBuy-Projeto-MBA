using LinkBuyLibrary.Configuration.Middlewares;
using LinkBuyMvc.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataBaseSelector();

builder.AddConfigIdentity();

builder.AddConfigJwtAPI();

builder.AddInterfacesProgram();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.AddConfigSwaggerAPI();

var app = builder.Build();

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseDbMigrationHelper();

app.Run();
