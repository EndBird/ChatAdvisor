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
    partial class MovieMessengerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("AccountUsername");

                    b.Property<string>("Chat");

                    b.Property<string>("From");

                    b.Property<string>("To");

                    b.HasKey("ID");

                    b.HasIndex("AccountUsername");

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
                        .HasForeignKey("AccountUsername");
                });
#pragma warning restore 612, 618
        }
    }
}