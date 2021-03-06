﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypingCounter.Context.Orm;

namespace TypingCounter.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("TypingCounter.Entities.Archive", b =>
                {
                    b.Property<long>("ArchiveId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Code")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ArchiveId", "Code");

                    b.ToTable("Archive");
                });

            modelBuilder.Entity("TypingCounter.Entities.ArchiveDate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ArchiveDate");
                });

            modelBuilder.Entity("TypingCounter.Entities.Current", b =>
                {
                    b.Property<int>("Code")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("Current");
                });

            modelBuilder.Entity("TypingCounter.Entities.Archive", b =>
                {
                    b.HasOne("TypingCounter.Entities.ArchiveDate", "ArchiveDate")
                        .WithMany("Archives")
                        .HasForeignKey("ArchiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
