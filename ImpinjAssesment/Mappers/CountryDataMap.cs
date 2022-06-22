using CsvHelper.Configuration;
using ImpinjAssesment.Models;

namespace ImpinjAssesment.Mappers
{
    public sealed class CountryDataMap : ClassMap<CountryDataUploadFile>
    {
        public CountryDataMap()
        {
            Map(c => c.Country).Name("Country");
            Map(r => r.Region).Name("Region");
            Map(it => it.ItemType).Name("Item Type");
            Map(sc => sc.SalesChannel).Name("Sales Channel");
            Map(op => op.OrderPriority).Name("Order Priority");
            Map(od => od.OrderDate).Name("Order Date");
            Map(sd => sd.ShipDate).Name("Ship Date");
            Map(id => id.OrderID).Name("Order ID");
            Map(us => us.UnitsSold).Name("Units Sold");
            Map(up => up.UnitPrice).Name("Unit Price");
            Map(uc => uc.UnitCost).Name("Unit Cost");
            Map(tr => tr.TotalRevenue).Name("Total Revenue");
            Map(tc => tc.TotalCost).Name("Total Cost");
            Map(tp => tp.TotalProfit).Name("Total Profit");
        }
    }
}
