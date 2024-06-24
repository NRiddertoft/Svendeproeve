using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Shared.Models;

namespace EmployeeHangfireCron.Algolia
{
    public class AlgoliaHelper
    {
        private static AlgoliaSettings? _algoliaSettings;

        public async Task<SearchResponse<User>> IndexUsers(AlgoliaSettings algoliaSettings)
        {
            var users = new List<User>();

            _algoliaSettings = algoliaSettings;
            SearchClient client = new SearchClient(_algoliaSettings.ApplicationId, _algoliaSettings.WriteApiKey);

            // Create an index (or connect to it, if an index with the name `ALGOLIA_INDEX_NAME` already exists)
            // https://www.algolia.com/doc/api-client/getting-started/instantiate-client-index/#initialize-an-index
            SearchIndex index = client.InitIndex("your_index_name");

            // Add new objects to the index
            // https://www.algolia.com/doc/api-reference/api-methods/add-objects/
            var obj = users;
            var res = await index.SaveObjectsAsync(obj);

            // Wait for the indexing task to complete
            // https://www.algolia.com/doc/api-reference/api-methods/wait-task/
            res.Wait();

            // Search the index for "Fo"
            // https://www.algolia.com/doc/api-reference/api-methods/search/
            var search = index.Search<User>(new Query(""));
            Console.WriteLine(search.Hits.ElementAt(0).ToString());

            return search;
        }
    }
}
