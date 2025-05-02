using LinkBuyLibrary.Configuration.Middlewares;
using LinkBuyMvc.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataBaseSelector();

builder.AddConfigIdentity();

builder.AddConfigJwtAPI();

builder.AddInterfacesProgram();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.AddConfigSwaggerAPI();

ConfiguracoesGerais.AddCultura();

var app = builder.Build();

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
