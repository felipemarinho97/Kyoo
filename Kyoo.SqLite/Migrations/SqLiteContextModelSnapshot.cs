﻿// <auto-generated />
using System;
using Kyoo.SqLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kyoo.SqLite.Migrations
{
    [DbContext(typeof(SqLiteContext))]
    partial class SqLiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("CollectionMetadataID", b =>
                {
                    b.Property<int>("ResourceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProviderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.HasKey("ResourceID", "ProviderID");

                    b.HasIndex("ProviderID");

                    b.ToTable("CollectionMetadataID");
                });

            modelBuilder.Entity("EpisodeMetadataID", b =>
                {
                    b.Property<int>("ResourceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProviderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.HasKey("ResourceID", "ProviderID");

                    b.HasIndex("ProviderID");

                    b.ToTable("EpisodeMetadataID");
                });

            modelBuilder.Entity("Kyoo.Models.Collection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Overview")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("Kyoo.Models.Episode", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AbsoluteNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EpisodeNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<string>("Overview")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SeasonID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SeasonNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShowID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Slug")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("SeasonID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.HasIndex("ShowID", "SeasonNumber", "EpisodeNumber", "AbsoluteNumber")
                        .IsUnique();

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("Kyoo.Models.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Kyoo.Models.Library", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Paths")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("Kyoo.Models.LibraryItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndAir")
                        .HasColumnType("TEXT");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<string>("Overview")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartAir")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToView("LibraryItems");
                });

            modelBuilder.Entity("Kyoo.Models.People", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("People");
                });

            modelBuilder.Entity("Kyoo.Models.PeopleRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PeopleID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<int>("ShowID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("PeopleID");

                    b.HasIndex("ShowID");

                    b.ToTable("PeopleRoles");
                });

            modelBuilder.Entity("Kyoo.Models.Provider", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("Kyoo.Models.Season", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<string>("Overview")
                        .HasColumnType("TEXT");

                    b.Property<int>("SeasonNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShowID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Slug")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.HasIndex("ShowID", "SeasonNumber")
                        .IsUnique();

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("Kyoo.Models.Show", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Aliases")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndAir")
                        .HasColumnType("TEXT");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsMovie")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Overview")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartAir")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StudioID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.HasIndex("StudioID");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("Kyoo.Models.Studio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Studios");
                });

            modelBuilder.Entity("Kyoo.Models.Track", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codec")
                        .HasColumnType("TEXT");

                    b.Property<int>("EpisodeID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsExternal")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsForced")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("TrackIndex")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.HasIndex("EpisodeID", "Type", "Language", "TrackIndex", "IsForced")
                        .IsUnique();

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Kyoo.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExtraData")
                        .HasColumnType("TEXT");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Permissions")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Kyoo.Models.WatchedEpisode", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EpisodeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WatchedPercentage")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserID", "EpisodeID");

                    b.HasIndex("EpisodeID");

                    b.ToTable("WatchedEpisodes");
                });

            modelBuilder.Entity("LinkCollectionShow", b =>
                {
                    b.Property<int>("CollectionID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShowID")
                        .HasColumnType("INTEGER");

                    b.HasKey("CollectionID", "ShowID");

                    b.HasIndex("ShowID");

                    b.ToTable("LinkCollectionShow");
                });

            modelBuilder.Entity("LinkLibraryCollection", b =>
                {
                    b.Property<int>("CollectionID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LibraryID")
                        .HasColumnType("INTEGER");

                    b.HasKey("CollectionID", "LibraryID");

                    b.HasIndex("LibraryID");

                    b.ToTable("LinkLibraryCollection");
                });

            modelBuilder.Entity("LinkLibraryProvider", b =>
                {
                    b.Property<int>("LibraryID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProviderID")
                        .HasColumnType("INTEGER");

                    b.HasKey("LibraryID", "ProviderID");

                    b.HasIndex("ProviderID");

                    b.ToTable("LinkLibraryProvider");
                });

            modelBuilder.Entity("LinkLibraryShow", b =>
                {
                    b.Property<int>("LibraryID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShowID")
                        .HasColumnType("INTEGER");

                    b.HasKey("LibraryID", "ShowID");

                    b.HasIndex("ShowID");

                    b.ToTable("LinkLibraryShow");
                });

            modelBuilder.Entity("LinkShowGenre", b =>
                {
                    b.Property<int>("GenreID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShowID")
                        .HasColumnType("INTEGER");

                    b.HasKey("GenreID", "ShowID");

                    b.HasIndex("ShowID");

                    b.ToTable("LinkShowGenre");
                });

            modelBuilder.Entity("PeopleMetadataID", b =>
                {
                    b.Property<int>("ResourceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProviderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.HasKey("ResourceID", "ProviderID");

                    b.HasIndex("ProviderID");

                    b.ToTable("PeopleMetadataID");
                });

            modelBuilder.Entity("SeasonMetadataID", b =>
                {
                    b.Property<int>("ResourceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProviderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.HasKey("ResourceID", "ProviderID");

                    b.HasIndex("ProviderID");

                    b.ToTable("SeasonMetadataID");
                });

            modelBuilder.Entity("ShowMetadataID", b =>
                {
                    b.Property<int>("ResourceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProviderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.HasKey("ResourceID", "ProviderID");

                    b.HasIndex("ProviderID");

                    b.ToTable("ShowMetadataID");
                });

            modelBuilder.Entity("ShowUser", b =>
                {
                    b.Property<int>("UsersID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WatchedID")
                        .HasColumnType("INTEGER");

                    b.HasKey("UsersID", "WatchedID");

                    b.HasIndex("WatchedID");

                    b.ToTable("LinkUserShow");
                });

            modelBuilder.Entity("StudioMetadataID", b =>
                {
                    b.Property<int>("ResourceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProviderID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.HasKey("ResourceID", "ProviderID");

                    b.HasIndex("ProviderID");

                    b.ToTable("StudioMetadataID");
                });

            modelBuilder.Entity("CollectionMetadataID", b =>
                {
                    b.HasOne("Kyoo.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Collection", null)
                        .WithMany("ExternalIDs")
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("EpisodeMetadataID", b =>
                {
                    b.HasOne("Kyoo.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Episode", null)
                        .WithMany("ExternalIDs")
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Kyoo.Models.Episode", b =>
                {
                    b.HasOne("Kyoo.Models.Season", "Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Kyoo.Models.Show", "Show")
                        .WithMany("Episodes")
                        .HasForeignKey("ShowID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Season");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Kyoo.Models.PeopleRole", b =>
                {
                    b.HasOne("Kyoo.Models.People", "People")
                        .WithMany("Roles")
                        .HasForeignKey("PeopleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Show", "Show")
                        .WithMany("People")
                        .HasForeignKey("ShowID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("People");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Kyoo.Models.Season", b =>
                {
                    b.HasOne("Kyoo.Models.Show", "Show")
                        .WithMany("Seasons")
                        .HasForeignKey("ShowID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Kyoo.Models.Show", b =>
                {
                    b.HasOne("Kyoo.Models.Studio", "Studio")
                        .WithMany("Shows")
                        .HasForeignKey("StudioID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Studio");
                });

            modelBuilder.Entity("Kyoo.Models.Track", b =>
                {
                    b.HasOne("Kyoo.Models.Episode", "Episode")
                        .WithMany("Tracks")
                        .HasForeignKey("EpisodeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Episode");
                });

            modelBuilder.Entity("Kyoo.Models.WatchedEpisode", b =>
                {
                    b.HasOne("Kyoo.Models.Episode", "Episode")
                        .WithMany()
                        .HasForeignKey("EpisodeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.User", null)
                        .WithMany("CurrentlyWatching")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Episode");
                });

            modelBuilder.Entity("LinkCollectionShow", b =>
                {
                    b.HasOne("Kyoo.Models.Collection", null)
                        .WithMany()
                        .HasForeignKey("CollectionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Show", null)
                        .WithMany()
                        .HasForeignKey("ShowID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkLibraryCollection", b =>
                {
                    b.HasOne("Kyoo.Models.Collection", null)
                        .WithMany()
                        .HasForeignKey("CollectionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Library", null)
                        .WithMany()
                        .HasForeignKey("LibraryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkLibraryProvider", b =>
                {
                    b.HasOne("Kyoo.Models.Library", null)
                        .WithMany()
                        .HasForeignKey("LibraryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Provider", null)
                        .WithMany()
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkLibraryShow", b =>
                {
                    b.HasOne("Kyoo.Models.Library", null)
                        .WithMany()
                        .HasForeignKey("LibraryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Show", null)
                        .WithMany()
                        .HasForeignKey("ShowID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkShowGenre", b =>
                {
                    b.HasOne("Kyoo.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Show", null)
                        .WithMany()
                        .HasForeignKey("ShowID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PeopleMetadataID", b =>
                {
                    b.HasOne("Kyoo.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.People", null)
                        .WithMany("ExternalIDs")
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("SeasonMetadataID", b =>
                {
                    b.HasOne("Kyoo.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Season", null)
                        .WithMany("ExternalIDs")
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("ShowMetadataID", b =>
                {
                    b.HasOne("Kyoo.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Show", null)
                        .WithMany("ExternalIDs")
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("ShowUser", b =>
                {
                    b.HasOne("Kyoo.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Show", null)
                        .WithMany()
                        .HasForeignKey("WatchedID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudioMetadataID", b =>
                {
                    b.HasOne("Kyoo.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kyoo.Models.Studio", null)
                        .WithMany("ExternalIDs")
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Kyoo.Models.Collection", b =>
                {
                    b.Navigation("ExternalIDs");
                });

            modelBuilder.Entity("Kyoo.Models.Episode", b =>
                {
                    b.Navigation("ExternalIDs");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("Kyoo.Models.People", b =>
                {
                    b.Navigation("ExternalIDs");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Kyoo.Models.Season", b =>
                {
                    b.Navigation("Episodes");

                    b.Navigation("ExternalIDs");
                });

            modelBuilder.Entity("Kyoo.Models.Show", b =>
                {
                    b.Navigation("Episodes");

                    b.Navigation("ExternalIDs");

                    b.Navigation("People");

                    b.Navigation("Seasons");
                });

            modelBuilder.Entity("Kyoo.Models.Studio", b =>
                {
                    b.Navigation("ExternalIDs");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Kyoo.Models.User", b =>
                {
                    b.Navigation("CurrentlyWatching");
                });
#pragma warning restore 612, 618
        }
    }
}