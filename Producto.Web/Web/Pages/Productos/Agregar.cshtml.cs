using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class AgregarModel : PageModel
    {
        [BindProperty]
        public ProductoRequest Producto { get; set; } = default!;
        [BindProperty]
        [Required(ErrorMessage = "Categoría necesaría")]
        public List<SelectListItem> Categorias { get; set; } = default!;
        [BindProperty]
        [Required(ErrorMessage = "SubCategoría necesaría")]
        public List<SelectListItem> SubCategorias { get; set; } = default!;

        private readonly IConfiguracion _configuration;

        public AgregarModel(IConfiguracion configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            await ObtenerCategorias();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await ObtenerCategorias();
                return Page();
            }
            var endpoint = _configuration.ObtenerMetodo("ProductosApiEndPoint", "AgregarProducto");
            var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync(endpoint, Producto);
            respuesta.EnsureSuccessStatusCode();
            return Redirect("./Index");
        }

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid idCategoria)
        {
            var subcategorias = await ObtenerSubCategorias(idCategoria);
            return new JsonResult(subcategorias);
        }

        #region Helpers
        private async Task<List<SubCategoria>> ObtenerSubCategorias(Guid idCategoria)
        {
            var endpoint = _configuration.ObtenerMetodo("ProductosApiEndPoint", "ObtenerSubCategoriasPorCategoria");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint,idCategoria));
            var respuesta = await cliente.SendAsync(solicitud);
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var informacion = JsonSerializer.Deserialize<List<SubCategoria>>(contenido, opciones);
                return informacion;
            }
            return new List<SubCategoria>();
        }

        private async Task ObtenerCategorias()
        {
            var endpoint = _configuration.ObtenerMetodo("ProductosApiEndPoint", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            if(respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var informacion = JsonSerializer.Deserialize<List<Categoria>>(contenido, opciones);
                Categorias = informacion.Select(c => new SelectListItem 
                { 
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                }).ToList();
            }
        }

        #endregion
    }
}
