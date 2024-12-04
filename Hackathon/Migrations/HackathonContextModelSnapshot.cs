﻿// <auto-generated />
using Hackathon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hackathon.Migrations
{
    [DbContext(typeof(HackathonContext))]
    partial class HackathonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Hackathon.CompitionDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Hackathons");
                });

            modelBuilder.Entity("Hackathon.EmployeeDto", b =>
                {
                    b.Property<int>("EmployeePk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmployeePk"));

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EmployeePk");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Hackathon.TeamDto", b =>
                {
                    b.Property<int>("TeamPk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TeamPk"));

                    b.Property<int>("HackathonId")
                        .HasColumnType("integer");

                    b.Property<int>("JuniorEmployeePk")
                        .HasColumnType("integer");

                    b.Property<int>("TeamLeadEmployeePk")
                        .HasColumnType("integer");

                    b.HasKey("TeamPk");

                    b.HasIndex("HackathonId");

                    b.HasIndex("JuniorEmployeePk");

                    b.HasIndex("TeamLeadEmployeePk");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Hackathon.WishlistDto", b =>
                {
                    b.Property<int>("WishlistPk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WishlistPk"));

                    b.PrimitiveCollection<int[]>("DesiredEmployees")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<int>("EmployeePk")
                        .HasColumnType("integer");

                    b.Property<int>("HackathonId")
                        .HasColumnType("integer");

                    b.HasKey("WishlistPk");

                    b.HasIndex("EmployeePk");

                    b.HasIndex("HackathonId");

                    b.ToTable("Wishlists");
                });

            modelBuilder.Entity("Hackathon.TeamDto", b =>
                {
                    b.HasOne("Hackathon.CompitionDto", "Hackathon")
                        .WithMany("Teams")
                        .HasForeignKey("HackathonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hackathon.EmployeeDto", "Junior")
                        .WithMany()
                        .HasForeignKey("JuniorEmployeePk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hackathon.EmployeeDto", "TeamLead")
                        .WithMany()
                        .HasForeignKey("TeamLeadEmployeePk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hackathon");

                    b.Navigation("Junior");

                    b.Navigation("TeamLead");
                });

            modelBuilder.Entity("Hackathon.WishlistDto", b =>
                {
                    b.HasOne("Hackathon.EmployeeDto", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeePk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hackathon.CompitionDto", "Hackathon")
                        .WithMany("Wishlists")
                        .HasForeignKey("HackathonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Hackathon");
                });

            modelBuilder.Entity("Hackathon.CompitionDto", b =>
                {
                    b.Navigation("Teams");

                    b.Navigation("Wishlists");
                });
#pragma warning restore 612, 618
        }
    }
}
