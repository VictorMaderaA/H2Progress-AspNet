using Newtonsoft.Json;

namespace Hack2ProgressAspMvc.Models.temp
{
    public class Habitacion
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "electrodomesticos")]
        public Electrodomestico[] Electrodomesticos { get; set; }

        [JsonProperty(PropertyName = "tomasLuz")]
        public TomaLuz[] TomasLuz { get; set; }

    }

}