using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface ICategoriaDA
    {
        Task<Categoria> Obtener(Guid id);
        Task<IEnumerable<Categoria>> Obtener();
    }
}
