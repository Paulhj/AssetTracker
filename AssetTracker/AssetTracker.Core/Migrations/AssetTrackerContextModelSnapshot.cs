﻿// <auto-generated />
using System;
using AssetTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssetTracker.Core.Migrations
{
    [DbContext(typeof(AssetTrackerContext))]
    partial class AssetTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssetTracker.Core.Entities.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDt");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Photo");

                    b.Property<int>("StatusId");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("TypeId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.AssetLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssetId");

                    b.Property<DateTime>("CreateDt");

                    b.Property<int>("LocationId");

                    b.Property<string>("Note");

                    b.Property<DateTime>("TransferDt");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("LocationId");

                    b.ToTable("AssetLocation");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.AssetOrganization", b =>
                {
                    b.Property<int>("AssetId");

                    b.Property<int>("OrganizationId");

                    b.HasKey("AssetId", "OrganizationId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AssetOrganization");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.OrganizationUser", b =>
                {
                    b.Property<int>("OrganizationId");

                    b.Property<int>("UserId");

                    b.HasKey("OrganizationId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("OrganizationUser");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("NmFirst");

                    b.Property<string>("NmLast");

                    b.Property<int>("SelectedOrganizationId");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.Asset", b =>
                {
                    b.HasOne("AssetTracker.Core.Entities.Status", "Status")
                        .WithMany("Assets")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AssetTracker.Core.Entities.Type", "Type")
                        .WithMany("Assets")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.AssetLocation", b =>
                {
                    b.HasOne("AssetTracker.Core.Entities.Asset", "Asset")
                        .WithMany("AssetLocations")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AssetTracker.Core.Entities.Location", "Location")
                        .WithMany("AssetLocations")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.AssetOrganization", b =>
                {
                    b.HasOne("AssetTracker.Core.Entities.Asset", "Asset")
                        .WithMany("AssetOrganizations")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AssetTracker.Core.Entities.Organization", "Organization")
                        .WithMany("AssetOrganizations")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.Location", b =>
                {
                    b.HasOne("AssetTracker.Core.Entities.Organization", "Organization")
                        .WithMany("Locations")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.OrganizationUser", b =>
                {
                    b.HasOne("AssetTracker.Core.Entities.Organization", "Organization")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AssetTracker.Core.Entities.User", "User")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.Status", b =>
                {
                    b.HasOne("AssetTracker.Core.Entities.Organization", "Organization")
                        .WithMany("Statuses")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AssetTracker.Core.Entities.Type", b =>
                {
                    b.HasOne("AssetTracker.Core.Entities.Organization", "Organization")
                        .WithMany("Types")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
