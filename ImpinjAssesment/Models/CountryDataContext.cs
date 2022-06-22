using Microsoft.EntityFrameworkCore;

namespace ImpinjAssesment.Models
{
    public class CountryDataContext : DbContext
    {
        public CountryDataContext(DbContextOptions<CountryDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source=C:\Users\rskid\Downloads\ImpinjAssessment\ImpinjAssesment\Database\CountryData.db");

        public DbSet<CountryDataUploadFile> CountryData { get; set; }
    }
}
