using Microsoft.EntityFrameworkCore;
using Udemy.Projet.API.REST.DataBase;
using Udemy.Projet.API.REST.Interfaces;
using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.Services
{
    public class TodoService : ITodoService
    {

        #region Injection de dépendance
        private readonly MyContextData? _context = null;

        public TodoService(MyContextData context)
        {
            _context = context;
        }
        #endregion

        #region Méthode de ITodoService implémentée

        #region Méthode Get => renvoie toute la liste des tâches.
        public async Task<List<TodoListmodel>?> Get()
        {
            List<TodoListmodel?> result = await _context.TodoListmodels
                                                        .AsNoTracking() //Permet de forcer l'arrêt du tracking si non bug si la ressouce est rappeler par une autre méthode
                                                        .ToListAsync();

            if (result == null)
                return null;

            //_context.Entry(result).State = EntityState.Detached; => Attention ici cela créée une erreur !

            return result;
        }
        #endregion

        #region Méthode GetByID => retourne une tâche sur base de son ID.
        public async Task<TodoListmodel?> GetById(int id)
        {
            
            TodoListmodel? result = await _context.TodoListmodels
                                                  .AsNoTracking()
                                                  .Where(i => i.id == id)
                                                  .FirstOrDefaultAsync();

            if (result == null)
                return null;

            _context.Entry(result).State = EntityState.Detached;

            return result;
        }
        #endregion

        #region Méthode AddOneTodo => rajoute une tâche a ma base de donnée TodoList.
        public async Task<TodoListmodel?> AddOneTodo(TodoListmodel model)
        {
            var result = _context.Add(model);

            if (result == null)
                return null;

            await _context.SaveChangesAsync();

            return model;
        }
        #endregion

        #region Méthode UpdateOneTodo => retourne une tâche modifier sur base d'une existante retrouver par son id.
        public async Task<TodoListmodel?> UpdateOneTodo(TodoListmodel model, int id)
        {
            var result = await _context.TodoListmodels
                                        .AsNoTracking()
                                        .Where(i => i.id == id)
                                        .FirstOrDefaultAsync();

            if (model.id != id)
                return null;

            if (result == null)
                return null;

            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            _context.Entry(model).State = EntityState.Detached;

            return result;
        }
        #endregion

        #region Méthode DeleteOneTodo => supprime une tâche sur base de son id.
        public async Task<TodoListmodel?> DeleteOneTodo(int id)
        {
            var result = await _context.TodoListmodels.FindAsync(id);

            if (result == null)
                return null;

            _context.Entry(result).State = EntityState.Deleted;

            _context.TodoListmodels.Remove(result);

            await _context.SaveChangesAsync();

            _context.Entry(result).State = EntityState.Detached;

            return result;
        } 
        #endregion
        #endregion
    }
}
