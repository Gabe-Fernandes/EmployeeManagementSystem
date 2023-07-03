using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations;

  /// <inheritdoc />
  public partial class weekendAddedToTimecard : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AddColumn<float>(
              name: "TimeInSat",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeInSun",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeOutSat",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeOutSun",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropColumn(
              name: "TimeInSat",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeInSun",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeOutSat",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeOutSun",
              table: "Timecards");
      }
  }
