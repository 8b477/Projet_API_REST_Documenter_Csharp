using Microsoft.AspNetCore.Mvc;
using Udemy.Projet.API.REST.Interfaces;
using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        #region Injection de dépendance de l'accès a ma base de données.

        private readonly ITodoService? _context = null;

        public TodoController(ITodoService? context)
        {
            _context = context;
        }
        #endregion

        #region Méthode GetAllOfTodoList => qui me retourne toute mes tâches
        [HttpGet]
        public async Task<ActionResult<List<TodoListmodel>>> GetAllOfTodoList()
        {
            var request = await _context.Get();

            if (request == null)
                return NoContent();

            return Ok(request);
        }
        #endregion

        #region Méthode AddOneTodo => ajoute une nouvelle tâche

        [HttpPost]
        public async Task<ActionResult> AddOneTodo([FromBody] TodoListmodel model)
        {

            var request = await _context.AddOneTodo(model);

            if (request == null)
                return BadRequest();

            return CreatedAtAction("AddOne", model);
        }
        #endregion

        #region Méthode GetByIdOfTodoList => qui renvoie un item sur base de l'id donnée
        [HttpGet("{id:int:range(5,25)}")] // ici je peut typé et aussi demander une contrainte, possible aussi avc REGEX
        public async Task<ActionResult<TodoListmodel>> GetByIdOfTodoList(int id)
        {
            var request = await _context.GetById(id);

            if (request == null)
                return BadRequest();

            return Ok(request);
        }
        #endregion

        #region Méthode UpdateOneTodo => exécute une modification sur une tache déjà présente       
        
        #region ===> TO DO !!!!!!!
        //Rajouter le fait de récupéré directement l'id par le biais du premier paramètre rentrée
        //et de l'insérée directement sur le model que l'on veut modifier,
        //*****************************************************************************************
        // ** Utiliser un modèle sans id en prop et le mapé sur l'original avant de le retourner **
        //***************************************************************************************** 
        #endregion

        [HttpPut]
        public async Task<ActionResult> UpdateOneTodo(TodoListmodel model, int id)
        {         
            var request = await _context.UpdateOneTodo(model, id);

            if (request == null)
                return BadRequest($"Impossible de metter à jour la ressource, le titre n'existe pas => {model.id}");
                        
            return Ok(model);
        }
        #endregion

        #region Méthode DeleteOneTodo => supprime une tâche existante sur base de son id.

        #region ===> TO DO !!!!!!!
        //Géré le cas ou la ressource données pour supprimer n'existe pas. 
        //TROUVER UN MOYEN POUR PASSER EN PARAMETRE UN STRING.
        #endregion

        [HttpDelete]
        public async Task<ActionResult<TodoListmodel>> DeleteOneTodo([FromQuery]int id)
        {

            var request = await _context.DeleteOneTodo(id);

            if (request == null)
                return BadRequest();

            return NoContent();

        } 
        #endregion
    }
}
