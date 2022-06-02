using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApis.Models
{
    public class Coche
    {
        public int IdCoche { get; set; }
        [JsonProperty("marca")]
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Conductor { get; set; }
        public string Imagen { get; set; }
    }
}
