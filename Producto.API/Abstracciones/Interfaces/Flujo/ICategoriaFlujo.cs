using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICategoriaFlujo
    {
        Task<Categoria> Obtener(Guid id);
        Task<IEnumerable<Categoria>> Obtener();
    }
}
