using System;
using System.ComponentModel.DataAnnotations;

namespace data_security.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(64)] // SHA-256 produces a 64-character string
        public string PasswordHash { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}