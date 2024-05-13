using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorCycle.Data.Migrations
{
    public partial class Change_Table_Locacao_To_Rental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
               migrationBuilder.RenameTable(
               name: "Locacao",
               newName: "Rental");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
            name: "Rental",
            newName: "Locacao");
            }
    }
}
