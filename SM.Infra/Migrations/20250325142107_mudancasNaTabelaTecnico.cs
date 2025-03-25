using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class mudancasNaTabelaTecnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicosId",
                table: "Tecnicos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicosId",
                table: "Tecnicos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
