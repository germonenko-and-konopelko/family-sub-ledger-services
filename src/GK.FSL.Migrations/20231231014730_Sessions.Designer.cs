﻿// <auto-generated />
using System;
using GK.FSL.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GK.FSL.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    [Migration("20231231014730_Sessions")]
    partial class Sessions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("core")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GK.FSL.Core.Models.Session", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ClientName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("client_name");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<TimeSpan?>("IdleTimeoutOverride")
                        .HasColumnType("interval")
                        .HasColumnName("idle_timeout_override");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("ip_address");

                    b.Property<DateTimeOffset>("LastRefresh")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_refresh");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("refresh_token");

                    b.Property<DateTimeOffset>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_sessions");

                    b.HasIndex("LastRefresh")
                        .HasDatabaseName("ix_sessions_last_refresh");

                    b.HasIndex("RefreshToken")
                        .IsUnique()
                        .HasDatabaseName("ix_sessions_refresh_token");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_sessions_user_id");

                    b.HasIndex("LastRefresh", "IdleTimeoutOverride")
                        .HasDatabaseName("ix_sessions_last_refresh_idle_timeout_override");

                    b.ToTable("sessions", "auth");
                });

            modelBuilder.Entity("GK.FSL.Core.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTimeOffset>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email_address");

                    b.ToTable("users", "core");
                });

            modelBuilder.Entity("GK.FSL.Core.Models.Session", b =>
                {
                    b.HasOne("GK.FSL.Core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_sessions_users_user_id");
                });

            modelBuilder.Entity("GK.FSL.Core.Models.User", b =>
                {
                    b.OwnsOne("GK.FSL.Core.Models.Password", "Password", b1 =>
                        {
                            b1.Property<long>("UserId")
                                .HasColumnType("bigint")
                                .HasColumnName("id");

                            b1.Property<byte[]>("Hash")
                                .IsRequired()
                                .HasMaxLength(400)
                                .HasColumnType("bytea")
                                .HasColumnName("PasswordHash");

                            b1.Property<byte[]>("Salt")
                                .IsRequired()
                                .HasMaxLength(400)
                                .HasColumnType("bytea")
                                .HasColumnName("PasswordSalt");

                            b1.HasKey("UserId");

                            b1.ToTable("users", "core");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_users_id");
                        });

                    b.Navigation("Password");
                });
#pragma warning restore 612, 618
        }
    }
}
