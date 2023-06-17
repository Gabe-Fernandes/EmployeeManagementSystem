using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations;

  /// <inheritdoc />
  public partial class TimecardWeeklyHoursPropChangedFromIntToFloat : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AlterColumn<float>(
              name: "WeeklyHours",
              table: "Timecards",
              type: "real",
              nullable: false,
              oldClrType: typeof(int),
              oldType: "int");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AlterColumn<int>(
              name: "WeeklyHours",
              table: "Timecards",
              type: "int",
              nullable: false,
              oldClrType: typeof(float),
              oldType: "real");
      }
  }
