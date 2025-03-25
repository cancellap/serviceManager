using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class mudancaNaTabelaEnderecoComplemento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnderecoComplementos_EnderecoId",
                table: "EnderecoComplementos");

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoComplementos_EnderecoId",
                table: "EnderecoComplementos",
                column: "EnderecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnderecoComplementos_EnderecoId",
                table: "EnderecoComplementos");

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoComplementos_EnderecoId",
                table: "EnderecoComplementos",
                column: "EnderecoId",
                unique: true);
        }
    }
}
