using EmployeeHangfireCron.Algolia;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Helpers;
using Shared.Models;

namespace GraphCronJob.Repositories
{
    public class OfficeLocationRepository
    {
        private readonly Context _context;

        public OfficeLocationRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<OfficeLocation>> GetOfficeLocations()
        {
            return await _context.OfficeLocations.ToListAsync();
        }

        public async Task PostOfficeLocation(OfficeLocation location, AlgoliaSettings algoliaSettings)
        {
            try
            {
                _context.OfficeLocations.Add(location);
                await _context.SaveChangesAsync();

                var locations = new List<OfficeLocation> { location };


                // Transform locations to Algolia format
                var algLocations = AlgoliaHelperOfficeLocations.TransformToAlgolia(locations);

                // Index locations with Algolia settings
                await AlgoliaHelperOfficeLocations.Index(algLocations, algoliaSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteOfficeLocation(int id, AlgoliaSettings algoliaSettings)
        {
            try
            {
                var location = await _context.OfficeLocations.FindAsync(id);
                if (location == null)
                {
                    return;
                }

                _context.OfficeLocations.Remove(location);
                await _context.SaveChangesAsync();

                List<string> idsToDelete = new() { id.ToString() };

                await AlgoliaHelperOfficeLocations.Delete(idsToDelete, algoliaSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}