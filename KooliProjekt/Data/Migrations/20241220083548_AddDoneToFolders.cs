using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDoneToFolders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Folders",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Pictures",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "Folders",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Done",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Folders",
                newName: "ID");
        }
    }
}
