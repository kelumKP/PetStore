using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp.APIClient
{
    // Region: PetStoreApiClient Class

    /// <summary>
    /// Represents a generic Pet Store API client for fetching data of a specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of data to fetch from the API.</typeparam>
    public class PetStoreApiClient<T> : IPetStoreApiClient<T>
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PetStoreApiClient{T}"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        /// <param name="httpClient">The HTTP client for making API requests.</param>
        public PetStoreApiClient(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Asynchronously fetches data from the Pet Store API.
        /// </summary>
        /// <returns>A list of data of the specified type.</returns>
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

        // Region: Private Methods

        /// <summary>
        /// Loads API settings from configuration.
        /// </summary>
        /// <returns>An instance of <see cref="ApiSettings"/> containing API configuration.</returns>
        private ApiSettings LoadApiSettings()
        {
            var apiSettings = new ApiSettings();
            _configuration.GetSection("ApiSettings").Bind(apiSettings);

            return apiSettings;
        }

        // End of Region: Private Methods
    }

    // End of Region: PetStoreApiClient Class
}
