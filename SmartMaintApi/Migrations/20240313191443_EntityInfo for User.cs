using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMaintApi.Migrations
{
    /// <inheritdoc />
    public partial class EntityInfoforUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EntityInfo_CreateDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EntityInfo_CreateUser",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityInfo_DeleteUser",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityInfo_Deleted",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityInfo_ModifiedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityInfo_ModifiedUser",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityInfo_CreateDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EntityInfo_CreateUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EntityInfo_DeleteUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EntityInfo_Deleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EntityInfo_ModifiedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EntityInfo_ModifiedUser",
                table: "Users");
        }
    }
}
