using data_security.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace data_security.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set Username as unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configure one-to-one relationship between User and CreditCard
            modelBuilder.Entity<User>()
                .HasOne(u => u.CreditCard)
                .WithOne(c => c.User)
                .HasForeignKey<CreditCard>(c => c.UserId);

            var adminPasswordHash = HashPassword("admin123");
            var userPasswordHash = HashPassword("User123!");
            var createdAt = new DateTime(2025, 4, 19, 14, 0, 0);

            var usersList = new List<User>
    {
        new User { UserID = 1, Username = "admin", PasswordHash = adminPasswordHash, IsAdmin = true, Email = "admin@example.com", CreatedAt = createdAt },
        new User { UserID = 2, Username = "user1", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user1@example.com", CreatedAt = createdAt },
        new User { UserID = 3, Username = "user2", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user2@example.com", CreatedAt = createdAt },
        new User { UserID = 4, Username = "user3", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user3@example.com", CreatedAt = createdAt },
        new User { UserID = 5, Username = "user4", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user4@example.com", CreatedAt = createdAt },
        new User { UserID = 6, Username = "user5", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user5@example.com", CreatedAt = createdAt },
        new User { UserID = 7, Username = "user6", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user6@example.com", CreatedAt = createdAt },
        new User { UserID = 8, Username = "user7", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user7@example.com", CreatedAt = createdAt },
        new User { UserID = 9, Username = "user8", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user8@example.com", CreatedAt = createdAt },
        new User { UserID = 10, Username = "user9", PasswordHash = userPasswordHash, IsAdmin = false, Email = "user9@example.com", CreatedAt = createdAt },
    };

            var creditCardsList = new List<CreditCard>
    {
        new CreditCard { Id = 1, FirstName = "Israeli", LastName = "Israeili", IdNumber = "123456789", CardNumber = "1234 5567 8901 2345", ValidDate = "12/32", CVC = "123", UserId = 1 },
        new CreditCard { Id = 2, FirstName = "User1", LastName = "Last1", IdNumber = "100000002", CardNumber = "4000 1002 5678 9002", ValidDate = "10/30", CVC = "102", UserId = 2 },
        new CreditCard { Id = 3, FirstName = "User2", LastName = "Last2", IdNumber = "100000003", CardNumber = "4000 1003 5678 9003", ValidDate = "10/30", CVC = "103", UserId = 3 },
        new CreditCard { Id = 4, FirstName = "User3", LastName = "Last3", IdNumber = "100000004", CardNumber = "4000 1004 5678 9004", ValidDate = "10/30", CVC = "104", UserId = 4 },
        new CreditCard { Id = 5, FirstName = "User4", LastName = "Last4", IdNumber = "100000005", CardNumber = "4000 1005 5678 9005", ValidDate = "10/30", CVC = "105", UserId = 5 },
        new CreditCard { Id = 6, FirstName = "User5", LastName = "Last5", IdNumber = "100000006", CardNumber = "4000 1006 5678 9006", ValidDate = "10/30", CVC = "106", UserId = 6 },
        new CreditCard { Id = 7, FirstName = "User6", LastName = "Last6", IdNumber = "100000007", CardNumber = "4000 1007 5678 9007", ValidDate = "10/30", CVC = "107", UserId = 7 },
        new CreditCard { Id = 8, FirstName = "User7", LastName = "Last7", IdNumber = "100000008", CardNumber = "4000 1008 5678 9008", ValidDate = "10/30", CVC = "108", UserId = 8 },
        new CreditCard { Id = 9, FirstName = "User8", LastName = "Last8", IdNumber = "100000009", CardNumber = "4000 1009 5678 9009", ValidDate = "10/30", CVC = "109", UserId = 9 },
        new CreditCard { Id = 10, FirstName = "User9", LastName = "Last9", IdNumber = "100000010", CardNumber = "4000 1010 5678 9010", ValidDate = "10/30", CVC = "110", UserId = 10 },
    };

            modelBuilder.Entity<User>().HasData(usersList);
            modelBuilder.Entity<CreditCard>().HasData(creditCardsList);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}