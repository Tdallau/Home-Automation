﻿// <auto-generated />
using System;
using HomeAutomation.Helpers.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomeAutomation.Migrations.MyShoppingList
{
    [DbContext(typeof(MyShoppingListContext))]
    [Migration("20200711091705_MovedAmountAndBoughtToShopProduct")]
    partial class MovedAmountAndBoughtToShopProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HomeAutomation.Models.Database.ShoppingList.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.ShoppingList.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ShoppingGroupId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingGroupId");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.ShoppingList.ShopProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Amount")
                        .HasColumnType("text");

                    b.Property<bool>("Bought")
                        .HasColumnType("boolean");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("ShopId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopProduct");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.ShoppingList.ShoppingGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ShoppingGroup");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.ShoppingList.ShoppingGroupUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ShoppingGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingGroupId");

                    b.ToTable("ShoppingGroupUser");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.ShoppingList.Shop", b =>
                {
                    b.HasOne("HomeAutomation.Models.Database.ShoppingList.ShoppingGroup", "ShoppingGroup")
                        .WithMany()
                        .HasForeignKey("ShoppingGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.ShoppingList.ShopProduct", b =>
                {
                    b.HasOne("HomeAutomation.Models.Database.ShoppingList.Product", "Product")
                        .WithMany("ShopProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeAutomation.Models.Database.ShoppingList.Shop", "Shop")
                        .WithMany("ShopProducts")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.ShoppingList.ShoppingGroupUser", b =>
                {
                    b.HasOne("HomeAutomation.Models.Database.ShoppingList.ShoppingGroup", "ShoppingGroup")
                        .WithMany()
                        .HasForeignKey("ShoppingGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}