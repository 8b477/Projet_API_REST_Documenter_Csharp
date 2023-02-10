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
        /// <h2>Aide à l'utilisation :</h2>
        /// <br></br>
        /// (1) Appuyer sur le bouton => '<strong>try it out</strong>'
        /// <br></br>
        /// (2) Appuyer sur le bouton => '<strong>Execute</strong>'
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <returns>retourne une liste de tâches</returns>
        [ProducesResponseType(200)]
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
        /// Ajoute une nouvelle tâche.
        /// </summary>
        /// <remarks>
        /// <h2>Aide à l'utilisation :</h2>
        /// <br></br>
        /// <p>(1) Appuyer sur le bouton => '<strong>try it out</strong>'</p>
        /// <br></br>
        /// <p>(2) Insérer une nouvelle tâche en renseignant chaque champ à sa <em>valeur</em> , comme ci dessous</p>
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
        /// <response code= "201">(Code: 201) la requête de création à réussi !</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <param name="model"></param>
        /// <returns>Ne retourne rien.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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
        /// <summary>
        /// Retourne une tâche lié à l'id fourni.
        /// </summary>
        /// <remarks>
        /// <h2>Aide à l'utilisation :</h2>
        /// <br></br>
        /// <p>(1) Appuyer sur le bouton => '<strong>try it out</strong>'</p>
        /// <p>(2) Insérée un identifiant / id</p>
        /// <p>(3) Appuyer sur le bouton => '<strong>Execute</strong>'</p>
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <param name="id"></param>
        /// <returns>Une tâche lié à l'id renseigner par l'utilisateur.</returns>
        [HttpGet("{id:int}")] // ici je peut typé et aussi demander une contrainte, possible aussi avc REGEX
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
        /// <summary>
        /// Permet de modifier une tâche déjà existante.
        /// </summary>
        /// <remarks>
        /// <h2>Aide à l'utilisation :</h2>
        /// <br></br>
        /// <p>(1) Appuyer sur le bouton => '<strong>try it out</strong>'</p>
        /// <br></br>
        /// <p>(2) Entrez un identifiant / id </p>
        /// <br></br>
        /// <p>(2) Insérer une nouvelle <em>valeur</em> au champ disponible, comme ci dessous</p>
        /// <br></br>
        /// <pre>
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
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Une tâche sur base de son id et permets de la modifier.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateOneTodo(int id, TodoListmodel model)
        {         
            var request = await _context.UpdateOneTodo(model, id);

            if (request == null)
                return BadRequest($"Impossible de mettre à jour la ressource, l'id : {model.id} n'existe pas !");
                        
            return Ok(model);
        }
        #endregion

        #region Méthode DeleteOneTodo => supprime une tâche existante sur base de son id.
        /// <summary>
        /// Permet de supprimer une tâche existante sur base de son identifiant / id.
        /// </summary>
        /// <remarks>
        /// <h2>Aide à l'utilisation :</h2>
        /// <br></br>
        /// <p>(1) Appuyer sur le bouton => '<strong>try it out</strong>'</p>
        /// <br></br>
        /// <p>(2) Entrez un identifiant / id </p>
        /// <br></br>
        /// <p>(3) Appuyer sur le bouton => '<strong>Execute</strong>'</p>
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <response code= "204">(Code: 204) La requête s'est exécuter correctement, le contenu est vide.</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <param name="id"></param>
        /// <returns>Ne retourne rien.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TodoListmodel>> DeleteOneTodo(int id)
        {

            var request = await _context.DeleteOneTodo(id);

            if (request == null)
                return BadRequest();

            return NoContent();

        }
        #endregion

        #region Exemple en plus..
        #region Démo route perso. => [HttpGet("ExempleJeDonneUnCheminPerso")]
        /// <summary>
        /// EXEMPLE DE ROUTE PERSO
        /// Permet de récupérer toute la liste des tâches disponible
        /// </summary>
        /// <remarks>
        /// <h2>*EXEMPLE DE ROUTE PERSO*,</h2>
        /// <h3>Aide à l'utilisation :</h3>
        /// <br></br>
        /// (1) Appuyer sur le bouton => '<strong>try it out</strong>'
        /// <br></br>
        /// (2) Appuyer sur le bouton => '<strong>Execute</strong>'
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <returns>Fait exactement le même que la première méthode GetAllOfTodoList(), retourne une liste de tâches</returns>
        [HttpGet("ExempleJeDonneUnCheminPerso")]
        public async Task<ActionResult<List<TodoListmodel>>> Get_All()
        {
            var request = await _context?.Get();

            if (request == null)
                return NoContent();

            return Ok(request);
        }
        #endregion

        #region Méthode qui renvoie un item sur base de l'id donnée avec une contrainte.
        /// <summary>
        /// EXEMPLE DE CONTRAINTE,
        /// Retourne une tâche lié à l'id fourni.
        /// </summary>
        /// <remarks>
        /// <h2>*EXEMPLE DE CONTRAINTE*, id autorisée de 1 à 5 compris.</h2>
        /// <h3>Aide à l'utilisation :</h3>
        /// <br></br>
        /// <p>(1) Appuyer sur le bouton => '<strong>try it out</strong>'</p>
        /// <p>(2) Insérée un identifiant / id</p>
        /// <p>(3) Appuyer sur le bouton => '<strong>Execute</strong>'</p>
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <param name="id"></param>
        /// <returns>Une tâche lié à l'id renseigner par l'utilisateur.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{id:int:range(1,5)}")] // => Ici je peut typé et aussi demander une contrainte, possible aussi avc REGEX
        public async Task<ActionResult<TodoListmodel>> Test_Condition(int id)
        {
            var request = await _context.GetById(id);

            if (request == null)
                return BadRequest();

            return Ok(request);
        }
        #endregion 
        #endregion
    }
}
