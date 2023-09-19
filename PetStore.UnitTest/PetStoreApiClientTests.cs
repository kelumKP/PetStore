using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using PetStore.ConsoleApp;
using PetStore.ConsoleApp.APIClient;
using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.UnitTest
{
    [TestFixture]
    public class PetStoreApiClientTests
    {
        // Region: FetchAvailablePetsAsync Failure Test

        [Test]
        public async Task FetchAvailablePetsAsync_Failure()
        {
            // Arrange

            // Create a mock IConfiguration
            var configuration = new Mock<IConfiguration>();

            // Define API settings
            var apiSettings = new ApiSettings
            {
                BaseApiUrl = "https://example.com",
                ApiEndpoint = "pet/findByStatus",
                Status = "available"
            };

            // Configure IConfiguration to return the API settings
            configuration.Setup(c => c.GetSection("ApiSettings"))
                .Returns(new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "ApiSettings:BaseApiUrl", apiSettings.BaseApiUrl },
                        { "ApiSettings:ApiEndpoint", apiSettings.ApiEndpoint },
                        { "ApiSettings:Status", apiSettings.Status },
                    })
                    .Build()
                    .GetSection("ApiSettings"));

            // Create a mock HTTP message handler
            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClient = new HttpClient(handler.Object);

            // Simulate a failure response
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError, // Simulate a failure status code
                    Content = new StringContent("Internal Server Error"),
                });

            // Create an instance of PetStoreApiClient
            var petStoreApiClient = new PetStoreApiClient<Pet>(configuration.Object, httpClient);

            try
            {
                // Act
                List<Pet> availablePets = await petStoreApiClient.FetchDataAsync();

                // Assert
                Assert.IsNull(availablePets); // Ensure that pets are null in case of failure
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception if needed
                Console.WriteLine($"HttpRequestException: {ex.Message}");
            }
        }

        // End of Region: FetchAvailablePetsAsync Failure Test

        // Region: FetchAvailablePetsAsync Success Test

        [Test]
        public async Task FetchAvailablePetsAsync_Success()
        {
            // Arrange

            // Create a mock IConfiguration
            var configuration = new Mock<IConfiguration>();

            // Define API settings
            var apiSettings = new ApiSettings
            {
                BaseApiUrl = "https://petstore.swagger.io/v2",
                ApiEndpoint = "pet/findByStatus",
                Status = "available"
            };

            // Configure IConfiguration to return the API settings
            configuration.Setup(c => c.GetSection("ApiSettings"))
                .Returns(new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "ApiSettings:BaseApiUrl", apiSettings.BaseApiUrl },
                        { "ApiSettings:ApiEndpoint", apiSettings.ApiEndpoint },
                        { "ApiSettings:Status", apiSettings.Status },
                    })
                    .Build()
                    .GetSection("ApiSettings"));

            // Create a mock HTTP message handler
            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClient = new HttpClient(handler.Object);

            // Create an instance of PetStoreApiClient
            var petStoreApiClient = new PetStoreApiClient<Pet>(configuration.Object, httpClient);

            try
            {
                // Act
                List<Pet> availablePets = await petStoreApiClient.FetchDataAsync();

                // Assert
                Assert.IsNotNull(availablePets); // Ensure that pets are not null in case of success

                // Additional assertions can be added to validate the behavior of the successful response
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception if needed
                Console.WriteLine($"HttpRequestException: {ex.Message}");
            }
        }

        // End of Region: FetchAvailablePetsAsync Success Test
    }
}
