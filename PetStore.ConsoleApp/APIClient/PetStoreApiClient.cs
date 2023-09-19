using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp.APIClient
{
    public class PetStoreApiClient : IPetStoreApiClient
    {
        private readonly IConfiguration _configuration;


        public PetStoreApiClient(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public async Task<List<Pet>> FetchAvailablePetsAsync()
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
                        return JsonConvert.DeserializeObject<List<Pet>>(jsonResponse);
                    }
                    else
                    {
                        // Log the error and throw a custom exception
                        string errorMessage = $"Failed to retrieve data. Status code: {response.StatusCode}";
                        Logger.LogError(new Exception(errorMessage));
                        throw new CustomApiException(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
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
