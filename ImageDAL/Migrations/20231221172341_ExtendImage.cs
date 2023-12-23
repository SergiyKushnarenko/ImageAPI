using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageDAL.Migrations
{
    public partial class ExtendImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Images",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Images");
        }
    }
}
