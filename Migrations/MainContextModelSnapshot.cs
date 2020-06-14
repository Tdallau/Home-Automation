﻿// <auto-generated />
using System;
using HomeAutomation.Helpers.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomeAutomation.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HomeAutomation.Models.Database.Authorization.App", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Area")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("App");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.Authorization.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.Authorization.UserApp", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Default")
                        .HasColumnType("boolean");

                    b.HasKey("UserId", "AppId");

                    b.HasIndex("AppId");

                    b.ToTable("UserApp");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.Authorization.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid>("AppId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("UserId");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.Authorization.UserApp", b =>
                {
                    b.HasOne("HomeAutomation.Models.Database.Authorization.App", "App")
                        .WithMany()
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeAutomation.Models.Database.Authorization.User", "User")
                        .WithMany("UserApps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.Authorization.UserToken", b =>
                {
                    b.HasOne("HomeAutomation.Models.Database.Authorization.App", "App")
                        .WithMany()
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeAutomation.Models.Database.Authorization.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
