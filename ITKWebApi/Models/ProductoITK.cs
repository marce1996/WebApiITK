using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITKWebApi.Models
{
    public class ProductoITK
    {
        public int id { get; set; }
        public string SKU { get; set; }
        public string Fert { get; set; }
        public string Modelo { get; set; }
        public string Tipo { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime Fecha { get; set; }
    }
}
