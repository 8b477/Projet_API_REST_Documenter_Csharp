using Microsoft.EntityFrameworkCore;
using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.DataBase
{
    public class MyContextData : DbContext
    {

        public MyContextData(DbContextOptions<MyContextData> options) : base(options)
        {

        }

        public DbSet<TodoListmodel>? TodoListmodels { get; set; } = null;
    }
}
