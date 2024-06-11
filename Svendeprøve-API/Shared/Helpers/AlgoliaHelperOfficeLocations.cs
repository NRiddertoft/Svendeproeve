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
    public static class AlgoliaHelperOfficeLocations
    {
        private const string ApiKey = "1be1d91c9d2d53cedf27773b7de48380";
        private const string AppId = "TCLIGR23SS";
        private const string IndexName = "dev_TeamFinder_OfficeLocations";

        public static async Task Index(IEnumerable<AlgoliaOfficeLocation> officeLocations)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            await index.SaveObjectsAsync(officeLocations);
        }

        public static async Task PartialUpdate(IEnumerable<AlgoliaOfficeLocation> officeLocations)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.PartialUpdateObjectsAsync(officeLocations, new RequestOptions());
        }

        public static async Task Delete(IEnumerable<string> objectIds)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.DeleteObjectsAsync(objectIds, new RequestOptions());
        }

        public static IEnumerable<AlgoliaOfficeLocation> TransformToAlgolia(IEnumerable<OfficeLocation> officeLocations)
        {
            var algoliaOfficeLocation = officeLocations.Select(officeLocation => new AlgoliaOfficeLocation
            {
                ObjectID = officeLocation.Id.ToString(),
                LocationName = officeLocation.LocationName
            }).ToList();

            return algoliaOfficeLocation;
        }
    }
}