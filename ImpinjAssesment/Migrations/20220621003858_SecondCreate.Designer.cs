// <auto-generated />
using System;
using ImpinjAssesment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImpinjAssesment.Migrations
{
    [DbContext(typeof(CountryDataContext))]
    [Migration("20220621003858_SecondCreate")]
    partial class SecondCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("ImpinjAssesment.Models.CountryDataUploadFile", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<char>("OrderPriority")
                        .HasColumnType("TEXT");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SalesChannel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ShipDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalProfit")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalRevenue")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("UnitsSold")
                        .HasColumnType("INTEGER");

                    b.HasKey("OrderID");

                    b.ToTable("CountryData");
                });
#pragma warning restore 612, 618
        }
    }
}
