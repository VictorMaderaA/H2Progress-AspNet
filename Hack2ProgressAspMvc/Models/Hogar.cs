using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hack2ProgressAspMvc.Models;
using Newtonsoft.Json;

namespace Hack2ProgressAspMvc.Models
{
    public class Hogar
    {
        [JsonProperty(PropertyName = "id")]

        public int Id { get; set; }
        public string Nombre { get; set; }
        public Habitacion[] Habitaciones { get; set; }
    }
}