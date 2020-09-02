using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApacheLogParserProject.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(nullable: false),
                    RequestDateTime = table.Column<DateTime>(nullable: false),
                    ClientIpAddress = table.Column<string>(nullable: false),
                    ClientGeolocation = table.Column<string>(nullable: false),
                    RequestRoute = table.Column<string>(nullable: false),
                    RequestQueryParameters = table.Column<string>(nullable: true),
                    ResponseCode = table.Column<int>(nullable: false),
                    ResponseSize = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
