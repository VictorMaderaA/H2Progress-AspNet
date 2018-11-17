namespace Hack2ProgressAspMvc.Models
{
    public class TomaEnergia
    {
        public int Id { get; set; }
        public PosicionEnum Posicion { get; set; }

        public int IdHabitacion { get; set; }
    }

    public enum PosicionEnum
    {
        Arriba,
        Izquierda,
        Derecha,
        Abajo
    }
}