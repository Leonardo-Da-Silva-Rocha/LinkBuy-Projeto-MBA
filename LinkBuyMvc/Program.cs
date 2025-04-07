using LinkBuyLibrary.Services;
using LinkBuyMvc.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataBaseSelector();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<VendedorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseDbMigrationHelper();

app.Run();