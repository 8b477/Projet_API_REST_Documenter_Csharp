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
        /// <summary>
        /// Permet de récupérer toute la liste des tâches disponible
        /// </summary>
        /// <remarks>
        /// Aide à l'utilisation :
        /// <br></br>
        /// (1) Appuyer sur le bouton => '<strong>try it out</strong>'
        /// <br></br>
        /// (2) Appuyer sur le bouton => '<strong>Execute</strong>'
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <returns>retourne une liste de tâches</returns>
        [HttpGet]
        public async Task<ActionResult<List<TodoListmodel>>> GetAllOfTodoList()
        {
            var request = await _context?.Get();

            if (request == null)
                return NoContent();

            return Ok(request);
        }
        #endregion

        #region Méthode AddOneTodo => Ajoute une nouvelle tâche
        /// <summary>
        /// Ajoute une nouvelle tâche
        /// </summary>
        /// <remarks>
        /// <h2>Aide à l'utilisation :</h2>
        /// <br></br>
        /// <p>(1) Appuyer sur le bouton => '<strong>try it out</strong>'</p>
        /// <br></br>
        /// <p>(2) Insérer une nouvelle tâche en renseignant chaque champ à sa valeur , comme ci dessous</p>
        /// <br></br>
        ///<pre>
        ///{<br></br>
        ///"id": <em>"ma valeur"</em>,<br></br>
        ///"title": "<em>ma valeur</em>",<br></br>
        ///"content": "<em>ma valeur</em>"<br></br>
        ///}
        /// </pre>
        /// <br></br>
        /// <p>(3) Appuyer sur le bouton => '<strong>Execute</strong>'</p>
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <response code= "201">(Code: 201) La requête de création à résussis !</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non compatible voire manquante !</response>
        /// <param name="model"></param>
        /// <returns>Ne retourne rien</returns>
        [HttpPost]
        public async Task<ActionResult> AddOneTodo(TodoListmodel model)
        {

            var request = await _context.AddOneTodo(model);

            if (request == null)
                return BadRequest();

            return CreatedAtAction("AddOneTodo", model);
        }
        #endregion

        #region Méthode GetByIdOfTodoList => qui renvoie un item sur base de l'id donnée
        [HttpGet("{id:int:range(1,5)}")] // ici je peut typé et aussi demander une contrainte, possible aussi avc REGEX
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateOneTodo([FromQuery] int id,TodoListmodel model)
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

        #region Démo route perso.
        [HttpGet("ExempleJeDonneUnCheminPerso")]
        public async Task<ActionResult<List<TodoListmodel>>> GetAll()
        {
            var request = await _context?.Get();

            if (request == null)
                return NoContent();

            return Ok(request);
        }
        #endregion
    }
}
