using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieMessenger.Migrations
{
    public partial class ChatSession2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropIndex(
                name: "IX_Message_ChatSessionID",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ChatSessionID",
                table: "Message");

            migrationBuilder.AddColumn<string>(
                name: "Chat",
                table: "ChatSession",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chat",
                table: "ChatSession");

            migrationBuilder.AddColumn<int>(
                name: "ChatSessionID",
                table: "Message",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatSessionID",
                table: "Message",
                column: "ChatSessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ChatSession_ChatSessionID",
                table: "Message",
                column: "ChatSessionID",
                principalTable: "ChatSession",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
