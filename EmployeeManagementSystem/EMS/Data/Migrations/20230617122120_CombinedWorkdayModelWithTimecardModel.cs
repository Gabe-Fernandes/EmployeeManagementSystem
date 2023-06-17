using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations;

  /// <inheritdoc />
  public partial class CombinedWorkdayModelWithTimecardModel : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "Workdays");

          migrationBuilder.DropColumn(
              name: "EndDate",
              table: "Timecards");

          migrationBuilder.AddColumn<float>(
              name: "TimeInFri",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeInMon",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeInThur",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeInTues",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeInWed",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeOutFri",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeOutMon",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeOutThur",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeOutTues",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);

          migrationBuilder.AddColumn<float>(
              name: "TimeOutWed",
              table: "Timecards",
              type: "real",
              nullable: false,
              defaultValue: 0f);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropColumn(
              name: "TimeInFri",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeInMon",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeInThur",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeInTues",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeInWed",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeOutFri",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeOutMon",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeOutThur",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeOutTues",
              table: "Timecards");

          migrationBuilder.DropColumn(
              name: "TimeOutWed",
              table: "Timecards");

          migrationBuilder.AddColumn<DateTime>(
              name: "EndDate",
              table: "Timecards",
              type: "datetime2",
              nullable: false,
              defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

          migrationBuilder.CreateTable(
              name: "Workdays",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  DailyHours = table.Column<int>(type: "int", nullable: false),
                  Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                  TimeIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  TimeOut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  TimecardId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Workdays", x => x.Id);
              });
      }
  }
