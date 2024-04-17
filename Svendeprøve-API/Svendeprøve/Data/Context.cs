using EmployeeAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(
                @"Server=CPH00301;Database=EmployeeAPI;Integrated Security=True;TrustServerCertificate=True");
            //dbContextOptionsBuilder.UseSqlServer(
            //    @"Server=10.22.24.204;Database=TeamFinder_Europe;User Id=tmfndr;Password=Flodhest13;TrustServerCertificate=True");
        }

        public DbSet<Employee> Employees { get; set; }
    }
}