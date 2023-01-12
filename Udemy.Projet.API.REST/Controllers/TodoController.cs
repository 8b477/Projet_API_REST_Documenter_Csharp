using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy.Projet.API.REST.DataBase;
using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        #region Fake data
        public static List<TodoListmodel> FakeDB = new List<TodoListmodel>() {
                    new TodoListmodel(){ id = 1, Title = "First", Content = "Ma première tâche" },
                    new TodoListmodel(){ id = 2, Title = "Second", Content = "Ma seconde tâche" },
                };
        #endregion

        #region Injection de dépendance de l'accès a ma base de données.

        private readonly MyContextData? _context = null;

        public TodoController(MyContextData context)
        {
            _context = context;
        }
        #endregion

        #region Méthode qui me retourne toute mes tâches
        [HttpGet]
        public async Task<ActionResult<List<TodoListmodel>>> Get()
        {

            var result = await _context.TodoListmodels.ToListAsync();

            if (result.Count == 0)
                return NoContent();
            return Ok(result);
        }
        #endregion

        #region Méthode AddOne => ajoute une nouvelle tâche

        [HttpPost]
        public async Task<ActionResult> AddOne([FromBody] TodoListmodel model)
        {
            if (model is not null)
            {
                _context?.Add(model);

                await _context.SaveChangesAsync(); //Ne pas oublier de sauvegarder !!
                                                   //si non le changement se fait bien m'est pas stocker en mémoire

                return CreatedAtAction("AddOne", model); // Avec CreatedAtAction donnée en premier paramètre le nom de l'action,
                                                         // enfaite le nom de ma méthode ici >AddOne<
                                                         // Status code renvoyer => 201
            }
            return BadRequest();
        }
        #endregion

        #region Méthode GetByID => qui renvoie un item sur base de l'id donnée
        [HttpGet("GetByID")]
        public async Task<ActionResult<TodoListmodel>> GetByID(int id)
        {
            var result = await _context.TodoListmodels.FindAsync(id);

            if (result == null)
                return BadRequest($"Pas d'item retrouver avec id => {id}");

            return Ok(result);
        }
        #endregion

        #region Méthode UpdateTodoList => exécute une modification sur une tache déjà présente       
        [HttpPut]
        public async Task<ActionResult> UpdateTodoList(TodoListmodel model, int id)
        {
                                                
            if (model.id != id)
                return BadRequest($"Impossible de metter à jour la ressource, le titre n'existe pas => {model.id}");

            _context.Entry(model).State = EntityState.Modified;

            _context.SaveChangesAsync();

            return Ok(model);
        }
        #endregion

        #region Méthode DeletetaskOfTodoList => supprime une tâche existante sur base de son id.
        //TROUVER UN MOYEN POUR PASSER EN PARAMETRE UN STRING.
        [HttpDelete]
        public async Task<ActionResult<TodoListmodel>> DeletetaskOfTodoList([FromQuery]int id)
        {

            var result = await _context.FindAsync<TodoListmodel>(id);

            if (result == null)
                return BadRequest($"Le titre => {id}, ne correspond pas");

            _context.Remove(result);

            _context.SaveChangesAsync();

            return NoContent();

        } 
        #endregion
    }
}
