﻿// <auto-generated />
using System;
using HomefinderAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HomefinderAPI.Data.Migrations
{
    [DbContext(typeof(HomefinderContext))]
    partial class HomefinderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("HomefinderAPI.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("HomefinderAPI.Models.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PropertyId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("HomefinderAPI.Models.LeaseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LeaseTypes");
                });

            modelBuilder.Entity("HomefinderAPI.Models.PropertyObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Area")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LeaseTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("LeaseTypeId");

                    b.HasIndex("PropertyTypeId");

                    b.ToTable("PropertyObjects");
                });

            modelBuilder.Entity("HomefinderAPI.Models.PropertyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PropertyTypes");
                });

            modelBuilder.Entity("HomefinderAPI.Models.Advertisement", b =>
                {
                    b.HasOne("HomefinderAPI.Models.PropertyObject", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("HomefinderAPI.Models.PropertyObject", b =>
                {
                    b.HasOne("HomefinderAPI.Models.Address", "Address")
                        .WithMany("PropertyObjects")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomefinderAPI.Models.LeaseType", "LeaseType")
                        .WithMany("PropertyObjects")
                        .HasForeignKey("LeaseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomefinderAPI.Models.PropertyType", "PropertyType")
                        .WithMany("PropertyObjects")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("LeaseType");

                    b.Navigation("PropertyType");
                });

            modelBuilder.Entity("HomefinderAPI.Models.Address", b =>
                {
                    b.Navigation("PropertyObjects");
                });

            modelBuilder.Entity("HomefinderAPI.Models.LeaseType", b =>
                {
                    b.Navigation("PropertyObjects");
                });

            modelBuilder.Entity("HomefinderAPI.Models.PropertyType", b =>
                {
                    b.Navigation("PropertyObjects");
                });
#pragma warning restore 612, 618
        }
    }
}
