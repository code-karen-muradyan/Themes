using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Themes.API.Infrastructure.Migrations.Base
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Themes");

            migrationBuilder.CreateTable(
                name: "ClientRequests",
                schema: "Themes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    TraceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRequests",
                schema: "Themes");
        }
    }
}
