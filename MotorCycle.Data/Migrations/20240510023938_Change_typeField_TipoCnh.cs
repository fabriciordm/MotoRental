using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorCycle.Data.Migrations
{
    public partial class Change_typeField_TipoCnh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TipoCNH",
                table: "DeliveryDriver",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TipoCNH",
                table: "DeliveryDriver",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
