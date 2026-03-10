using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using System.Text.Json;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {

        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;

        public TipoCambioServicio(IConfiguracion configuracion, IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<decimal?> ObtenerTipoCambioActual()
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPointBCCR", "ObtenerTipoCambio");
            var token = _configuracion.ObtenerValor("TokenBCCR");
            var servicio = _httpClient.CreateClient("ServicioTipoCambio");
            servicio.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var respuesta = await servicio.GetAsync(string.Format(endpoint, DateTime.Now.ToString("yyyy/MM/dd")));
            if (respuesta.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 0;
            }
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultado = JsonSerializer.Deserialize<TipoCambio>(contenido, opciones);
            return resultado.Datos.FirstOrDefault().Indicadores.FirstOrDefault().Series.FirstOrDefault().ValorDatoPorPeriodo;
        }
    }
}
