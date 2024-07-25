using Microsoft.EntityFrameworkCore;
using WaterMonitorSimple.Database;
using WaterMonitorSimple.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// add Razor pages
builder.Services.AddRazorPages();

// add database
var connStr = builder.Configuration.GetConnectionString("SimpleWaterMonitorDb");
builder.Services.AddDbContext<WaterMonitorDbContext>(options => {
    options.UseMySql(connStr, MariaDbServerVersion.AutoDetect(connStr));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapRazorPages();

app.MapGroup("/api/v1/report")
    .AddExcelEndpoints();

app.Run();
