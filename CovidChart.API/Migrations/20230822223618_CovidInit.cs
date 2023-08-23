using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CovidChart.API.Migrations
{
    /// <inheritdoc />
    public partial class CovidInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Covids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City1 = table.Column<int>(type: "int", nullable: false),
                    City2 = table.Column<int>(type: "int", nullable: false),
                    City3 = table.Column<int>(type: "int", nullable: false),
                    City4 = table.Column<int>(type: "int", nullable: false),
                    City5 = table.Column<int>(type: "int", nullable: false),
                    CovidDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Covids", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Covids");
        }
    }
}
