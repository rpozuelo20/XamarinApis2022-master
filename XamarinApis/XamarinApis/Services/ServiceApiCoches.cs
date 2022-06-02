using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinApis.Models;

namespace XamarinApis.Services
{
    public class ServiceApiCoches
    {
        private string _UrlApi;

        public ServiceApiCoches() {
            this._UrlApi = "https://apicochespaco.azurewebsites.net/";
        }

        public async Task<List<Coche>> GetCochesAsync()
        {
            string request = "/webresources/coches";
            using (HttpClient client = new HttpClient())
            {
                //LO UNICO QUE RECOMIENDO ES NO UTILIZAR 
                //BASEADDRESS YA QUE NO VA BIEN, DEPENDE DE LAS 
                //VERSIONES DEL DISPOSITIVO QUE DA ERROR.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));
                Uri uri = new Uri(this._UrlApi + request);
                HttpResponseMessage response =
                    await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string data =
                        await response.Content.ReadAsStringAsync();
                    List<Coche> coches =
                        JsonConvert.DeserializeObject<List<Coche>>(data);
                    return coches;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
