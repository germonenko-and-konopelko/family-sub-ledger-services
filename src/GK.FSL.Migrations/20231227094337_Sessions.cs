using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GK.FSL.Migrations
{
    /// <inheritdoc />
    public partial class Sessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "session",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RefreshToken = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ClientName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    LastRefresh = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IdleTimeoutOverride = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_session_LastRefresh",
                schema: "auth",
                table: "session",
                column: "LastRefresh");

            migrationBuilder.CreateIndex(
                name: "IX_session_LastRefresh_IdleTimeoutOverride",
                schema: "auth",
                table: "session",
                columns: new[] { "LastRefresh", "IdleTimeoutOverride" });

            migrationBuilder.CreateIndex(
                name: "IX_session_RefreshToken",
                schema: "auth",
                table: "session",
                column: "RefreshToken",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "session",
                schema: "auth");
        }
    }
}
