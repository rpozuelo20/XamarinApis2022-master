using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApis.Models
{
    public class Doctor
    {
        public int IdDoctor { get; set; }
        public int IdHospital { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public int Salario { get; set; }

        public bool Favorito { get; set; }
    }
}
