namespace Hack2ProgressAspMvc.Models
{
    using Microsoft.Azure.Documents;
    using Newtonsoft.Json;
    
    public class Habitacion
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "electrodomesticos")]
        public Electrodomestico[] Electrodomesticos { get; set; }

        [JsonProperty(PropertyName = "tomasLuz")]
        public TomaLuz[] TomasLuz { get; set; }

            //[JsonProperty(PropertyName = "extras")]

        }

}