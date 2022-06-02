using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using XamarinApis.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace XamarinApis.Services
{
    public class ServiceApiDoctores
    {
        //  Creamos la propiedad privada de la url y el header:
        private string _UrlApi;
        private MediaTypeWithQualityHeaderValue _Header;
        //  Creamos el constructor del service:
        public ServiceApiDoctores(IConfiguration configuration)
        {
            this._UrlApi = configuration["ApiUrls:ApiDoctores"];
            this._Header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        //  Metodo que nos permitira realizar las llamadas a la api:
        private async Task<T> CallApiASync<T>(string request)
        {
            using(HttpClient client = new HttpClient())
            {
                Uri uri = new Uri(this._UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this._Header);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        //  Metodo que nos permitira recuperar los doctores:
        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string request = "/api/doctores";
            List<Doctor> doctores = await this.CallApiASync<List<Doctor>>(request);
            return doctores;
        }
        //  Metodo que nos permitira recuperar un doctor:
        public async Task<Doctor> FindDoctor(int id)
        {
            string request = "/api/doctores/" + id;
            Doctor doctor = await this.CallApiASync<Doctor>(request);
            return doctor;
        }
        //  Metodo que nos permitira eliminar un doctor:
        public async Task DeleteDoctorAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/doctores/" + id;
                Uri uri = new Uri(this._UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this._Header);
                HttpResponseMessage response = await client.DeleteAsync(uri);
            }
        }
    }
}
