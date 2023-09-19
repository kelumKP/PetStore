using Moq.Protected;
using Moq;
using System.Net;
using PetStore.ConsoleApp.APIClient;
using PetStore.Models;
using PetStore.ConsoleApp;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PetStore.UnitTest
{
    [TestFixture]
    public class PetStoreApiClientTests
    {
        [Test]
        public async Task FetchAvailablePetsAsync_Success()
        {
        }

        [Test]
        public async Task FetchAvailablePetsAsync_Failure()
        {
        }

    }
}
