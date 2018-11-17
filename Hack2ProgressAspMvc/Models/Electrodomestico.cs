namespace Hack2ProgressAspMvc.Models
{
    public class Electrodomestico
    {
        public string Nombre { get; set; }
        public TipoElectrodomesticoEnum Tipo { get; set; }
        public string Descripcion { get; set; }

    }

    public enum TipoElectrodomesticoEnum
    {
        Refrigerador,
        Lavavajillas,
        Lavadoras,
        Secadoras,
        Lampara,
        Horno,
        Aire,
        Extra
    }
}