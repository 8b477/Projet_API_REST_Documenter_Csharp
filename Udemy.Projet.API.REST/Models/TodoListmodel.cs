using System.ComponentModel.DataAnnotations;

namespace Udemy.Projet.API.REST.Models
{
    public class TodoListmodel
    {
        #region Properties
        [Key]
        public int id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        #endregion
    }
}
