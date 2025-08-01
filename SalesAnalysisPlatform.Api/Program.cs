using Oracle.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesAnalysisPlatform.Infrastructure.IOC;
using SalesAnalysisPlatform.Infrastructure.Context;
using SalesAnalysisPlatform.Infrastructure.Repositories;
using SalesAnalysisPlatform.Infrastructure.Interfaces;
using SalesAnalysisPlatform.Application.Interfaces;
using SalesAnalysisPlatform.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb"))
);

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("OracleDb");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("OracleDb connection string is missing");
}
builder.Services.AddInfrastructure(connectionString);

builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb", policy =>
        policy.WithOrigins("https://localhost:7291")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowWeb");

app.MapControllers();

app.Run();
