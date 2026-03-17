
using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase, IProductoController
    {

        private IProductoFlujo _productoFlujo;
        private ILogger<ProductoController> _logger;

        public ProductoController(IProductoFlujo productoFlujo, ILogger<ProductoController> logger)
        {
            _productoFlujo = productoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ProductoRequest producto)
        {
            var respuesta = await _productoFlujo.Agregar(producto);
            return CreatedAtAction(nameof(Obtener), new {Id=respuesta});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar([FromRoute]Guid Id, [FromBody] ProductoRequest producto)
        {
            if (!await VerificacionProductoExiste(Id))
                return NotFound("No se encontro el producto");
            var respuesta = await _productoFlujo.Editar(Id, producto);
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar([FromRoute]Guid Id)
        {
            if (!await VerificacionProductoExiste(Id))
                return NotFound("No se encontro el producto");
            var respuesta = await _productoFlujo.Eliminar(Id);
            return Ok(respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var respuesta = await _productoFlujo.Obtener();
            if (!respuesta.Any())
                return NoContent();
            return Ok(respuesta);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            if(!await VerificacionProductoExiste(Id))
                return NotFound("No se encontro el producto");
            var respuesta = await _productoFlujo.Obtener(Id);
            return Ok(respuesta);
        }

        private async Task<bool> VerificacionProductoExiste(Guid id)
        {
            var producto = await _productoFlujo.Obtener(id);
            return producto != null;
        }
    }
}
