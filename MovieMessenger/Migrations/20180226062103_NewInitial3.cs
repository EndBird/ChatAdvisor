using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieMessenger.Migrations
{
    public partial class NewInitial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                table: "Message",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Message",
                newName: "From");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "Message",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Message",
                newName: "Name");
        }
    }
}
