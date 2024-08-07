﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WaterQualityMonitorApi.Database;

#nullable disable

namespace WaterQualityMonitorApi.Migrations
{
    [DbContext(typeof(WaterMonitorDbContext))]
    [Migration("20240720014130_CreateAccessPermissions")]
    partial class CreateAccessPermissions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("PermissionUser", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("PermissionsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("PermissionUser");
                });

            modelBuilder.Entity("WaterQualityMonitorApi.Database.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Guid")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("WaterQualityMonitorApi.Database.Models.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MeasuredTime")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("WaterQualityMonitorApi.Database.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("WaterQualityMonitorApi.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ActiveUser")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PermissionUser", b =>
                {
                    b.HasOne("WaterQualityMonitorApi.Database.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WaterQualityMonitorApi.Database.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WaterQualityMonitorApi.Database.Models.Device", b =>
                {
                    b.HasOne("WaterQualityMonitorApi.Database.Models.User", "Owner")
                        .WithMany("Devices")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WaterQualityMonitorApi.Database.Models.Measurement", b =>
                {
                    b.HasOne("WaterQualityMonitorApi.Database.Models.Device", "Device")
                        .WithMany("Measurements")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("WaterQualityMonitorApi.Database.Models.Device", b =>
                {
                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("WaterQualityMonitorApi.Database.Models.User", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
