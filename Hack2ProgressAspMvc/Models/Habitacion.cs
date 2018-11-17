using Newtonsoft.Json;

namespace Hack2ProgressAspMvc.Models
{
    public class Habitacion
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "tipo")]
        public TipoHabitacionEnum Tipo { get; set; }

        [JsonProperty(PropertyName = "tomasEnergia")]
        public string TomasEnergia { get; set; }


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