using Microsoft.EntityFrameworkCore;

using Udemy.Projet.API.REST.Controllers;
using Udemy.Projet.API.REST.DataBase;
using Udemy.Projet.API.REST.Interfaces;
using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.Services
{
    public class TodoService : ITodoService
    {

        #region Injection de dépendance
        private readonly MyContextData? _context = null;
        private readonly ILogger<TodoController> _logger;

        public TodoService(MyContextData context, ILogger<TodoController> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Méthode de ITodoService implémentée

        #region Méthode Get => renvoie toute la liste des tâches.
        public async Task<List<TodoListmodel>?> Get(CancellationToken cancel)
        {
            List<TodoListmodel?> result = await _context.TodoListmodels
                                                        .AsNoTracking() //Permet de forcer l'arrêt du tracking si non bug si la ressouce est rappeler par une autre méthode
                                                        .ToListAsync(cancel);

            if (result == null)
                return null;

            //_context.Entry(result).State = EntityState.Detached; => Attention ici cela créée une erreur !

            return result;
        }
        #endregion

        #region Méthode GetByID => retourne une tâche sur base de son ID.
        public async Task<TodoListmodel?> GetById(int id, CancellationToken cancel)
        {
            TodoListmodel? result = await _context.TodoListmodels
                                                  .AsNoTracking()
                                                  .Where(i => i.id == id)
                                                  .FirstOrDefaultAsync(cancel);

            if (result == null)
            {
                _logger.LogError($"La tâche rechercher avec l'id: ({id}) n'existe pas !");

                return null;
            }

            _context.Entry(result).State = EntityState.Detached;

            _logger.LogInformation("La requête 'GetById' est un succès");

            return result;
        }
        #endregion

        #region Méthode AddOneTodo => rajoute une tâche a ma base de donnée TodoList.
        public async Task<TodoListmodel?> AddOneTodo(TodoListmodel model, CancellationToken cancel)
        {
            var result = _context.Add(model);

            if (result == null)
            {
                _logger.LogWarning("Le model retournée n'est pas complet !");

                return null;
            }


            await _context.SaveChangesAsync(cancel);

            _logger.LogInformation("La requête 'AddOneTodo' est un succès");

            return model;
        }
        #endregion

        #region Méthode UpdateOneTodo => retourne une tâche modifier sur base d'une existante retrouver par son id.
        public async Task<TodoListmodel?> UpdateOneTodo(TodoListmodel model, int id, CancellationToken cancel)
        {
            var result = await _context.TodoListmodels
                                        .AsNoTracking()
                                        .Where(i => i.id == id)
                                        .FirstOrDefaultAsync(cancel);

            if (model.id != id)
            {
                _logger.LogError($"La tâche rechercher avec l'id: ({id}) n'existe pas !");

                return null;
            }

            if (result == null)
            {
                _logger.LogWarning("Le model retournée n'est pas complet !");

                return null;
            }

            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            _context.Entry(model).State = EntityState.Detached;

            _logger.LogInformation("La requête 'UpdateOneTodo' est un succès");

            return result;
        }
        #endregion

        #region Méthode DeleteOneTodo => supprime une tâche sur base de son id.
        public async Task<TodoListmodel?> DeleteOneTodo(int id, CancellationToken cancel)
        {
            var result = await _context.TodoListmodels.FindAsync(id, cancel);

            if (result == null)
            {
                _logger.LogError($"La tâche rechercher avec l'id: ({id}) n'existe pas !");

                return null;
            }

            _context.Entry(result).State = EntityState.Deleted;

            _context.TodoListmodels.Remove(result);

            await _context.SaveChangesAsync();

            _context.Entry(result).State = EntityState.Detached;

            _logger.LogInformation("La requête 'DeleteOneTodo' est un succès");

            return result;
        } 
        #endregion
        #endregion
    }
}
