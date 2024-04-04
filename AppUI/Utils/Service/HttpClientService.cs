using AppUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppUI.Utils.Service
{
    internal class HttpClientService : HttpClientBase
    {

        private readonly HttpClient _httpClient;

        public HttpClientService()
        {
            _httpClient = new HttpClient();
        }

        public override async Task<T> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonSerializer.Deserialize<T>(jsonResponse);
            }
            catch (JsonException e)
            {
                // Considera logear el jsonResponse para inspeccionarlo
                Console.WriteLine($"Error al deserializar el JSON: {e.Message}");
                Console.WriteLine($"JSON recibido: {jsonResponse}");
                throw; // O manejar de otra manera, según sea necesario
            }
        }

    }
}
