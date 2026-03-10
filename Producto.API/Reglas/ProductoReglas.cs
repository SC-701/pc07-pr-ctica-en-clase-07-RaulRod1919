using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {

        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio)
        {
            _tipoCambioServicio = tipoCambioServicio;
        }

        public async Task<decimal?> ObtenerValorUSD(decimal precio)
        {
            if(precio == 0)
            {
                return 0;
            }
            var tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioActual();
            return precio / tipoCambio;
        }
    }
}
