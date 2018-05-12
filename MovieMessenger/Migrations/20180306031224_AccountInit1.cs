using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieMessenger.Migrations
{
    public partial class AccountInit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Account",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Account",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Account",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Account",
                newName: "username");
        }
    }
}
