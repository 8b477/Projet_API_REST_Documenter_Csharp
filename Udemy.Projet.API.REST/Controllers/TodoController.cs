using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.API.REST.Swagger.Filters;

using Udemy.Projet.API.REST.Interfaces;
using Udemy.Projet.API.REST.Models;

namespace Udemy.Projet.API.REST.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[DisableFilter] => Permet d'activer le filtre perso qui se situe dans le dossier < Filter >.
    public class TodoController : ControllerBase
    {
        #region Injection de dépendance de l'accès a ma base de données.

        private readonly ITodoService? _service = null;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService? context, ILogger<TodoController> logger)
        {
            _service = context;
            _logger = logger;
        }
        #endregion

        #region Méthode GetAllOfTodoList => qui me retourne toute mes tâches
        /// <summary>
        /// Permet de récupérer toute la liste des tâches disponible
        /// </summary>
        /// <remarks>
        /// <h2><u>Aide à l'utilisation :</u></h2>
        /// <br></br>
        /// (1) Appuyer sur le bouton => '<strong>try it out</strong>'.
        /// <br></br>
        /// (2) Appuyer sur le bouton => '<strong>Execute</strong>'.
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <returns>retourne une liste de tâches</returns>
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<ActionResult<List<TodoListmodel>>> GetAllOfTodoList(CancellationToken cancel)
        {
            List<TodoListmodel>? request = await _service?.Get(cancel);

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
        /// <h2><u>Aide à l'utilisation :</u></h2>
        /// <br></br>
        /// (1) Appuyer sur le bouton => '<strong>try it out</strong>'.
        /// <br></br>
        /// (2) Insérer une nouvelle tâche en renseignant chaque champ à sa <em>valeur</em> , comme ci dessous.
        /// <br></br>
        ///<example>
        ///<code>
        ///{
        ///<br></br>
        ///  "id": <em>"valeur"</em>,<br></br>
        ///  "title": "<em>valeur</em>",<br></br>
        ///  "content": "<em>valeur</em>"
        ///<br></br>
        ///}
        ///</code>
        ///</example>
        /// <br></br>
        /// (3) Appuyer sur le bouton => '<strong>Execute</strong>'.
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <response code= "201">(Code: 201) la requête de création à réussi !</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <response code= "401">(Code: 401) Vous n'avez pas les authorisations pour éffectuer la requête !</response>
        /// <param name="model"></param>
        /// <param name="cancel"></param>
        /// <returns>Ne retourne rien.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult> AddOneTodo(TodoListmodel model, CancellationToken cancel)
        {

            TodoListmodel? request = await _service?.AddOneTodo(model, cancel);

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
        /// <h2><u>Aide à l'utilisation :</u></h2>
        /// <br></br>
        /// (1) Appuyer sur le bouton => '<strong>try it out</strong>'.
        /// (2) Insérée un identifiant / id.
        /// (3) Appuyer sur le bouton => '<strong>Execute</strong>'.
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <response code= "401">(Code: 401) Vous n'avez pas les authorisations pour éffectuer la requête !</response>
        /// <param name="id"></param>
        /// <param name="cancel"></param>
        /// <returns>Une tâche lié à l'id renseigner par l'utilisateur.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpGet("{id:int}")] // => ici je peut typé et aussi demander une contrainte, possible aussi avc REGEX
        public async Task<ActionResult<TodoListmodel>> GetByIdOfTodoList(int id, CancellationToken cancel)
        {
            TodoListmodel? request = await _service?.GetById(id, cancel);

            if (request == null)
                return BadRequest();

            return Ok(request);
        }
        #endregion

        #region Méthode UpdateOneTodo => exécute une modification sur une tache déjà présente       
        /// <summary>
        /// Permet de modifier une tâche déjà existante.
        /// </summary>
        /// <remarks>
        /// <h2><u>Aide à l'utilisation :</u></h2>
        /// <br></br>
        /// (1) Appuyer sur le bouton => '<strong>try it out</strong>'.
        /// <br></br>
        /// (2) Entrez un identifiant / id.
        /// <br></br>
        /// (2) Insérer une nouvelle <em>valeur</em> au champ disponible, comme ci dessous.
        /// <br></br>
        ///<example>
        ///<code>
        ///{
        ///<br></br>
        ///"id": <em>"valeur"</em>,<br></br>
        ///"title": "<em>valeur</em>",<br></br>
        ///"content": "<em>valeur</em>"<br></br>
        ///}
        ///</code>
        ///</example>
        /// <br></br>
        /// (3) Appuyer sur le bouton => '<strong>Execute</strong>'.
        /// </remarks>
        /// <response code= "200">(Code: 200) La requête s'est exécuter correctement !</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <response code= "401">(Code: 401) Vous n'avez pas les authorisations pour éffectuer la requête !</response>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="cancel"></param>
        /// <returns>Une tâche sur base de son id et permets de la modifier.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateOneTodo(int id, TodoListmodel model, CancellationToken cancel)
        {
            TodoListmodel? request = await _service?.UpdateOneTodo(model, id, cancel);

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
        /// <h2><u>Aide à l'utilisation :</u></h2>
        /// <br></br>
        /// (1) Appuyer sur le bouton => '<strong>try it out</strong>'.
        /// <br></br>
        /// (2) Entrez un identifiant / id.
        /// <br></br>
        /// (3) Appuyer sur le bouton => '<strong>Execute</strong>'.
        /// </remarks>
        /// <response code= "204">(Code: 204) La requête s'est exécuter correctement, le contenu est vide.</response>
        /// <response code= "400">(Code: 400) La requête à échoué, valeur d'entrée non référencer dans la base de données !</response>
        /// <response code= "401">(Code: 401) Vous n'avez pas les authorisations pour éffectuer la requête !</response>
        /// <param name="id"></param>
        /// <param name="cancel"></param>
        /// <returns>Ne retourne rien.</returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TodoListmodel>> DeleteOneTodo(int id, CancellationToken cancel)
        {

            TodoListmodel? request = await _service?.DeleteOneTodo(id, cancel);

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
        [AllowAnonymous]
        [HttpGet("ExempleJeDonneUnCheminPerso")]
        public async Task<ActionResult<List<TodoListmodel>>> Get_All(CancellationToken cancel)
        {
            //Test du CancellationToken.
            _logger.LogWarning("Début de l'attente");
            await Task.Delay(10000, cancel); //Ajout d'un délai pour le test.

            List<TodoListmodel>? request = await _service?.Get(cancel);

            if (request == null)
                return NoContent();

            _logger.LogWarning("Fin de l'attente");

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
        /// <response code= "401">(Code: 401) Vous n'avez pas les authorisations pour éffectuer la requête !</response>
        /// <param name="id"></param>
        /// <param name="cancel"></param>
        /// <returns>Une tâche lié à l'id renseigner par l'utilisateur.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpGet("{id:int:range(1,5)}")] // => Ici je peut typé et aussi demander une contrainte, possible aussi avc REGEX
        public async Task<ActionResult<TodoListmodel>> Test_Condition(int id, CancellationToken cancel)
        {
            TodoListmodel? request = await _service?.GetById(id, cancel);

            if (request == null)
                return BadRequest();

            return Ok(request);
        }
        #endregion 
        #endregion
    }
}
