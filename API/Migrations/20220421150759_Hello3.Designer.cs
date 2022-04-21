﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220421150759_Hello3")]
    partial class Hello3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("API.Model.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hall_ID")
                        .HasColumnType("int");

                    b.Property<string>("User_ID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Hall_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("API.Model.Hall", b =>
                {
                    b.Property<int>("HallID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HallID"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hall_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HallID");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("API.Model.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int>("HallID")
                        .HasColumnType("int");

                    b.Property<string>("ReviewContent")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("API.Model.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Model.Event", b =>
                {
                    b.HasOne("API.Model.Hall", "Halls")
                        .WithMany("HallEvents")
                        .HasForeignKey("Hall_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Model.User", "Users")
                        .WithMany("UserEvents")
                        .HasForeignKey("User_ID");

                    b.Navigation("Halls");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("API.Model.Hall", b =>
                {
                    b.Navigation("HallEvents");
                });

            modelBuilder.Entity("API.Model.User", b =>
                {
                    b.Navigation("UserEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
