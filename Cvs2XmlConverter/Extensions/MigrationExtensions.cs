using JonasSoftHouseConverter.Models;

namespace JonasSoftHouseConverter.Extensions
{
    public static class MigrationExtensions
    {
        /// <summary>
        /// Creates a MigrationResult representing file not found
        /// </summary>
        /// <param name="result"></param>
        public static void NoFileResult(this MigrationResult result)
        {
            result.Success = false;
            result.Message = "Could not find the file";
        }

        public static void CommandNotImplementedResult(this MigrationResult result)
        {
            result.Message = "The command is not implemented, please try again";
            result.Success = false;
        }
    }
}
