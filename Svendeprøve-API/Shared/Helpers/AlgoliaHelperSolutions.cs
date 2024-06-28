using Algolia.Search.Clients;
using Algolia.Search.Http;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class AlgoliaHelperSolutions
    {
        private const string ApiKey = "7f557e37d42067a0fd517105470c4dce";
        private const string AppId = "W8HB85WO7P";
        private const string IndexName = "dev_TeamFinder_Solutions";

        public static async Task Index(IEnumerable<AlgoliaSolution> solutions)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            await index.SaveObjectsAsync(solutions);
        }

        public static async Task PartialUpdate(IEnumerable<AlgoliaSolution> solutions)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.PartialUpdateObjectsAsync(solutions, new RequestOptions());
        }

        public static IEnumerable<AlgoliaSolution> TransformToAlgolia(IEnumerable<Solution> solutions)
        {
            var algoliaSolutions = solutions.Select(solution => new AlgoliaSolution
            {
                ObjectID = solution.Id.ToString(),
                SolutionName = solution.Title,
                Link = solution.Link
            }).ToList();

            return algoliaSolutions;
        }

        public static async Task Delete(IEnumerable<string> idsToDelete)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.DeleteObjectsAsync(idsToDelete, new RequestOptions());
        }
    }
}
