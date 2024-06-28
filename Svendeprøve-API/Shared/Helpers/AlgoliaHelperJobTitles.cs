﻿using Algolia.Search.Clients;
using Algolia.Search.Http;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class AlgoliaHelperJobTitles
    {
        public static async Task Index(IEnumerable<AlgoliaJobTitle> jobTitles)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            await index.SaveObjectsAsync(jobTitles);
        }

        public static async Task PartialUpdate(IEnumerable<AlgoliaJobTitle> jobTitles)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.PartialUpdateObjectsAsync(jobTitles, new RequestOptions());
        }

        public static async Task Delete(IEnumerable<string> objectIds)
        {
            var client = new SearchClient(AppId, ApiKey);
            var index = client.InitIndex(IndexName);

            // TODO: Set autoGenerateObjectIDIfNotExist to true
            await index.DeleteObjectsAsync(objectIds, new RequestOptions());
        }

        public static IEnumerable<AlgoliaJobTitle> TransformToAlgolia(IEnumerable<JobTitle> jobTitles)
        {
            var algoliaJobTitles = jobTitles.Select(jobTitle => new AlgoliaJobTitle
            {
                ObjectID = jobTitle.Id.ToString(),
                Title = jobTitle.Title
            }).ToList();

            return algoliaJobTitles;
        }
    }
}
