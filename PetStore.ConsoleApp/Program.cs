using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PetStore.ConsoleApp;
using PetStore.ConsoleApp.APIClient;
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
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var httpClient = new HttpClient(); // Create an instance of HttpClient

            // Use dependency injection to resolve IPetStoreApiClient
            IPetStoreApiClient<Pet> petStoreApiClient = new PetStoreApiClient<Pet>(configuration, httpClient);
            List<Pet> availablePets = await petStoreApiClient.FetchDataAsync();
            OutputHandler.PrintPets(availablePets);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Register the HttpClient as a singleton
                services.AddSingleton<HttpClient>();

                // Register IPetStoreApiClient<Pet> and provide the HttpClient instance
                services.AddTransient<IPetStoreApiClient<Pet>, PetStoreApiClient<Pet>>();
            });
}
