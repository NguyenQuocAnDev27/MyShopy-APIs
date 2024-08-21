using MyShopy.Models.Interfaces;
using MyShopy.Models.Services;
using System.Data;
using System.Data.SqlClient;    

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register the DbService
builder.Services.AddTransient<IDbService>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new DbService(new SqlConnection(connectionString));
});

// Register Service
builder.Services.AddTransient<ProductStatusService>();

// Add logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
