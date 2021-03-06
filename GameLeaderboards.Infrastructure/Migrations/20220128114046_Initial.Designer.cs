// <auto-generated />
using System;
using GameLeaderboards.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameLeaderboards.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220128114046_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Leaderboard", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RaceId");

                    b.HasIndex("CountryId");

                    b.HasIndex("PlatformId");

                    b.HasIndex("RaceId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Leaderboards");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LapCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Leaderboard", b =>
                {
                    b.HasOne("GameLeaderboards.Domain.Models.Country", "Country")
                        .WithMany("Leaderboards")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameLeaderboards.Domain.Models.Platform", "Platform")
                        .WithMany("Leaderboards")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameLeaderboards.Domain.Models.Race", "Race")
                        .WithMany("Leaderboards")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameLeaderboards.Domain.Models.User", "User")
                        .WithMany("Leaderboards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameLeaderboards.Domain.Models.Vehicle", "Vehicle")
                        .WithMany("Leaderboards")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Platform");

                    b.Navigation("Race");

                    b.Navigation("User");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Country", b =>
                {
                    b.Navigation("Leaderboards");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Platform", b =>
                {
                    b.Navigation("Leaderboards");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Race", b =>
                {
                    b.Navigation("Leaderboards");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.User", b =>
                {
                    b.Navigation("Leaderboards");
                });

            modelBuilder.Entity("GameLeaderboards.Domain.Models.Vehicle", b =>
                {
                    b.Navigation("Leaderboards");
                });
#pragma warning restore 612, 618
        }
    }
}
