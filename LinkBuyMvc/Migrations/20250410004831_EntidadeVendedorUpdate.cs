using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkBuyMvc.Migrations
{
    /// <inheritdoc />
    public partial class EntidadeVendedorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Vendedores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FkLogin",
                table: "Vendedores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Vendedores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "FkLogin",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Vendedores");
        }
    }
}
