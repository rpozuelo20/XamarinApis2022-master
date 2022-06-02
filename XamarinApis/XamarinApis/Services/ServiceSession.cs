using System;
using System.Collections.Generic;
using System.Text;
using XamarinApis.Models;

namespace XamarinApis.Services
{
    public class ServiceSession
    {
        //  Aqui tendremos todas las propiedades de los objetos que deseemos almacenar:
        public List<Doctor> Favoritos { get; set; }
        public ServiceSession()
        {
            this.Favoritos = new List<Doctor>();
        }
    }
}
