using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface ISubCategoriaController
    {
        Task<IActionResult> Obtener(Guid id);
        Task<IActionResult> Obtener();
        Task<IActionResult> ObtenerPorCategoria(Guid idCategoria);
    }
}
