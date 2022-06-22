using ImpinjAssesment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace ImpinjAssesment.Services
{
    public class CountryDataRepository : ICountryDataRepository
    {
        private readonly CountryDataContext _countryDataContext;
        private readonly string connection = System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        public CountryDataRepository()
        {
        }

        public CountryDataRepository(CountryDataContext context)
        {
            _countryDataContext = context;
        }
        public async Task<IEnumerable<CountryDataUploadFile>> GetData()
        {
            return await _countryDataContext.CountryData.ToListAsync();
        }

        public async Task Insert(CountryDataUploadFile record)
        {
            _countryDataContext.CountryData.Add(record);
            await _countryDataContext.SaveChangesAsync();

            return;
        }

        public async Task BulkUpload(List<CountryDataUploadFile> records)
        {
            using (var conn = new SqliteConnection(connection))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    var cmd = conn.CreateCommand();

                    try
                    {
                        foreach (var record in records)
                        {
                            cmd.CommandText = "INSERT INTO CountryData VALUES (" + record.OrderID + ", \"" +
                                                                                   record.Country + "\", '" +
                                                                                   record.Region + "', '" +
                                                                                   record.ItemType + "', '" +
                                                                                   record.SalesChannel + "', '" +
                                                                                   record.OrderPriority + "', '" +
                                                                                   record.OrderDate.ToShortDateString() + "', '" +
                                                                                   record.ShipDate.ToShortDateString() + "', " +
                                                                                   record.UnitsSold + ", " +
                                                                                   record.UnitPrice + ", " +
                                                                                   record.UnitCost + ", " +
                                                                                   record.TotalRevenue + ", " +
                                                                                   record.TotalCost + ", " +
                                                                                   record.TotalProfit + ")";
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Database Insert Error occurred: " + ex.Message);
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<string> GetMostCommonRegion()
        {
            string MostCommonRegion = String.Empty;

            using (var conn = new SqliteConnection(connection))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    var cmd = conn.CreateCommand();

                    try
                    {
                        cmd.CommandText = "SELECT Region, COUNT(Region) AS Count " +
                                          "FROM CountryData " +
                                          "GROUP BY Region " +
                                          "ORDER BY Count DESC " +
                                          "LIMIT 1";
                        cmd.CommandType = System.Data.CommandType.Text;

                        MostCommonRegion = (string)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Database Error occurred: " + ex.Message);
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return MostCommonRegion;
        }

        public async Task<List<string>> GetMaxMinOrderDate()
        {
            List<string> MaxMinOrderDates = new List<string>();

            using (var conn = new SqliteConnection(connection))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT MIN(OrderDate) AS OrderDates " +
                                      "FROM CountryData " +
                                      "UNION " +
                                      "SELECT MAX(OrderDate) " +
                                      "FROM CountryData ";

                    cmd.CommandType = System.Data.CommandType.Text;

                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        MaxMinOrderDates.Add(Convert.ToString(rdr["OrderDates"]));
                    }
                }

                conn.Close();
            }
            
            string duration = ComputeOrderDateDuration(MaxMinOrderDates);
            MaxMinOrderDates.Add(duration);

            return MaxMinOrderDates;
        }

        public async Task<double> GetTotalRevenue()
        {
            double totalRevenue = 0;

            using (var conn = new SqliteConnection(connection))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT SUM(TotalRevenue) FROM CountryData ";
                    cmd.CommandType = System.Data.CommandType.Text;

                    totalRevenue = (double)cmd.ExecuteScalar();
                }

                conn.Close();
            }

            return totalRevenue;
        }

        public async Task<double> GetUnitCostMedian()
        {
            double unitCostMedian = 0;

            using (var conn = new SqliteConnection(connection))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT AVG(UnitCost) " +
                                      "FROM " +
                                      "(SELECT UnitCost " +
                                      "FROM CountryData " +
                                      "ORDER BY UnitCost " +
                                      "LIMIT 2 - (SELECT COUNT(*) " +
                                      "FROM CountryData) % 2 " +
                                      "OFFSET(SELECT(COUNT(*) - 1) / 2 " +
                                      "FROM CountryData))";
                    cmd.CommandType = System.Data.CommandType.Text;

                    unitCostMedian = (double)cmd.ExecuteScalar();
                }

                conn.Close();
            }

            return unitCostMedian;
        }

        private string ComputeOrderDateDuration(List<string> MaxMinOrderDates)
        {
            string minOrderDate = MaxMinOrderDates[0];
            string maxOrderDate = MaxMinOrderDates[1];

            DateTime minDate = Convert.ToDateTime(minOrderDate, System.Globalization.CultureInfo.InvariantCulture);
            DateTime maxDate = Convert.ToDateTime(maxOrderDate, System.Globalization.CultureInfo.InvariantCulture);

            var duration = maxDate.Subtract(minDate);

            return duration.Days.ToString();
        }
    }
}
