using System.ComponentModel.DataAnnotations;

namespace Udemy.Projet.API.REST.Models
{
    /// <summary>
    /// Représente la table TodoList
    /// </summary>
    public class TodoListmodel
    {
        /// <summary>
       /// Identifiant
       /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Titre
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Contenu
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
