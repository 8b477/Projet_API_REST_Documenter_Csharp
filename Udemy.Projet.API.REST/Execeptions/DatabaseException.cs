namespace Projet.API.REST.Swagger.Execeptions
{
    /// <summary>
    /// Exeption lié à la base de données
    /// </summary>
    public class DatabaseException : Exception
    {
      
        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public DatabaseException(){}

        /// <summary>
        /// Constructeur avec un paramètre de type :
        /// => string.
        /// </summary>
        public DatabaseException(string message): base(message){}

        /// <summary>
        /// Constructeur avec paramètre de type:
        /// => String
        /// => Exception.
        /// </summary>
        public DatabaseException(string message, Exception innerException): base(message, innerException){}
    }  
}
