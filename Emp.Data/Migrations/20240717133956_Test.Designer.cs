﻿// <auto-generated />
using System;
using Emp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Emp.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240717133956_Test")]
    partial class Test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Emp.Entity.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfEntry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<int>("RoleOfEmp")
                        .HasColumnType("integer");

                    b.Property<string>("Salary")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("05625c09-4c13-42ca-a0d7-ab2d3465a65b"),
                            DateOfEntry = new DateTime(2024, 7, 17, 13, 39, 56, 384, DateTimeKind.Utc).AddTicks(9480),
                            Department = "Back End Development",
                            Email = "yusufakyus47@gmail.com",
                            IsDeleted = false,
                            LastName = "Akyüz",
                            Name = "Yusuf",
                            PhoneNumber = "05415125099",
                            RoleOfEmp = 1,
                            Salary = "40000"
                        },
                        new
                        {
                            Id = new Guid("00cefd26-89cb-4c1c-9fce-ddf1ff5a727c"),
                            DateOfEntry = new DateTime(2024, 7, 17, 13, 39, 56, 384, DateTimeKind.Utc).AddTicks(9480),
                            Department = "Back End Development",
                            Email = "yusufakyus47@gmail.com",
                            IsDeleted = false,
                            LastName = "Akyüz",
                            Name = "Yusuf",
                            PhoneNumber = "05415125099",
                            RoleOfEmp = 1,
                            Salary = "40000"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
