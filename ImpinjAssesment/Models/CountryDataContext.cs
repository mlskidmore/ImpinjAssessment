using Microsoft.EntityFrameworkCore;

namespace ImpinjAssesment.Models
{
    public class CountryDataContext : DbContext
    {
        public CountryDataContext(DbContextOptions<CountryDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite(System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ConnectionString);

        public DbSet<CountryDataUploadFile> CountryData { get; set; }
    }
}
