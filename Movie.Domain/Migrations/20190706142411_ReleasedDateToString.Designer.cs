﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movie.Domain;

namespace Movie.Domain.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20190706142411_ReleasedDateToString")]
    partial class ReleasedDateToString
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Movie.Domain.Model.Movie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("ImdbRating")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Released")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<short>("Year");

                    b.HasKey("Id");

                    b.ToTable("Movie","Movie");
                });
#pragma warning restore 612, 618
        }
    }
}
