using Shared.Models;
using Algolia.Search.Clients;
using Algolia.Search.Http;
using System;

namespace Shared.Helpers
{
    public static class AlgoliaHelperUsers
    {
        private const string ApiKey = "1be1d91c9d2d53cedf27773b7de48380";
        private const string AppId = "TCLIGR23SS";
        private const string IndexName = "dev_TeamFinder";

        public static async Task Index(IEnumerable<AlgoliaUser> users)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            await index.SaveObjectsAsync(users);
        }

        public static async Task Delete(IEnumerable<string> objectIds)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            await index.DeleteObjectsAsync(objectIds);
        }

        public static async Task PartialUpdate(IEnumerable<AlgoliaUser> users)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.PartialUpdateObjectsAsync(users, new RequestOptions());
        }

        public static IEnumerable<AlgoliaUser> TransformToAlgolia(IEnumerable<User> users)
        {
            var algoliaUsers = users.Select(user => new AlgoliaUser
            {
                ObjectID = user.AzureId,
                DisplayName = user.DisplayName,
                Solutions = user.Solutions?.Select(solution => new AlgoliaSolution()
                {
                    SolutionName = solution.Title
                }).ToList(),
                JobTitle = user.JobTitle,
                OfficeLocation = user.OfficeLocation,
                Skills = user.Skills?.Select(skill => new AlgoliaSkill
                {
                    Tag = skill.Title
                }).ToList(),
                UserPrincipalName = user.UserPrincipalName,
                HasImage = user.ImageSize != null && user.ImageSize != 0,
            }).ToList();

            return algoliaUsers;
        }
    }
}