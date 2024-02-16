﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Migration.Reproducer.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Migration.Reproducer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("test_schema")
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "status_type", new[] { "active", "inactive" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Migration.Reproducer.Models.MyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<StatusType>("status")
                        .HasColumnType("status_type");

                    b.Property<StatusType>("status_type")
                        .HasColumnType("status_type");

                    b.HasKey("Id");

                    b.ToTable("MyEntities", "test_schema");

                    b.HasDiscriminator<StatusType>("status_type");
                });

            modelBuilder.Entity("Migration.Reproducer.Models.ActiveEntity", b =>
                {
                    b.HasBaseType("Migration.Reproducer.Models.MyEntity");

                    b.HasDiscriminator().HasValue(StatusType.Active);
                });

            modelBuilder.Entity("Migration.Reproducer.Models.InactiveEntity", b =>
                {
                    b.HasBaseType("Migration.Reproducer.Models.MyEntity");

                    b.HasDiscriminator().HasValue(StatusType.Inactive);
                });
#pragma warning restore 612, 618
        }
    }
}
