﻿// <auto-generated />
using ClueBot.Resources.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClueBot.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    [Migration("20190112140932_Migration")]
    partial class Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("ClueBot.Resources.Database.Experience", b =>
                {
                    b.Property<ulong>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.HasKey("UserId");

                    b.ToTable("ExperiencePoints");
                });
#pragma warning restore 612, 618
        }
    }
}
