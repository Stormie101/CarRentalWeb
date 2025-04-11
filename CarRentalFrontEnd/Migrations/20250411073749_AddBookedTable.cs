using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalFrontEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddBookedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Booked",
                keyColumn: "CarName",
                keyValue: null,
                column: "CarName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CarName",
                table: "Booked",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Booked",
                keyColumn: "CarImage",
                keyValue: null,
                column: "CarImage",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CarImage",
                table: "Booked",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Booked",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DropoffLocation",
                table: "Booked",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PickupLocation",
                table: "Booked",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Booked");

            migrationBuilder.DropColumn(
                name: "DropoffLocation",
                table: "Booked");

            migrationBuilder.DropColumn(
                name: "PickupLocation",
                table: "Booked");

            migrationBuilder.AlterColumn<string>(
                name: "CarName",
                table: "Booked",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CarImage",
                table: "Booked",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
