using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PetStore.ConsoleApp;
using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        try
        {
            List<Pet> availablePets = await FetchAvailablePetsAsync();

            PrintPets(availablePets);
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    #region API Methods

    /// <summary>
    /// Fetches a list of available pets from the API.
    /// </summary>
    /// <returns>The list of available pets.</returns>
    static async Task<List<Pet>> FetchAvailablePetsAsync()
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
                    LogError(new Exception($"Failed to retrieve data. Status code: {response.StatusCode}"));
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

    /// <summary>
    /// Loads API settings from the configuration file.
    /// </summary>
    /// <returns>The API settings.</returns>
    static ApiSettings LoadApiSettings()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var apiSettings = new ApiSettings();
        configuration.GetSection("ApiSettings").Bind(apiSettings);

        return apiSettings;
    }

    #endregion

    #region Logging

    /// <summary>
    /// Logs an error to the console and optionally to a log file.
    /// </summary>
    /// <param name="ex">The exception to log.</param>
    static void LogError(Exception ex)
    {
        // Log the error to the console and optionally to a log file
        Console.WriteLine($"Error: {ex.Message}");
        Trace.WriteLine($"Error: {ex}");
    }

    #endregion

    #region Output

    /// <summary>
    /// Prints the list of pets to the console.
    /// </summary>
    /// <param name="pets">The list of pets to print.</param>
    static void PrintPets(List<Pet> pets)
    {
        var sortedPets = pets
            .OrderByDescending(pet => pet.category?.name)
            .ThenByDescending(pet => pet.name);

        foreach (var pet in sortedPets)
        {
            Console.WriteLine(pet.ToString());
            Console.WriteLine();
        }
    }

    #endregion
}
