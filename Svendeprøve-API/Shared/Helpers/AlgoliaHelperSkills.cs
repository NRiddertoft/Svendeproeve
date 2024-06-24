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
    public static class AlgoliaHelperSkills
    {
        private const string ApiKey = "1be1d91c9d2d53cedf27773b7de48380";
        private const string AppId = "TCLIGR23SS";
        private const string IndexName = "dev_TeamFinder_Skills";

        public static async Task Index(IEnumerable<AlgoliaSkill> skills)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            await index.SaveObjectsAsync(skills);
        }

        public static async Task PartialUpdate(IEnumerable<AlgoliaSkill> skills)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.PartialUpdateObjectsAsync(skills, new RequestOptions());
        }

        public static async Task Delete(IEnumerable<string> objectIds)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.DeleteObjectsAsync(objectIds, new RequestOptions());
        }

        public static IEnumerable<AlgoliaSkill> TransformToAlgolia(IEnumerable<Skill> skills)
        {
            var algoliaSkills = skills.Select(skill => new AlgoliaSkill
            {
                ObjectID = skill.Id.ToString(),
                Tag = skill.Title,
                Category = skill.Category,
            }).ToList();

            return algoliaSkills;
        }
    }
}
