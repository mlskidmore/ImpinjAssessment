using ImpinjAssesment.Controllers;
using ImpinjAssesment.Services;
using Moq;
using Xunit;

namespace ImpinjAssessment.Tests
{
    
    public class CountryDataRepositoryTests
    {
        private readonly CountryDataRepository _countryDataRepository;
        private readonly Mock<ICountryDataRepository> _countryDataMock = new Mock<ICountryDataRepository>();
                
        //[TestMethod]
        [Fact]
        public async Task GetUnitCostMedian_Returns_Correct_Median()
        {
            // Arrange
            
            // Act

            // Assert
        }
    }
}