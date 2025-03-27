using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class mudancaNaTabelaServicoTecnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicoTecnico_Servicos_ServicoId",
                table: "ServicoTecnico");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicoTecnico_Tecnicos_TecnicoId",
                table: "ServicoTecnico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicoTecnico",
                table: "ServicoTecnico");

            migrationBuilder.RenameTable(
                name: "ServicoTecnico",
                newName: "ServicoTecnicos");

            migrationBuilder.RenameIndex(
                name: "IX_ServicoTecnico_ServicoId",
                table: "ServicoTecnicos",
                newName: "IX_ServicoTecnicos_ServicoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicoTecnicos",
                table: "ServicoTecnicos",
                columns: new[] { "TecnicoId", "ServicoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoTecnicos_Servicos_ServicoId",
                table: "ServicoTecnicos",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoTecnicos_Tecnicos_TecnicoId",
                table: "ServicoTecnicos",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicoTecnicos_Servicos_ServicoId",
                table: "ServicoTecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicoTecnicos_Tecnicos_TecnicoId",
                table: "ServicoTecnicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicoTecnicos",
                table: "ServicoTecnicos");

            migrationBuilder.RenameTable(
                name: "ServicoTecnicos",
                newName: "ServicoTecnico");

            migrationBuilder.RenameIndex(
                name: "IX_ServicoTecnicos_ServicoId",
                table: "ServicoTecnico",
                newName: "IX_ServicoTecnico_ServicoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicoTecnico",
                table: "ServicoTecnico",
                columns: new[] { "TecnicoId", "ServicoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoTecnico_Servicos_ServicoId",
                table: "ServicoTecnico",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoTecnico_Tecnicos_TecnicoId",
                table: "ServicoTecnico",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
