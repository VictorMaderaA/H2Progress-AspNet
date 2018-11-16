namespace Hack2ProgressAspMvc.Models
{
    using Microsoft.Azure.Documents;
    using Newtonsoft.Json;
    
    public class Electrodomestico
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "tipo")]
        public TipoElectrodomestico TipoElectrodomestico { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }


        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty(PropertyName = "consumo")]
        public double Consumo { get; set; }

    }

    public class TomaLuz
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "posicion")]
        public Pair Electrodomesticos { get; set; }

        [JsonProperty(PropertyName = "cantidad")]
        public int Cantidad { get; set; }


        [JsonProperty(PropertyName = "potencia")]
        public int Potencia { get; set; }
}

}