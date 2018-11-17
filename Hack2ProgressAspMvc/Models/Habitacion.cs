using Newtonsoft.Json;

namespace Hack2ProgressAspMvc.Models
{
    public class Habitacion
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        public int IdHogar { get; set; }
    }
}