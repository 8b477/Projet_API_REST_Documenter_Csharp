namespace Projet.API.REST.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResult<T> where T :class
    {
        public int StatutCode { get; set; }
        public bool IsSucced { get; set; }
        public T Data { get; set; }
    }
}
