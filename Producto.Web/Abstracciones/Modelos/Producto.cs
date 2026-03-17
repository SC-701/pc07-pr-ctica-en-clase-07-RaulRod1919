using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        [StringLength(50, ErrorMessage = "El nombre tiene que ser superior a 2 caracteres y menor a 50", MinimumLength = 3)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La propiedad descripción es requerida")]
        [StringLength(100, ErrorMessage = "La descripcion tiene que ser superior a 2 caracteres y menor a 100", MinimumLength = 3)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La propiedad precio es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "La propiedad stock es requerida")]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Stock solo acepta valores númericos iguales o mayores que 0")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "La propiedad código de barras es requerida")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "El código de barras solo acepta valores númericos con un tamaño igual a 13 caracteres #############")]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoRequest
    {
        public Guid Id { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }
    }

    public class ProductoDetalle : ProductoResponse
    {
        public decimal PrecioUSD { get; set; }
    }

}
