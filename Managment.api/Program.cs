using Managment.api.Middleware;
using Managment.core;
using Managment.core.Database.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.implementServices(new Managment.core.Database.Models.DatabaseConfigurator()
{
    ConnectionString = "Data Source=PersonManagementSystem.sqlite"
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Inicializa la base de datos.
IDatabaseInitializer databaseInitializer = app.Services.GetRequiredService<IDatabaseInitializer>();
databaseInitializer.InitializeDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//middlewares personalizados
app.UseMiddleware<ErrorHandlingMiddleware>(); // Manejo de errores


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
