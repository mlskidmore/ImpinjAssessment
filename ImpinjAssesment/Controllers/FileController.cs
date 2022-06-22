using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using ImpinjAssesment.Mappers;
using ImpinjAssesment.Models;
using ImpinjAssesment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ImpinjAssesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ICountryDataRepository _countryDataRepository;

        public FileController(ICountryDataRepository countryDataRepository)
        {
            _countryDataRepository = countryDataRepository;
        }

        public IActionResult GetTest()
        {
            return Ok("File Upload API running ...");
        }

        [HttpGet]
        [Route("UnitCostMedian")]
        public async Task<double> GetUnitCostMedian()
        {
            return await _countryDataRepository.GetUnitCostMedian();
        }

        [HttpGet]
        [Route("CommonRegion")]
        public async Task<string> GetMostCommonRegion()
        {
            return await _countryDataRepository.GetMostCommonRegion();
        }

        [HttpGet]
        [Route("MaxMinOrderDate")]
        public async Task<List<string>> GetMaxMinOrderDate()
        {
            List<string> OrderDates = new List<string>();

            OrderDates = await _countryDataRepository.GetMaxMinOrderDate();

            return OrderDates;
        }

        [HttpGet]
        [Route("TotalRevenue")]
        public async Task<double> GetTotalRevenue()
        {
            double TotalRevenue = 0;

            TotalRevenue = await _countryDataRepository.GetTotalRevenue();

            return TotalRevenue;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            List<CountryDataUploadFile> records = new List<CountryDataUploadFile>();

            if (file != null)
            {
                if (!file.FileName.Contains(".csv"))
                {
                    throw new Exception("File is not valid CSV");
                }
                else
                {
                    using var memoryStream = new MemoryStream(new byte[file.Length]);
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var reader = new StreamReader(memoryStream))
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<CountryDataMap>();
                        csvReader.Read();
                        csvReader.ReadHeader();
                        records = csvReader.GetRecords<CountryDataUploadFile>().ToList();
                    }
                }

                /*foreach(var record in records)
                {
                    await _countryDataRepository.Insert(record);
                }*/

                await _countryDataRepository.BulkUpload(records);
            }
            else
            {
                throw new Exception("File can not be empty");
            }

            return Ok("ok");
        }        
    }
}
