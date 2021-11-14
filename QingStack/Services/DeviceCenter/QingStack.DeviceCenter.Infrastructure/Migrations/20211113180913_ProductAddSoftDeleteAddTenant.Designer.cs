﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QingStack.DeviceCenter.Infrastructure.EntityFrameworks;

namespace QingStack.DeviceCenter.Infrastructure.Migrations
{
    [DbContext(typeof(DeviceCenterDbContext))]
    [Migration("20211113180913_ProductAddSoftDeleteAddTenant")]
    partial class ProductAddSoftDeleteAddTenant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.PermissionAggregate.PermissionGrant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ProviderName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "ProviderName", "ProviderKey");

                    b.ToTable("PermissionGrants");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Coordinate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("Online")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<string>("SerialNo")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.DeviceTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceTags");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate.ProjectGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("Sorting")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectGroups");
                });

           

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.Device", b =>
                {
                    b.HasOne("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.Product", "Product")
                        .WithMany("Devices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.DeviceAddress", "Address", b1 =>
                        {
                            b1.Property<int>("DeviceId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("DeviceId");

                            b1.ToTable("DeviceAddresses");

                            b1.WithOwner()
                                .HasForeignKey("DeviceId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.DeviceTag", b =>
                {
                    b.HasOne("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.Device", null)
                        .WithMany("Tags")
                        .HasForeignKey("DeviceId");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate.ProjectGroup", b =>
                {
                    b.HasOne("QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate.ProjectGroup", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate.Project", "Project")
                        .WithMany("Groups")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Project");
                });

          

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.Device", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate.Product", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate.Project", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate.ProjectGroup", b =>
                {
                    b.Navigation("Children");
                });

          
#pragma warning restore 612, 618
        }
    }
}
