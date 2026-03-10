using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class ProductoDA : IProductoDA
    {

        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"AgregarProducto";
            var respuesta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            });
            return respuesta;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            await verificarExistenciaProducto(Id);
            string query = @"EditarProducto";
            var respuesta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            });
            return respuesta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarExistenciaProducto(Id);
            string query = @"EliminarProducto";
            var respuesta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return respuesta;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"ObtenerProductos";
            var respuesta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return respuesta;
        }

        public async Task<ProductoDetalle> Obtener(Guid Id)
        {
            string query = @"ObtenerProducto";
            var respuesta = await _sqlConnection.QueryAsync<ProductoDetalle>(query, new {Id=Id});
            return respuesta.FirstOrDefault();
        }

        private async Task verificarExistenciaProducto(Guid Id)
        {
            ProductoResponse? RespuestaProducto = await Obtener(Id);
            if (RespuestaProducto == null)
                throw new Exception("No se encontro ningun producto");
        }

    }
}
