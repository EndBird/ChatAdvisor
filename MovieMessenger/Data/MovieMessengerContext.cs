using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieMessenger.Models;

namespace MovieMessenger.Models
{
    public class MovieMessengerContext : DbContext
    {
        public MovieMessengerContext (DbContextOptions<MovieMessengerContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatSession>()
                .HasKey(o => new { o.From, o.To });
        }
        public DbSet<MovieMessenger.Models.Message> Message { get; set; }

        public DbSet<MovieMessenger.Models.Account> Account { get; set; }

        public DbSet<MovieMessenger.Models.ChatSession> ChatSession { get; set; }
    }
}
