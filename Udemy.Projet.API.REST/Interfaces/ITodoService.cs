using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.Interfaces
{
    public interface ITodoService
    {
        //Retourne une liste de Todomodel
        Task<List<TodoListmodel>?> Get();

        //Retourne une liste de Todomodel sur base de l'id passer en paramètre
        Task<TodoListmodel?> GetById(int id);

        //Ajoute une tâche en base de donnée
        Task<TodoListmodel?> AddOneTodo(TodoListmodel model);

        //Modifier une tâche déjà existante en se basant sur son id
        Task<TodoListmodel?> UpdateOneTodo(TodoListmodel model, int id);

        //Supprimer une tâche de la liste en passant son id
        Task<TodoListmodel?> DeleteOneTodo(int id);

    }
}
