using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp.APIClient
{
    public class PetStoreApiClient<T> : IPetStoreApiClient<T>
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PetStoreApiClient(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<T>> FetchDataAsync()
        {
            try
            {
                var apiSettings = LoadApiSettings();

                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"{apiSettings.BaseApiUrl}/{apiSettings.ApiEndpoint}?status={apiSettings.Status}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<T>>(jsonResponse);
                    }
                    else
                    {
                        // Log the error
                        string errorMessage = $"Failed to retrieve data. Status code: {response.StatusCode}";
                        Logger.LogError(new Exception(errorMessage));
                        return null; // Return a default value or handle the error case as needed
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw; // This line can be safely removed
            }
        }

        private ApiSettings LoadApiSettings()
        {
            var apiSettings = new ApiSettings();
            _configuration.GetSection("ApiSettings").Bind(apiSettings);

            return apiSettings;
        }
    }
}