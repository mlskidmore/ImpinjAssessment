using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImpinjAssesment.Models
{
    public class CountryDataUploadFile
    {
        [Index(1)]
        //[Name("Country")]
        public string Country { get; set; }

        [Index(0)]
        //[Name("Region")]
        public string Region { get; set; }

        [Index(2)]
        //[Name("Item Type")]
        public string ItemType { get; set; }
        
        [Index(3)]
        //[Name("Sales Channel")]
        public string SalesChannel { get; set; }

        [Index(4)]
        //[Name("Order Priority")]
        public char OrderPriority { get; set; }

        [Index(5)]
        //[Name("Order Date")]
        public DateTime OrderDate { get; set; }

        [Index(7)]
        //[Name("Ship Date")]
        public DateTime ShipDate { get; set; }

        [Index(6)]
        [Key]
        //[Name("Order ID")]
        public int OrderID { get; set; }

        [Index(8)]
        //[Name("Units Sold")]
        public int UnitsSold { get; set; }

        [Index(9)]
        //[Name("Unit Price")]
        public double UnitPrice { get; set; }

        [Index(10)]
        //[Name("Unit Cost")]
        public double UnitCost { get; set; }

        [Index(11)]
        //[Name("Total Revenue")]
        public double TotalRevenue { get; set; }

        [Index(12)]
        //[Name("Total Cost")]
        public double TotalCost { get; set; }

        [Index(13)]
        //[Name("Total Profit")]
        public double TotalProfit { get; set; }
    }
}
