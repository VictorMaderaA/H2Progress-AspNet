using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Hack2ProgressAspMvc.Models;
using Newtonsoft.Json;

namespace Hack2ProgressAspMvc.Models
{
    public class Hogar
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "habitaciones")]
        public string[] Habitaciones { get; set; }
    }
}