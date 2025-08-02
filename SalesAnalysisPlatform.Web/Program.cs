using Microsoft.AspNetCore.Mvc;
using SalesAnalysisPlatform.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("SalesAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7237");
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<SalesApiService>();

builder.Services.AddScoped<CustomerApiService>();

builder.Services.AddHttpClient<SalesApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7237");
});

builder.Services.AddHttpClient<CustomerApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7237");
});

builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
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

app.Run();
