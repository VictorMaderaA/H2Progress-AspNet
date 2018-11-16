using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hack2ProgressAspMvc.Models
{
    public class Hogar
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Object[] Habitaciones { get; set; }
    }
}