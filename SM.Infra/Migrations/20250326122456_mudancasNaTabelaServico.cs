using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class mudancasNaTabelaServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ServicoTecnico",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ServicoTecnico",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ServicoTecnico",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ServicoTecnico",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ServicoTecnico",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ServicoTecnico");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ServicoTecnico");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServicoTecnico");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ServicoTecnico");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ServicoTecnico");
        }
    }
}
