namespace PetStore.ConsoleApp
{
    public class ApiSettings
    {
        // Region: Properties

        /// <summary>
        /// Gets or sets the base URL of the Pet Store API.
        /// </summary>
        public string BaseApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the status of pets to search for (e.g., "available").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the API endpoint for finding pets by status.
        /// </summary>
        public string ApiEndpoint { get; set; }

        // End of Region: Properties
    }
}