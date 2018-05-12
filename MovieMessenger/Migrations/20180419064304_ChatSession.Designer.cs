﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MovieMessenger.Models;
using System;

namespace MovieMessenger.Migrations
{
    [DbContext(typeof(MovieMessengerContext))]
    [Migration("20180419064304_ChatSession")]
    partial class ChatSession
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieMessenger.Models.Account", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.HasKey("Username");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("MovieMessenger.Models.ChatSession", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.HasIndex("Username");

                    b.ToTable("ChatSession");
                });

            modelBuilder.Entity("MovieMessenger.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("From");

                    b.Property<string>("Text");

                    b.Property<string>("To");

                    b.HasKey("ID");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("MovieMessenger.Models.ChatSession", b =>
                {
                    b.HasOne("MovieMessenger.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("Username");
                });
#pragma warning restore 612, 618
        }
    }
}
