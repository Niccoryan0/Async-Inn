﻿// <auto-generated />
using Async_Inn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Async_Inn.Migrations
{
    [DbContext(typeof(AsyncInnDbContext))]
    [Migration("20200721190529_addedHotelAndRoomSeededData")]
    partial class addedHotelAndRoomSeededData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Async_Inn.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Seattle",
                            Name = "Dummy Hotel",
                            Phone = "3606655432",
                            State = "Wa",
                            StreetAddress = "123 Dummy St."
                        },
                        new
                        {
                            Id = 2,
                            City = "Redmond",
                            Name = "Dummy Hotel v2",
                            Phone = "3606332332",
                            State = "Wash",
                            StreetAddress = "123456 Dummy St."
                        },
                        new
                        {
                            Id = 3,
                            City = "Everett",
                            Name = "Dummy Hotel v3",
                            Phone = "36066553231222",
                            State = "Washington",
                            StreetAddress = "123456789 Dummy St."
                        });
                });

            modelBuilder.Entity("Async_Inn.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FloorPlan")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FloorPlan = 0,
                            Name = "Dummy Room"
                        },
                        new
                        {
                            Id = 2,
                            FloorPlan = 2,
                            Name = "Dummy Room v2"
                        },
                        new
                        {
                            Id = 3,
                            FloorPlan = 4,
                            Name = "Dummy Room v3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
