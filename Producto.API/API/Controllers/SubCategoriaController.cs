using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaController : ControllerBase, ISubCategoriaController
    {

        private readonly ISubCategoriaFlujo _subCategoriaFlujo;

        public SubCategoriaController(ISubCategoriaFlujo subCategoriaFlujo)
        {
            _subCategoriaFlujo = subCategoriaFlujo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid id)
        {
            if(await Existe(id))
                return NotFound("SubCategoría no registrada");
            var respuesta = await _subCategoriaFlujo.Obtener(id);
            return Ok(respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var respuesta = await _subCategoriaFlujo.Obtener();
            if (!respuesta.Any())
                return NoContent();
            return Ok(respuesta);
        }
        [HttpGet("Categoria/{idCategoria}")]
        public async Task<IActionResult> ObtenerPorCategoria([FromRoute] Guid idCategoria)
        {
            var respuesta = await _subCategoriaFlujo.ObtenerPorCategoria(idCategoria);
            if(!respuesta.Any())
                return NoContent();
            return Ok(respuesta);
        }

        #region Helpers

        private async Task<bool> Existe(Guid id)
        {
            var subCategoria = await _subCategoriaFlujo.Obtener(id);
            return subCategoria != null;
        }

        #endregion

    }
}
