using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetStore.ConsoleApp;
using PetStore.ConsoleApp.APIClient;
using PetStore.Models;
class Program
{
    static async Task Main()
    {
        try
        {
            // Load configuration settings from appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Create an instance of HttpClient to make API requests
            var httpClient = new HttpClient();

            // Use dependency injection to resolve IPetStoreApiClient
            IPetStoreApiClient<Pet> petStoreApiClient = new PetStoreApiClient<Pet>(configuration, httpClient);

            // Fetch the list of available pets from the API
            List<Pet> availablePets = await petStoreApiClient.FetchDataAsync();

            // Print the list of available pets
            OutputHandler.PrintPets(availablePets);
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur
            Logger.LogError(ex);
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Register the HttpClient as a singleton to be reused across the application
                services.AddSingleton<HttpClient>();

                // Register IPetStoreApiClient<Pet> and provide the HttpClient instance
                services.AddTransient<IPetStoreApiClient<Pet>, PetStoreApiClient<Pet>>();
            });
}