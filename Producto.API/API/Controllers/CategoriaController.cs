using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase, ICategoriaController
    {

        private readonly ICategoriaFlujo _categoriaFlujo;

        public CategoriaController(ICategoriaFlujo categoriaFlujo)
        {
            _categoriaFlujo = categoriaFlujo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid id)
        {
            if (await Existe(id))
                return NotFound("No se encontro la categoría");
            var respuesta = await _categoriaFlujo.Obtener(id);
            return Ok(respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var respuesta = await _categoriaFlujo.Obtener();
            if (!respuesta.Any())
                return NoContent();
            return Ok(respuesta);
        }

        #region Helpers

        private async Task<bool> Existe(Guid id)
        {
            var categoria = await _categoriaFlujo.Obtener(id);
            return categoria != null;
        }

        #endregion
    }
}
