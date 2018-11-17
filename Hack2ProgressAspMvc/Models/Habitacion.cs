namespace Hack2ProgressAspMvc.Models
{
    public class Habitacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoHabitacionEnum Tipo { get; set; }
        public TomaEnergia[] TomasEnergia { get; set; }


    }

    public enum TipoHabitacionEnum
    {
        Baño,
        Cocina,
        Salon,
        Habitacion,
        SalonExterior,
        Otros

    }
}