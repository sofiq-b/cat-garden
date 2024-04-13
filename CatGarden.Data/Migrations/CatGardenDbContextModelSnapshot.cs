﻿// <auto-generated />
using System;
using CatGarden.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CatGarden.Data.Migrations
{
    [DbContext(typeof(CatGardenDbContext))]
    partial class CatGardenDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CatGarden.Data.Models.AdoptionApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ApplicationStatus")
                        .HasColumnType("int");

                    b.Property<int>("CatId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CatId");

                    b.HasIndex("UserId");

                    b.ToTable("AdoptionApplications");
                });

            modelBuilder.Entity("CatGarden.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CatGarden.Data.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("CatGarden.Data.Models.Cat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasMaxLength(30)
                        .HasColumnType("int");

                    b.Property<int>("AvailabilityStatus")
                        .HasColumnType("int");

                    b.Property<int>("Breed")
                        .HasColumnType("int");

                    b.Property<int>("CatteryId")
                        .HasColumnType("int");

                    b.Property<int>("CoatLength")
                        .HasColumnType("int");

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CatteryId");

                    b.HasIndex("UserId");

                    b.ToTable("Cats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 2,
                            AvailabilityStatus = 0,
                            Breed = 41,
                            CatteryId = 1,
                            CoatLength = 2,
                            Color = 3,
                            DateAdded = new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "White furball, a picture of serenity, absolutely loves lounging around.",
                            Gender = 0,
                            LikesCount = 0,
                            Name = "Jimmy"
                        },
                        new
                        {
                            Id = 2,
                            Age = 2,
                            AvailabilityStatus = 0,
                            Breed = 10,
                            CatteryId = 1,
                            CoatLength = 1,
                            Color = 6,
                            DateAdded = new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Playful and very energetic, knows how to do a handshake!",
                            Gender = 0,
                            LikesCount = 0,
                            Name = "Nagi"
                        });
                });

            modelBuilder.Entity("CatGarden.Data.Models.Cattery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<DateTime>("EstablishmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Catteries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "5, Inzh. Georgi Belov",
                            City = 19,
                            EstablishmentDate = new DateTime(2012, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Whisker Haven",
                            OwnerId = new Guid("b1bfe4d3-a412-4ffe-b066-fc04238e432b")
                        },
                        new
                        {
                            Id = 2,
                            Address = "29, Sevastokrator Kaloyan",
                            City = 23,
                            EstablishmentDate = new DateTime(2006, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Purrfect Paws",
                            OwnerId = new Guid("b1bfe4d3-a412-4ffe-b066-fc04238e432b")
                        });
                });

            modelBuilder.Entity("CatGarden.Data.Models.CatteryOwner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CatteryOwners");
                });

            modelBuilder.Entity("CatGarden.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CatId")
                        .HasColumnType("int");

                    b.Property<int?>("CatteryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCover")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CatId");

                    b.HasIndex("CatteryId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CatId = 1,
                            IsCover = false,
                            Name = "jimmy_image2.jpg",
                            URL = "/cats/gallery/jimmy_image2.jpg"
                        },
                        new
                        {
                            Id = 2,
                            CatId = 1,
                            IsCover = false,
                            Name = "jimmy_image3.jpg",
                            URL = "/cats/gallery/jimmy_image3.jpg"
                        },
                        new
                        {
                            Id = 3,
                            CatId = 2,
                            IsCover = false,
                            Name = "nagi_image2.jpg",
                            URL = "/cats/gallery/nagi_image2.jpg"
                        },
                        new
                        {
                            Id = 4,
                            CatId = 2,
                            IsCover = false,
                            Name = "nagi_image3.jpg",
                            URL = "/cats/gallery/nagi_image3.jpg"
                        },
                        new
                        {
                            Id = 5,
                            CatId = 1,
                            IsCover = true,
                            Name = "jimmy_cover.jpg",
                            URL = "/cats/cover/jimmy_cover.jpg"
                        },
                        new
                        {
                            Id = 6,
                            CatId = 2,
                            IsCover = true,
                            Name = "nagi_cover.jpg",
                            URL = "/cats/cover/nagi_cover.jpg"
                        },
                        new
                        {
                            Id = 7,
                            CatteryId = 2,
                            IsCover = true,
                            Name = "simone-nolgo-WMeQtoH-a3w-unsplash.jpg",
                            URL = "/catteris/cover/simone-nolgo-WMeQtoH-a3w-unsplash.jpg"
                        },
                        new
                        {
                            Id = 8,
                            CatteryId = 1,
                            IsCover = true,
                            Name = "ries-bosch-sj16pUqOoco-unsplash.jpg",
                            URL = "/catteries/cover/ries-bosch-sj16pUqOoco-unsplash.jpg"
                        });
                });

            modelBuilder.Entity("CatGarden.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CatteryId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CatteryId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CatGarden.Data.Models.UserFavCat", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CatId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "CatId");

                    b.HasIndex("CatId");

                    b.ToTable("UsersFavCats");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CatGarden.Data.Models.AdoptionApplication", b =>
                {
                    b.HasOne("CatGarden.Data.Models.Cat", "Cat")
                        .WithMany("AdoptionApplications")
                        .HasForeignKey("CatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatGarden.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatGarden.Data.Models.Cat", b =>
                {
                    b.HasOne("CatGarden.Data.Models.Cattery", "Cattery")
                        .WithMany("Cats")
                        .HasForeignKey("CatteryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatGarden.Data.Models.ApplicationUser", "User")
                        .WithMany("AdoptedCats")
                        .HasForeignKey("UserId");

                    b.Navigation("Cattery");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatGarden.Data.Models.Cattery", b =>
                {
                    b.HasOne("CatGarden.Data.Models.CatteryOwner", "Owner")
                        .WithMany("Catteries")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CatGarden.Data.Models.CatteryOwner", b =>
                {
                    b.HasOne("CatGarden.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatGarden.Data.Models.Image", b =>
                {
                    b.HasOne("CatGarden.Data.Models.Cat", "Cat")
                        .WithMany("Images")
                        .HasForeignKey("CatId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CatGarden.Data.Models.Cattery", "Cattery")
                        .WithMany("Images")
                        .HasForeignKey("CatteryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Cat");

                    b.Navigation("Cattery");
                });

            modelBuilder.Entity("CatGarden.Data.Models.Review", b =>
                {
                    b.HasOne("CatGarden.Data.Models.Cattery", "Cattery")
                        .WithMany("Reviews")
                        .HasForeignKey("CatteryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatGarden.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cattery");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatGarden.Data.Models.UserFavCat", b =>
                {
                    b.HasOne("CatGarden.Data.Models.Cat", "Cat")
                        .WithMany("UserFavCats")
                        .HasForeignKey("CatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CatGarden.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CatGarden.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CatGarden.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatGarden.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CatGarden.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CatGarden.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("AdoptedCats");
                });

            modelBuilder.Entity("CatGarden.Data.Models.Cat", b =>
                {
                    b.Navigation("AdoptionApplications");

                    b.Navigation("Images");

                    b.Navigation("UserFavCats");
                });

            modelBuilder.Entity("CatGarden.Data.Models.Cattery", b =>
                {
                    b.Navigation("Cats");

                    b.Navigation("Images");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("CatGarden.Data.Models.CatteryOwner", b =>
                {
                    b.Navigation("Catteries");
                });
#pragma warning restore 612, 618
        }
    }
}
