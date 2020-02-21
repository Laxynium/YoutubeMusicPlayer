﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore;

namespace YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore.Migrations.Module1
{
    [DbContext(typeof(Module1DbContext))]
    partial class Module1DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore.Entity1", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("module1_entities");
                });
#pragma warning restore 612, 618
        }
    }
}