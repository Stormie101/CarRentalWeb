﻿// <auto-generated />
using System;
using CarRentalFrontEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRentalFrontEnd.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CarRentalFrontEnd.Models.Booked", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("CarImage")
                        .HasColumnType("longtext");

                    b.Property<string>("CarName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DropoffDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DropoffLocation")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PickupDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PickupLocation")
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Booked");
                });

            modelBuilder.Entity("CarRentalFrontEnd.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarRentalFrontEnd.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AdditionalFeedback")
                        .HasColumnType("longtext");

                    b.Property<string>("CarCondition")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerService")
                        .HasColumnType("longtext");

                    b.Property<string>("DropoffProcess")
                        .HasColumnType("longtext");

                    b.Property<string>("OverallSatisfaction")
                        .HasColumnType("longtext");

                    b.Property<string>("PickupProcess")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("CarRentalFrontEnd.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DropoffDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DropoffLocation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PickupDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PickupLocation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RentalDays")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPayment")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("CarRentalFrontEnd.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarRentalFrontEnd.Models.Rental", b =>
                {
                    b.HasOne("CarRentalFrontEnd.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });
#pragma warning restore 612, 618
        }
    }
}
