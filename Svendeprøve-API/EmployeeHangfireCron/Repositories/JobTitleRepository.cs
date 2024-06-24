using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared;

namespace GraphCronJob.Repositories
{
    public class JobTitleRepository
    {
        private readonly Context _context;

        public JobTitleRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<JobTitle>> GetJobTitles()
        {
            return await _context.JobTitles.ToListAsync();
        }

        public async Task PostJobTitle(JobTitle jobTitle)
        {
            try
            {
                _context.JobTitles.Add(jobTitle);
                await _context.SaveChangesAsync();

                var jobTitles = new List<JobTitle> { jobTitle };

                // TODO: Add algolia
                //var algJobTitles = AlgoliaHelperJobTitles.TransformToAlgolia(jobTitles);
                //await AlgoliaHelperJobTitles.Index(algJobTitles);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteJobTitle(int id)
        {
            try
            {
                var jobTitle = await _context.JobTitles.FindAsync(id);
                if (jobTitle == null)
                {
                    return;
                }

                _context.JobTitles.Remove(jobTitle);
                await _context.SaveChangesAsync();


                // TODO: Add algolia
                //List<string> idsToDelete = new() { id.ToString() };

                //await AlgoliaHelperJobTitles.Delete(idsToDelete);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
