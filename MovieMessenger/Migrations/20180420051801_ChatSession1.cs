using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieMessenger.Migrations
{
    public partial class ChatSession1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSession_Account_Username",
                table: "ChatSession");

            migrationBuilder.DropIndex(
                name: "IX_ChatSession_Username",
                table: "ChatSession");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "ChatSession",
                newName: "To");

            migrationBuilder.AddColumn<int>(
                name: "ChatSessionID",
                table: "Message",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "ChatSession",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountUsername",
                table: "ChatSession",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "ChatSession",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatSessionID",
                table: "Message",
                column: "ChatSessionID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ChatSession_ChatSessionID",
                table: "Message",
                column: "ChatSessionID",
                principalTable: "ChatSession",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSession_Account_AccountUsername",
                table: "ChatSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_ChatSession_ChatSessionID",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ChatSessionID",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_ChatSession_AccountUsername",
                table: "ChatSession");

            migrationBuilder.DropColumn(
                name: "ChatSessionID",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "AccountUsername",
                table: "ChatSession");

            migrationBuilder.DropColumn(
                name: "From",
                table: "ChatSession");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "ChatSession",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "ChatSession",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatSession_Username",
                table: "ChatSession",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSession_Account_Username",
                table: "ChatSession",
                column: "Username",
                principalTable: "Account",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
