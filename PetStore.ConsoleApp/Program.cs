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


            // Use dependency injection to resolve IPetStoreApiClient
            IPetStoreApiClient petStoreApiClient = new PetStoreApiClient(configuration);

            List<Pet> availablePets = await petStoreApiClient.FetchAvailablePetsAsync();
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
            services.AddTransient<IPetStoreApiClient, PetStoreApiClient>();
        });
}
