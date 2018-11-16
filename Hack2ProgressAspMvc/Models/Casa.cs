namespace Hack2ProgressAspMvc.Models
{
    using Microsoft.Azure.Documents;
    using Newtonsoft.Json;

    public class Casa
    {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "idHabitacion")]
        public int Habitacion { get; set; }


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