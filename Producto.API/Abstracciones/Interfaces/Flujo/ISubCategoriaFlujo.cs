using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ISubCategoriaFlujo
    {
        Task<SubCategoria> Obtener(Guid id);
        Task<IEnumerable<SubCategoria>> Obtener();
        Task<IEnumerable<SubCategoria>> ObtenerPorCategoria(Guid idCategoria);
    }
}
