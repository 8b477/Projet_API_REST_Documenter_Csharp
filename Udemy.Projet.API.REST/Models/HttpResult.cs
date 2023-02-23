namespace Projet.API.REST.Swagger.Models
{
    public class HttpResult<T> where T :class
    {
        public int StatutCode { get; set; }
        public bool IsSucced { get; set; }
        public T Data { get; set; }
    }
}
