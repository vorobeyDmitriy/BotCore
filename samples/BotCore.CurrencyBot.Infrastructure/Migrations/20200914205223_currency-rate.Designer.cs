﻿// <auto-generated />
using System;
using BotCore.CurrencyBot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BotCore.CurrencyBot.Infrastructure.Migrations
{
    [DbContext(typeof(BotCoreTestContext))]
    [Migration("20200914205223_currency-rate")]
    partial class currencyrate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BotCore.CurrencyBot.Core.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.Property<int>("Scale")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("BotCore.CurrencyBot.Core.Entities.CurrencyRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FromId")
                        .HasColumnType("integer");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.Property<int>("ToId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("CurrencyRates");
                });

            modelBuilder.Entity("BotCore.CurrencyBot.Core.Entities.CurrencyRate", b =>
                {
                    b.HasOne("BotCore.CurrencyBot.Core.Entities.Currency", "From")
                        .WithMany("FromCurrencyRates")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotCore.CurrencyBot.Core.Entities.Currency", "To")
                        .WithMany("ToCurrencyRates")
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}