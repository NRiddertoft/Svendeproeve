namespace EmployeeHangfireCron.Algolia
{
    public class AlgoliaSettings
    {
        public string? ApplicationId { get; set; }
        public string? WriteApiKey { get; set; }
        public string? Index { get; set; }

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
