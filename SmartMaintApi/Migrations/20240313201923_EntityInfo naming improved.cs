using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMaintApi.Migrations
{
    /// <inheritdoc />
    public partial class EntityInfonamingimproved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityInfo_ModifiedUser",
                table: "Users",
                newName: "EntityInfo_ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "EntityInfo_ModifiedDate",
                table: "Users",
                newName: "EntityInfo_Modified");

            migrationBuilder.RenameColumn(
                name: "EntityInfo_DeleteUser",
                table: "Users",
                newName: "EntityInfo_DeleteBy");

            migrationBuilder.RenameColumn(
                name: "EntityInfo_CreateUser",
                table: "Users",
                newName: "EntityInfo_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "EntityInfo_CreateDate",
                table: "Users",
                newName: "EntityInfo_Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityInfo_ModifiedBy",
                table: "Users",
                newName: "EntityInfo_ModifiedUser");

            migrationBuilder.RenameColumn(
                name: "EntityInfo_Modified",
                table: "Users",
                newName: "EntityInfo_ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "EntityInfo_DeleteBy",
                table: "Users",
                newName: "EntityInfo_DeleteUser");

            migrationBuilder.RenameColumn(
                name: "EntityInfo_CreatedBy",
                table: "Users",
                newName: "EntityInfo_CreateUser");

            migrationBuilder.RenameColumn(
                name: "EntityInfo_Created",
                table: "Users",
                newName: "EntityInfo_CreateDate");
        }
    }
}
