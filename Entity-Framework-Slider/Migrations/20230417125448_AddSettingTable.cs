using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity_Framework_Slider.Migrations
{
    public partial class AddSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_says",
                table: "says");

            migrationBuilder.DropPrimaryKey(
                name: "PK_instagrams",
                table: "instagrams");

            migrationBuilder.RenameTable(
                name: "says",
                newName: "Says");

            migrationBuilder.RenameTable(
                name: "instagrams",
                newName: "Instagrams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Says",
                table: "Says",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instagrams",
                table: "Instagrams",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "SoftDelete", "Value" },
                values: new object[] { 1, "HeaderLogo", false, "logo.png" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "SoftDelete", "Value" },
                values: new object[] { 2, "Phone", false, "357289953" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "SoftDelete", "Value" },
                values: new object[] { 3, "Email", false, "p135@code.edu.az" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Says",
                table: "Says");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instagrams",
                table: "Instagrams");

            migrationBuilder.RenameTable(
                name: "Says",
                newName: "says");

            migrationBuilder.RenameTable(
                name: "Instagrams",
                newName: "instagrams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_says",
                table: "says",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_instagrams",
                table: "instagrams",
                column: "Id");
        }
    }
}
