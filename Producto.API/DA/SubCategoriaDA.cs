using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class SubCategoriaDA : ISubCategoriaDA
    {

        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public SubCategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<SubCategoria> Obtener(Guid id)
        {
            var query = @"ObtenerSubCategoria";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoria>(query, new {Id = id});
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<IEnumerable<SubCategoria>> Obtener()
        {
            var query = @"ObtenerSubCategorias";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoria>(query);
            return resultadoConsulta;
        }

        public async Task<IEnumerable<SubCategoria>> ObtenerPorCategoria(Guid idCategoria)
        {
            var query = @"ObtenerSubCategoriaPorCategoria";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoria>(query, new { IdCategoria = idCategoria });
            return resultadoConsulta;
        }
    }
}
