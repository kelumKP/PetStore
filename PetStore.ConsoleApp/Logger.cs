namespace PetStore.ConsoleApp
{
    public class Logger
    {
        // Region: Public Methods

        /// <summary>
        /// Logs an error message to the console.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public static void LogError(Exception ex)
        {
            // Log the error to the console
            Console.WriteLine($"Error: {ex.Message}");

            // You can add code here to log to a file, database, or any other desired destination.
        }

        // End of Region: Public Methods
    }
}