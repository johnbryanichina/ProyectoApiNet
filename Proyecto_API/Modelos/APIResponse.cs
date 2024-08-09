using System.Net;

namespace Proyecto_API.Modelos
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { set;get; }
        public bool IsExitoso { set; get; } = true;
        public List <string> ErrorMessages { get;set; }
        public object Resultado { get; set; }
    }
}
