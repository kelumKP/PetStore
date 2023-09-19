using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp.APIClient
{
    // Region: IPetStoreApiClient Interface

    /// <summary>
    /// Represents an interface for a Pet Store API client that fetches data of a specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of data to fetch from the API.</typeparam>
    public interface IPetStoreApiClient<T>
    {
        /// <summary>
        /// Asynchronously fetches data from the Pet Store API.
        /// </summary>
        /// <returns>A list of data of the specified type.</returns>
        Task<List<T>> FetchDataAsync();
    }

    // End of Region: IPetStoreApiClient Interface
}
