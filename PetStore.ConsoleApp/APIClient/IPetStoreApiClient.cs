using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp.APIClient
{
    public interface IPetStoreApiClient<T>
    {
        Task<List<T>> FetchDataAsync();
    }
}
