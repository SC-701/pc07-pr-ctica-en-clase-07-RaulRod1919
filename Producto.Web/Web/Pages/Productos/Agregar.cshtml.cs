using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Productos
{
    public class AgregarModel : PageModel
    {
        [BindProperty]
        public ProductoRequest Producto { get; set; } = default!;
        public void OnGet()
        {
        }
    }
}
