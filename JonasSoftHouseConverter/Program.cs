using JonasSoftHouseConverter;
using JonasSoftHouseConverter.Extensions;
using JonasSoftHouseConverter.Helpers;
using JonasSoftHouseConverter.Models;
bool showMenu = true;
string filePath = string.Empty;
MigrationResult response = new MigrationResult();

while (showMenu)
{
    Console.Clear();

    ConsoleHelper.PrintWelcome();
    
    // Print message if any
    if (!string.IsNullOrEmpty(response.Message))
        ConsoleHelper.PrintMessage(response, filePath);
    
    // Print a menu
    ConsoleHelper.PrintMenu();

    // Handle menu selections
    switch (Console.ReadLine())
    {
        case "1":
            // Migrate cvs to XML
            Console.WriteLine("Enter the full path of the file (*.cvs)");
            var fileName = Console.ReadLine();
            if (!string.IsNullOrEmpty(fileName))
            {
                response = new Migrator(fileName, Constants.CVS_DELIMITER).Result;
                filePath = response.Success ? fileName.Substring(0, fileName.LastIndexOf('\\')) : string.Empty;
            }
            else
            {
                response.NoFileResult();
            }
            showMenu = true;
            break;
        case "2":
            // Exit
            Console.WriteLine("Good bye!");
            showMenu = false;
            break;
        default:
            response.CommandNotImplementedResult();
            showMenu = true;
            break;
    }
}
