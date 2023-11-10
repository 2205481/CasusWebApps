﻿// <auto-generated />
using System;
using CasusWebApps.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CasusWebApps.Migrations
{
    [DbContext(typeof(WasteDbContext))]
    [Migration("20231109174355_NoItemType")]
    partial class NoItemType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CasusWebApps.Models.AnnotationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BoundingBoxHeight")
                        .HasColumnType("int")
                        .HasColumnName("BoundingBoxHeight");

                    b.Property<int>("BoundingBoxWidth")
                        .HasColumnType("int")
                        .HasColumnName("BoundingBoxWidth");

                    b.Property<int>("BoundingBoxX")
                        .HasColumnType("int")
                        .HasColumnName("BoundingBoxX");

                    b.Property<int>("BoundingBoxY")
                        .HasColumnType("int")
                        .HasColumnName("BoundingBoxY");

                    b.Property<string>("CanvasImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AnnotationModels");
                });

            modelBuilder.Entity("CasusWebApps.Models.ImageHandler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ImageHandlers");
                });

            modelBuilder.Entity("CasusWebApps.Models.ImageTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ImageHandlerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ObjectType")
                        .HasColumnType("int");

                    b.Property<float>("TypeWindow")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ImageHandlerId");

                    b.ToTable("ImageTag");
                });

            modelBuilder.Entity("CasusWebApps.Models.ImageTag", b =>
                {
                    b.HasOne("CasusWebApps.Models.ImageHandler", "ImageHandler")
                        .WithMany("ImageTags")
                        .HasForeignKey("ImageHandlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImageHandler");
                });

            modelBuilder.Entity("CasusWebApps.Models.ImageHandler", b =>
                {
                    b.Navigation("ImageTags");
                });
#pragma warning restore 612, 618
        }
    }
}
