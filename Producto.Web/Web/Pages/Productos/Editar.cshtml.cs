using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EditarModel : PageModel
    {
        [BindProperty]
        public ProductoResponse Producto { get; set; } = default!;
        [BindProperty]
        [Required(ErrorMessage = "Categoría necesaría")]
        public List<SelectListItem> Categorias { get; set; } = default!;
        [BindProperty]
        [Required(ErrorMessage = "SubCategoría necesaría")]
        public List<SelectListItem> SubCategorias { get; set; } = default!;
        [BindProperty]
        public Guid CategoriaSeleccionada { get; set; }
        [BindProperty]
        public Guid SubCategoriaSeleccionada { get; set; }

        private readonly IConfiguracion _configuration;

        public EditarModel(IConfiguracion configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null)
                return NotFound();
            string endpoint = _configuration.ObtenerMetodo("ProductosApiEndPoint", "ObtenerProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await ObtenerCategorias();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
                if (Producto != null)
                {
                    CategoriaSeleccionada = Guid.Parse(Categorias.Where(m => m.Text == Producto.Categoria).FirstOrDefault().Value);
                    SubCategorias = (await ObtenerSubCategorias(CategoriaSeleccionada)).Select(m =>
                    new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Nombre,
                        Selected = m.Nombre == Producto.SubCategoria
                    }).ToList();
                    SubCategoriaSeleccionada = Guid.Parse(SubCategorias.Where(m => m.Text == Producto.SubCategoria).FirstOrDefault().Value);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await ObtenerCategorias();
                return Page();
            }
            string endpoint = _configuration.ObtenerMetodo("ProductosApiEndPoint", "EditarProducto");
            var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(string.Format(endpoint, Producto.Id),
                new ProductoRequest
                {
                    IdSubCategoria = SubCategoriaSeleccionada,
                    CodigoBarras = Producto.CodigoBarras,
                    Descripcion = Producto.Descripcion,
                    Nombre = Producto.Nombre,
                    Precio = Producto.Precio,
                    Stock = Producto.Stock
                });
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
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
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, idCategoria));
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
            if (respuesta.IsSuccessStatusCode)
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
