using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Nova30MudancaNaTabelaServicoTecnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServicoTecnicos_Id",
                table: "ServicoTecnicos",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServicoTecnicos_Id",
                table: "ServicoTecnicos");
        }
    }
}
