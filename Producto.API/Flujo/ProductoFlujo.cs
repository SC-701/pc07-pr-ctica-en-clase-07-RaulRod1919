using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {

        private readonly IProductoDA _productoDA;
        private readonly IProductoReglas _reglas;


        public ProductoFlujo(IProductoDA productoDA, IProductoReglas reglas)
        {
            _productoDA = productoDA;
            _reglas = reglas;
        }

        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            return await _productoDA.Agregar(producto);
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            return await _productoDA.Editar(Id, producto);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _productoDA.Eliminar(Id);
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            return await _productoDA.Obtener();
        }

        public async Task<ProductoDetalle> Obtener(Guid Id)
        {
            var producto = await _productoDA.Obtener(Id);
            if(producto == null)
                return null;
            producto.PrecioUSD = (decimal)await _reglas.ObtenerValorUSD(producto.Precio);
            return producto;
        }
    }
}
