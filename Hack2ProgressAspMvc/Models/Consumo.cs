using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hack2ProgressAspMvc.Models
{
    public class Consumo
    {
        public int Id { get; set; }
        public int IdToma { get; set; }
        public float ConsumoEnergetico { get; set; }
        public DateTime Fecha { get; set; }
    }
}