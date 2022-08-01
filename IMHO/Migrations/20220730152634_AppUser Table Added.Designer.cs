﻿// <auto-generated />
using System;
using IMHO.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IMHO.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220730152634_AppUser Table Added")]
    partial class AppUserTableAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IMHO.Data.AppUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("LastName")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NameIdentifier")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Password")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Provider")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Roles")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Username")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("UserId");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "j@test.com",
                            FirstName = "Junhyeok",
                            LastName = "Jang",
                            Mobile = "111-111-111",
                            Password = "h",
                            Provider = "Cookies",
                            Roles = "Admin",
                            Username = "j"
                        });
                });

            modelBuilder.Entity("IMHO.Models.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}