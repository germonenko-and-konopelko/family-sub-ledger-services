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
                name: "sessions",
                schema: "auth",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    refresh_token = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    client_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ip_address = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    last_refresh = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    idle_timeout_override = table.Column<TimeSpan>(type: "interval", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_sessions_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_sessions_last_refresh",
                schema: "auth",
                table: "sessions",
                column: "last_refresh");

            migrationBuilder.CreateIndex(
                name: "ix_sessions_last_refresh_idle_timeout_override",
                schema: "auth",
                table: "sessions",
                columns: new[] { "last_refresh", "idle_timeout_override" });

            migrationBuilder.CreateIndex(
                name: "ix_sessions_refresh_token",
                schema: "auth",
                table: "sessions",
                column: "refresh_token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_sessions_user_id",
                schema: "auth",
                table: "sessions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sessions",
                schema: "auth");
        }
    }
}
