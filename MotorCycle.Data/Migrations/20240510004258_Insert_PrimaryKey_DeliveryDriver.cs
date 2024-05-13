using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorCycle.Data.Migrations
{
    public partial class Insert_PrimaryKey_DeliveryDriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagemCNH",
                table: "DeliveryDriver",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ImagemCNH",
                table: "DeliveryDriver",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
