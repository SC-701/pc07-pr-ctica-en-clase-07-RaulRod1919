using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface ISubCategoriaDA
    {
        Task<SubCategoria> Obtener(Guid id);
        Task<IEnumerable<SubCategoria>> Obtener();
        Task<IEnumerable<SubCategoria>> ObtenerPorCategoria(Guid idCategoria);
    }
}
