﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolicyDetails.Models;

#nullable disable

namespace PolicyDetails.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PolicyDetails.Models.PolicyData", b =>
                {
                    b.Property<int>("PolicyKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PolicyKey"));

                    b.Property<int>("ContractNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("ContractStatusCode")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ETLDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MaturityDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextRenewalDue")
                        .HasColumnType("datetime2");

                    b.Property<string>("PolicyStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PremiumAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RiskCommencementDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SumAssuredAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PolicyKey");

                    b.ToTable("PolicyData");
                });
#pragma warning restore 612, 618
        }
    }
}
