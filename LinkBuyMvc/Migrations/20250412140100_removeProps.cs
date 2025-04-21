using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkBuyMvc.Migrations
{
    /// <inheritdoc />
    public partial class removeProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Vendedores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Vendedores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Vendedores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
