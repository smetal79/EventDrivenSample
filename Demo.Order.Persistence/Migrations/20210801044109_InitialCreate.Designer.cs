﻿// <auto-generated />
using System;
using Demo.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Demo.Order.Persistence.Migrations
{
    [DbContext(typeof(DemoContext))]
    [Migration("20210801044109_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Demo.Order.Domain.Order", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("key");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("discriminator");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric")
                        .HasColumnName("total");

                    b.HasKey("Key")
                        .HasName("pk_orders");

                    b.ToTable("orders");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Order");
                });

            modelBuilder.Entity("Demo.Order.Domain.DraftOrder", b =>
                {
                    b.HasBaseType("Demo.Order.Domain.Order");

                    b.HasDiscriminator().HasValue("DraftOrder");
                });

            modelBuilder.Entity("Demo.Order.Domain.ProcessedOrder", b =>
                {
                    b.HasBaseType("Demo.Order.Domain.Order");

                    b.HasDiscriminator().HasValue("ProcessedOrder");
                });

            modelBuilder.Entity("Demo.Order.Domain.SubmittedOrder", b =>
                {
                    b.HasBaseType("Demo.Order.Domain.Order");

                    b.HasDiscriminator().HasValue("SubmittedOrder");
                });
#pragma warning restore 612, 618
        }
    }
}