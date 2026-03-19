using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class SubCategoriaFlujo : ISubCategoriaFlujo
    {

        private readonly ISubCategoriaDA _subCategoriaDA;

        public SubCategoriaFlujo(ISubCategoriaDA subCategoriaDA)
        {
            _subCategoriaDA = subCategoriaDA;
        }

        public async Task<SubCategoria> Obtener(Guid id)
        {
            return await _subCategoriaDA.Obtener(id);
        }

        public async Task<IEnumerable<SubCategoria>> Obtener()
        {
            return await _subCategoriaDA.Obtener();
        }

        public async Task<IEnumerable<SubCategoria>> ObtenerPorCategoria(Guid idCategoria)
        {
            return await _subCategoriaDA.ObtenerPorCategoria(idCategoria);
        }
    }
}
