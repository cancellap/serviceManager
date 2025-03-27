using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Nova50MudancaNaTabelaServicoTecnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ServicoTecnicos");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ServicoTecnicos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServicoTecnicos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ServicoTecnicos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ServicoTecnicos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ServicoTecnicos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ServicoTecnicos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ServicoTecnicos",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ServicoTecnicos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ServicoTecnicos",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
