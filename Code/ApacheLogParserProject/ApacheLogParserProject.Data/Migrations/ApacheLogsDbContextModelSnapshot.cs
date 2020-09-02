﻿// <auto-generated />
using System;
using ApacheLogParserProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApacheLogParserProject.Data.Migrations
{
    [DbContext(typeof(ApacheLogsDbContext))]
    partial class ApacheLogsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApacheLogParserProject.Data.Entities.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientGeolocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientIpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RequestDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestQueryParameters")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestRoute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResponseCode")
                        .HasColumnType("int");

                    b.Property<int?>("ResponseSize")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}