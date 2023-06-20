using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations;

  /// <inheritdoc />
  public partial class AppUserIdForTimecardModel : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AddColumn<string>(
              name: "AppUserId",
              table: "Timecards",
              type: "nvarchar(max)",
              nullable: true);

          migrationBuilder.AlterColumn<string>(
              name: "StreetAddress",
              table: "AspNetUsers",
              type: "nvarchar(40)",
              maxLength: 40,
              nullable: false,
              defaultValue: "",
              oldClrType: typeof(string),
              oldType: "nvarchar(max)",
              oldNullable: true);

          migrationBuilder.AlterColumn<string>(
              name: "State",
              table: "AspNetUsers",
              type: "nvarchar(40)",
              maxLength: 40,
              nullable: false,
              defaultValue: "",
              oldClrType: typeof(string),
              oldType: "nvarchar(max)",
              oldNullable: true);

          migrationBuilder.AlterColumn<string>(
              name: "PostalCode",
              table: "AspNetUsers",
              type: "nvarchar(40)",
              maxLength: 40,
              nullable: false,
              defaultValue: "",
              oldClrType: typeof(string),
              oldType: "nvarchar(max)",
              oldNullable: true);

          migrationBuilder.AlterColumn<string>(
              name: "LastName",
              table: "AspNetUsers",
              type: "nvarchar(40)",
              maxLength: 40,
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(max)");

          migrationBuilder.AlterColumn<string>(
              name: "FirstName",
              table: "AspNetUsers",
              type: "nvarchar(40)",
              maxLength: 40,
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(max)");

          migrationBuilder.AlterColumn<string>(
              name: "City",
              table: "AspNetUsers",
              type: "nvarchar(40)",
              maxLength: 40,
              nullable: false,
              defaultValue: "",
              oldClrType: typeof(string),
              oldType: "nvarchar(max)",
              oldNullable: true);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropColumn(
              name: "AppUserId",
              table: "Timecards");

          migrationBuilder.AlterColumn<string>(
              name: "StreetAddress",
              table: "AspNetUsers",
              type: "nvarchar(max)",
              nullable: true,
              oldClrType: typeof(string),
              oldType: "nvarchar(40)",
              oldMaxLength: 40);

          migrationBuilder.AlterColumn<string>(
              name: "State",
              table: "AspNetUsers",
              type: "nvarchar(max)",
              nullable: true,
              oldClrType: typeof(string),
              oldType: "nvarchar(40)",
              oldMaxLength: 40);

          migrationBuilder.AlterColumn<string>(
              name: "PostalCode",
              table: "AspNetUsers",
              type: "nvarchar(max)",
              nullable: true,
              oldClrType: typeof(string),
              oldType: "nvarchar(40)",
              oldMaxLength: 40);

          migrationBuilder.AlterColumn<string>(
              name: "LastName",
              table: "AspNetUsers",
              type: "nvarchar(max)",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(40)",
              oldMaxLength: 40);

          migrationBuilder.AlterColumn<string>(
              name: "FirstName",
              table: "AspNetUsers",
              type: "nvarchar(max)",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(40)",
              oldMaxLength: 40);

          migrationBuilder.AlterColumn<string>(
              name: "City",
              table: "AspNetUsers",
              type: "nvarchar(max)",
              nullable: true,
              oldClrType: typeof(string),
              oldType: "nvarchar(40)",
              oldMaxLength: 40);
      }
  }
