using System.Data.Common;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Projet.API.REST.Swagger.Execeptions;

using Udemy.Projet.API.REST.Controllers;
using Udemy.Projet.API.REST.DataBase;
using Udemy.Projet.API.REST.Interfaces;
using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.Services
{
    /// <summary>
    /// Class services controller.
    /// </summary>
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

        /// <summary>
        /// Check la validité (mini)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool CleanUpModel(TodoListmodel model)
        {
            // id, title, content

            if (model == null)
                return false;

            if (String.IsNullOrWhiteSpace(model.Title) || String.IsNullOrWhiteSpace(model.Content))
            {
                return false;
            }

            return true;
        }

        #region Méthode de ITodoService implémentée

        /// <summary>
        /// Méthode Get => renvoie toute la liste des tâches.
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public async Task<List<TodoListmodel>?> Get(CancellationToken cancel)
        {
            try
            {
                List<TodoListmodel?> result = await _context
                                                .TodoListmodels.AsNoTracking()
                                                .ToListAsync(cancel);

                // Vérifie si la liste est vide et renvoie null si c'est le cas
                if (!result.Any())
                    return null;

                //_context.Entry(result).State = EntityState.Detached; => Attention ici cela crée une erreur 500!
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la récupération des données.");
                throw;
            }
        }

        /// <summary>
        /// Retourne une tâche sur base de son ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public async Task<TodoListmodel?> GetById(int id, CancellationToken cancel)
        {
            try
            {

                TodoListmodel? result = await _context.TodoListmodels
                                                      .AsNoTracking()
                                                      .Where(i => i.Id == id)
                                                      .FirstOrDefaultAsync(cancel);

                if (result == null)
                {
                    _logger.LogError($"La tâche rechercher avec l'id: ({id}) n'existe pas !");

                    return null;
                }

                _context.Entry(result).State = EntityState.Detached;


                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// Méthode AddOneTodo => rajoute une tâche a ma base de donnée TodoList.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public async Task<TodoListmodel?> AddOneTodo(TodoListmodel model, CancellationToken cancel)
        {

            if (!CleanUpModel(model))
            {
                
                _logger.LogWarning("Le model retournée n'est pas complet !");

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Le modèle est incomplet ou incorrect.",
                    Detail = "Le modèle que vous avez envoyé est invalide."
                };
                throw new ProblemDetailsException(problemDetails);
            }

            try
            {

                var result = _context.Add(model);

                await _context.SaveChangesAsync(cancel);

                _logger.LogInformation("La requête 'AddOneTodo' est un succès");

                return model;

            }
            catch (DbException ex)
            {
                _logger.LogError("Une erreur de base de données s'est produite lors de l'ajout d'un Todo : {Error}", ex.Message);
                throw new DatabaseException("Une erreur de base de données s'est produite lors de l'ajout d'un Todo.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de l'ajout d'un Todo.");
                throw new ServiceException("Une erreur s'est produite lors de l'ajout d'un Todo.", ex);
            }
            return null;
        }


        /// <summary>
        /// Modifie une tâche enregistrée via son id.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public async Task<TodoListmodel?> UpdateOneTodo(TodoListmodel model, int id, CancellationToken cancel)
        {

            try
            {
                var result = await _context.TodoListmodels
                                            .AsNoTracking()
                                            .Where(i => i.Id == id)
                                            .FirstOrDefaultAsync(cancel);

                if (model.Id != id)
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
            catch (DbException ex)
            {
                _logger.LogError("Une erreur de base de données s'est produite lors de l'ajout d'un Todo : {Error}", ex.Message);
                throw new DatabaseException("Une erreur de base de données s'est produite lors de l'ajout d'un Todo.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de l'ajout d'un Todo.");
                throw new ServiceException("Une erreur s'est produite lors de l'ajout d'un Todo.", ex);
            }
        }

        /// <summary>
        /// Supprime une tâche enregistrée sur base de son id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public async Task<TodoListmodel?> DeleteOneTodo(int id, CancellationToken cancel)
        {
            try
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
            catch (DbException ex)
            {
                _logger.LogError("Une erreur de base de données s'est produite lors de l'ajout d'un Todo : {Error}", ex.Message);
                throw new DatabaseException("Une erreur de base de données s'est produite lors de l'ajout d'un Todo.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de l'ajout d'un Todo.");
                throw new ServiceException("Une erreur s'est produite lors de l'ajout d'un Todo.", ex);
            }
        }
    }
}
    #endregion


