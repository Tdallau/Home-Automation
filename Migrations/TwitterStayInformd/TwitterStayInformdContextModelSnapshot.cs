﻿// <auto-generated />
using System;
using HomeAutomation.Helpers.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomeAutomation.Migrations.TwitterStayInformd
{
    [DbContext(typeof(TwitterStayInformdContext))]
    partial class TwitterStayInformdContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HomeAutomation.Models.Database.TwitterStayInformd.Tweet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tweet");
                });

            modelBuilder.Entity("HomeAutomation.Models.Database.TwitterStayInformd.TwitterTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BodId")
                        .HasColumnType("text");

                    b.Property<string>("ChatId")
                        .HasColumnType("text");

                    b.Property<string>("CronTime")
                        .HasColumnType("text");

                    b.Property<int>("NumberOfTweets")
                        .HasColumnType("integer");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TwitterTask");
                });
#pragma warning restore 612, 618
        }
    }
}