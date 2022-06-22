using ImpinjAssesment.Models;

namespace ImpinjAssesment.Services
{
    public interface ICountryDataRepository
    {
        Task<IEnumerable<CountryDataUploadFile>> GetData();

        Task Insert(CountryDataUploadFile file);

        Task BulkUpload(List<CountryDataUploadFile> records);

        Task<double> GetUnitCostMedian();

        Task<string> GetMostCommonRegion();

        Task<List<string>> GetMaxMinOrderDate();

        Task<double> GetTotalRevenue();
    }
}
