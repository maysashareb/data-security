using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data_security.Models
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "ID Number")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "ID must be 9 digits")]
        public string IdNumber { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        [RegularExpression(@"^\d{4}\s\d{4}\s\d{4}\s\d{4}$", ErrorMessage = "Card number must be in format: XXXX XXXX XXXX XXXX")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Valid Until")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/([2-9][0-9])$", ErrorMessage = "Valid date must be in format: MM/YY")]
        public string ValidDate { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC must be 3 digits")]
        public string CVC { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}