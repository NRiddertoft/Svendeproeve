using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Shared.Models;

namespace EmployeeHangfireCron.Algolia
{
    public class AlgoliaHelper
    {
        public static AlgoliaSettings LoadAlgoliaSettings()
        {
            // Load settings
            IConfiguration config = new ConfigurationBuilder()
                // appsettings.json is required
                .AddJsonFile("appsettings.json", optional: false)
                // appsettings.Development.json" is optional, values override appsettings.json
                .AddJsonFile($"appsettings.Development.json", optional: true)
                // User secrets are optional, values override both JSON files
                .AddUserSecrets<Program>()
                .Build();

            return config.GetRequiredSection("AlgoliaSettings").Get<AlgoliaSettings>() ??
                   throw new Exception("Could not load app settings.");
        }
    }
}
