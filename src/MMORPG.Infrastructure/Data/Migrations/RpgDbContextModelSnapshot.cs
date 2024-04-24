﻿// <auto-generated />
using MMORPG.Infrastrutture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(MMORPGDbContext))]
    partial class RpgDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("MMORPG.Domain.Entity.CharacterEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Hp")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsConnected")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LevelValue")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoveDirection")
                        .HasColumnType("INTEGER");

                    b.Property<double>("PositionX")
                        .HasColumnType("REAL");

                    b.Property<double>("PositionY")
                        .HasColumnType("REAL");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("MMORPG.Domain.Entity.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Username = "Andre"
                        },
                        new
                        {
                            Id = 2,
                            Username = "Mazza"
                        },
                        new
                        {
                            Id = 3,
                            Username = "Aba"
                        },
                        new
                        {
                            Id = 4,
                            Username = "Axel"
                        });
                });

            modelBuilder.Entity("MMORPG.Domain.Entity.CharacterEntity", b =>
                {
                    b.HasOne("MMORPG.Domain.Entity.UserEntity", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MMORPG.Domain.Entity.UserEntity", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
