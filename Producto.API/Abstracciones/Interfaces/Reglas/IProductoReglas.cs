namespace Abstracciones.Interfaces.Reglas
{
    public interface IProductoReglas
    {

        Task<decimal?> ObtenerValorUSD(decimal precio);

    }
}
