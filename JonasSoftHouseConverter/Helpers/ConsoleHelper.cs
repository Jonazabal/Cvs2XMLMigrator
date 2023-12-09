using JonasSoftHouseConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonasSoftHouseConverter.Helpers
{
    public static class ConsoleHelper
    {
        public static void PrintMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Migrate file");
            Console.WriteLine("2. Exit");
            Console.Write("\r\nSelect an option: ");
        }

        public static void PrintMessage(MigrationResult response, string filePath)
        {
            Console.ForegroundColor = response.Success ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"Message: {response.Message}");
            if (response.Success)
                Console.WriteLine($"File saved to {filePath}{Path.DirectorySeparatorChar}{Constants.OUTPUT_FILE_NAME}\r\n");
            Console.WriteLine("");
            Console.ResetColor();
        }
    }
}
