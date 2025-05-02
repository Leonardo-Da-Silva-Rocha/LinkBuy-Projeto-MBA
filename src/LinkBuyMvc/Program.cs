using LinkBuyLibrary.Configuration.Middlewares;
using LinkBuyMvc.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataBaseSelector();

builder.AddConfigIdentity();

builder.AddConfigCookieMVC();

builder.Services.AddControllersWithViews();

builder.AddInterfacesProgram();

var app = builder.Build();

ConfiguracoesGerais.AddCultura();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseDbMigrationHelper();

app.Run();