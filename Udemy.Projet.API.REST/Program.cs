#region Using
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Serilog;

using Udemy.Projet.API.REST.Configuration;
using Udemy.Projet.API.REST.DataBase;
using Udemy.Projet.API.REST.Interfaces;
using Udemy.Projet.API.REST.Services;
using Projet.API.REST.Swagger;
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

#region Extensions de mes Services voire dossier => Configuration, ServicesConfiguration. 
builder.Services.AddSwaggerGenService(); // => Ajout de la doc Swagger
builder.Services.AddAuthentificationService(); // => Authentification service JWTBearer 
builder.Services.AddControllerService(); // => Ajout d'une demande d'authorisation sur tout les controllers
#endregion

#region Configuration du logger => SERILOG
var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();

builder.Host.UseSerilog();
#endregion

#region BASE DE DONNEES
//Ajout de ma base de données
builder.Services.AddDbContext<MyContextData>(options => options.UseInMemoryDatabase("DataBase"));
#endregion

#region injection de dépendance
builder.Services.AddTransient<ITodoService, TodoService>();
#endregion

#region En cours => JWT
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//{
//options.TokenValidationParameters = new()
//{
//IssuerSigningKey = TokenHelper.SIGNING_KEY,
//ClockSkew = TimeSpan.FromMinutes(5)
//};
//}); 
#endregion


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

app.Run();



