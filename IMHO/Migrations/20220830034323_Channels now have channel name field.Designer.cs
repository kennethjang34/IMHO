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
    [Migration("20220830034323_Channels now have channel name field")]
    partial class Channelsnowhavechannelnamefield
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AccountChannel", b =>
                {
                    b.Property<int>("ChannelsChannelId")
                        .HasColumnType("int");

                    b.Property<int>("MembersUserId")
                        .HasColumnType("int");

                    b.HasKey("ChannelsChannelId", "MembersUserId");

                    b.HasIndex("MembersUserId");

                    b.ToTable("AccountChannel");
                });

            modelBuilder.Entity("IMHO.Models.Account", b =>
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

                    b.Property<string>("RolesString")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("Roles");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("UserId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            UserId = -1,
                            Email = "j@test.com",
                            FirstName = "Junhyeok",
                            LastName = "Jang",
                            Mobile = "111-111-111",
                            Password = "h",
                            Provider = "Cookies",
                            RolesString = "Admin",
                            Username = "j"
                        });
                });

            modelBuilder.Entity("IMHO.Models.Channel", b =>
                {
                    b.Property<int>("ChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessibilityType")
                        .HasColumnType("int");

                    b.Property<string>("ChannelName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ChannelId");

                    b.ToTable("Channels");

                    b.HasData(
                        new
                        {
                            ChannelId = -1,
                            AccessibilityType = 2,
                            ChannelName = "test",
                            Description = "TEST CHANNEL"
                        });
                });

            modelBuilder.Entity("IMHO.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("IMHO.Models.Image", b =>
                {
                    b.Property<int?>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Caption")
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Format")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageName")
                        .HasColumnType("longtext");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("ImageId");

                    b.HasIndex("PostId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            ImageId = 2,
                            Caption = "test image",
                            FileName = "/Users/JANG/IMHO/IMHO/Resources/Images/seol.jpeg",
                            Format = "jpeg",
                            PostId = 2,
                            UploadedAt = new DateTime(2022, 8, 29, 20, 43, 23, 13, DateTimeKind.Local).AddTicks(9620)
                        });
                });

            modelBuilder.Entity("IMHO.Models.Post", b =>
                {
                    b.Property<int?>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ExposedTo")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<bool>("Published")
                        .HasMaxLength(10)
                        .HasColumnType("tinyint(10)");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime?>("UpdatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Views")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ChannelId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("IMHO.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<string>("TagDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TagName")
                        .HasColumnType("longtext");

                    b.HasKey("TagId");

                    b.HasIndex("ChannelId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            TagId = -1,
                            ChannelId = -1,
                            TagDescription = "TEST TAG DESCRIPTION",
                            TagName = "TEST TAG"
                        });
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<int>("PostsPostId")
                        .HasColumnType("int");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("int");

                    b.HasKey("PostsPostId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("AccountChannel", b =>
                {
                    b.HasOne("IMHO.Models.Channel", null)
                        .WithMany()
                        .HasForeignKey("ChannelsChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMHO.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("MembersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMHO.Models.Comment", b =>
                {
                    b.HasOne("IMHO.Models.Account", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMHO.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("IMHO.Models.Image", b =>
                {
                    b.HasOne("IMHO.Models.Post", "Post")
                        .WithMany("Images")
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("IMHO.Models.Post", b =>
                {
                    b.HasOne("IMHO.Models.Account", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMHO.Models.Channel", "Channel")
                        .WithMany("Posts")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("IMHO.Models.Tag", b =>
                {
                    b.HasOne("IMHO.Models.Channel", "Channel")
                        .WithMany("Tags")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("IMHO.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMHO.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMHO.Models.Account", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("IMHO.Models.Channel", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("IMHO.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
