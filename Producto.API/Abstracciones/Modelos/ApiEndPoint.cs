namespace Abstracciones.Modelos
{
    public class ApiEndPoint
    {

        public string UrlBase { get; set; }
        public List<Metodo> Metodos { get; set; }

    }

    public class Metodo
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}
