using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Productos
{
    public class EditarModel : PageModel
    {
        [BindProperty]
        public ProductoRequest Producto { get; set; } = default!;
        public void OnGet()
        {
        }
    }
}
