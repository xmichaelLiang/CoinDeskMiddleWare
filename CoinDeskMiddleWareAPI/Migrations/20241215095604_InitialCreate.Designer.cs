﻿// <auto-generated />
using System;
using CurrencyDBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoinDeskMiddleWareAPI.Migrations
{
    [DbContext(typeof(CurrencyDbContext))]
    [Migration("20241215095604_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CurrencyDBContext.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurrencyId"));

                    b.Property<string>("CreateID")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CurrencyId");

                    b.HasIndex("CurrencyCode")
                        .IsUnique()
                        .HasDatabaseName("UQ_CurrencyCode");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("IDX_CurrencyId");

                    b.ToTable("Currency", (string)null);
                });

            modelBuilder.Entity("CurrencyDBContext.Models.CurrencyChgLog", b =>
                {
                    b.Property<string>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("ModifyDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("ModifyUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NewData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LogID");

                    b.ToTable("CurrencyChgLog", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}