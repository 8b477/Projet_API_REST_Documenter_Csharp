using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.Services
{
    public interface ITodoServices
    {

        //Méthode Get => retourne toute la liste des tâches.
        public void Get();

        //Méthode GetByID => retourne une tâche en fonction de l'id entrée.
        public TodoListmodel GetByID();

        //Méthode AddOneTodo => pour ajouter une tâche.
        public void AddOneTodo(TodoListmodel todo);

        //Méthode UpdateTodo => pour mettre à jour une tâche déjas enregistrer par le biais de son id.
        public void UpdateOneTodo(TodoListmodel todo);

        //Méthode DeleteOneTodo => pour supprimer une tâche avec son id.
        public void DeleteOneTodo(int id);
    }
}
