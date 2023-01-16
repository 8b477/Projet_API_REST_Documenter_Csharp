using Microsoft.EntityFrameworkCore;
using Udemy.Projet.API.REST.Configuration;
using Udemy.Projet.API.REST.DataBase;
using Udemy.Projet.API.REST.Interfaces;
using Udemy.Projet.API.REST.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenService(); // => extension dispo dans /Configuration/ServicesConfiguration

#region injection de dépendance
builder.Services.AddTransient<ITodoService, TodoService>();
#endregion

#region BASE DE DONNEES
//Ajout de ma base de données
builder.Services.AddDbContext<MyContextData>(options => options.UseInMemoryDatabase("DataBase")); 
#endregion

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
