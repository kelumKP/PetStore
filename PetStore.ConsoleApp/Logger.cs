using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp
{
    public class Logger
    {
        public static void LogError(Exception ex)
        {
            // Log the error to the console and optionally to a log file
            Console.WriteLine($"Error: {ex.Message}");
            // You can add code here to log to a file, database, or any other desired destination.
        }
    }
}
