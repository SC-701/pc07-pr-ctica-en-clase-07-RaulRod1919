using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class CategoriaDA : ICategoriaDA
    {

        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public CategoriaDA(IRepositorioDapper repositorioDapper, SqlConnection sqlConnection)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = sqlConnection;
        }

        public async Task<Categoria> Obtener(Guid id)
        {
            var query = @"ObtenerCategoria";
            var respuesta = await _sqlConnection.QueryAsync<Categoria>(query, new { Id = id });
            return respuesta.FirstOrDefault();
        }

        public async Task<IEnumerable<Categoria>> Obtener()
        {
            var query = @"ObtenerCategorias";
            var respuesta = await _sqlConnection.QueryAsync<Categoria>(query);
            return respuesta;
        }
    }
}
