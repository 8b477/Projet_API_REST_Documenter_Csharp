namespace Projet.API.REST.Swagger.Execeptions
{
    /// <summary>
    /// Exeption lié au services.
    /// </summary>
    public class ServiceException : Exception
    {

        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public ServiceException(){}

        /// <summary>
        /// Constructeur avec un paramètre de type :
        /// => string.
        /// </summary>
        public ServiceException(string message): base(message){}

        /// <summary>
        /// Constructeur avec paramètre de type:
        /// => String
        /// => Exception.
        /// </summary>
        public ServiceException(string message, Exception innerException): base(message, innerException) {}
    }
}
