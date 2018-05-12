using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieMessenger.Migrations
{
    public partial class ChatSession3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountUsername",
                table: "ChatSession",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatSession_AccountUsername",
                table: "ChatSession",
                column: "AccountUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSession_Account_AccountUsername",
                table: "ChatSession",
                column: "AccountUsername",
                principalTable: "Account",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSession_Account_AccountUsername",
                table: "ChatSession");

            migrationBuilder.DropIndex(
                name: "IX_ChatSession_AccountUsername",
                table: "ChatSession");

            migrationBuilder.DropColumn(
                name: "AccountUsername",
                table: "ChatSession");
        }
    }
}
