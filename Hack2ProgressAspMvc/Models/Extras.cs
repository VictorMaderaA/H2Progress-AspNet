namespace Hack2ProgressAspMvc.Models
{
    public class Pair
    {

        public int first, second;

        Pair(int first, int second)
        {

            this.first = first;
            this.second = second;

        }
    }

    public enum TipoElectrodomestico
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

