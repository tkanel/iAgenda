using Microsoft.EntityFrameworkCore.Migrations;

namespace iAgenda.Data.Migrations
{
    public partial class AddBranchOfficeTableUpdateCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchOfficeId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BranchOffices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchOffices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_BranchOfficeId",
                table: "Persons",
                column: "BranchOfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_BranchOffices_BranchOfficeId",
                table: "Persons",
                column: "BranchOfficeId",
                principalTable: "BranchOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_BranchOffices_BranchOfficeId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "BranchOffices");

            migrationBuilder.DropIndex(
                name: "IX_Persons_BranchOfficeId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "BranchOfficeId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "Companies");
        }
    }
}
