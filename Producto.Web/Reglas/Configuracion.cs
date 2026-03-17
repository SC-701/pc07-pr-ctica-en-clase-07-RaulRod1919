using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {

        private readonly IConfiguration _configuracion;

        public Configuracion(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        public string ObtenerMetodo(string modulo, string metodo)
        {
            string urlBase = ObtenerUrlBase(modulo);
            string valorMetodo = _configuracion.GetSection(modulo).Get<ApiEndPoint>().Metodos.FirstOrDefault(m => m.Nombre == metodo).Valor;
            return $"{urlBase}/{valorMetodo}";
        }

        public string ObtenerValor(string modulo)
        {
            string valor = _configuracion.GetSection(modulo).Value;
            return valor;
        }

        private string ObtenerUrlBase(string modulo)
        {
            string urlBase = _configuracion.GetSection(modulo).Get<ApiEndPoint>().UrlBase;
            return urlBase;
        }
    }
}
