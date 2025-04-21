using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkBuyMvc.Migrations
{
    /// <inheritdoc />
    public partial class VendedorCampoFkAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FkLogin",
                table: "Vendedores",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FkLogin",
                table: "Vendedores",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
