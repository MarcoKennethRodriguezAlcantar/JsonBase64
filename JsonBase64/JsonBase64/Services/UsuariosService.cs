using JsonBase64.Helpers;
using JsonBase64.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonBase64.Services
{
    public class UsuariosService
    {
        HttpClient client;
        private readonly string API_USUARIOS = "Usuarios";      

        public UsuariosService()
        {
#if DEBUG
            var handler = new BypassSslValidationClientHandler();            
            client = new HttpClient(handler);
#else
            client = new HttpClient();
#endif
        }

        public async Task<string> RegisterAsync(Usuario usuario)
        {     
           
            string result = string.Empty;            
            if (usuario != null && !string.IsNullOrEmpty(usuario.UserName) && !string.IsNullOrEmpty(usuario.Password))
            {
                result = JsonSerializer.Serialize(usuario);
                StringContent content = new StringContent(result, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                
                response = await client.PostAsync(AppResources.APIResources.APIHOST + API_USUARIOS, content);
                if (response.IsSuccessStatusCode)
                {
                    var contenido = response.Content;
                    result = await contenido.ReadAsStringAsync();
                }


            }
            return result;
        }
    }
}
