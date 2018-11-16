namespace Hack2ProgressAspMvc.Models
{
    using Microsoft.Azure.Documents;
    using Newtonsoft.Json;

    public class Casa
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "habitaciones")]
        public Habitacion[] Habitaciones { get; set; }


        /*[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }*/
    }
}