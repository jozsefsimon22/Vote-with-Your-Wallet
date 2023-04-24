using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Vote_with_Your_Wallet.Models
{
    public class User
    {
        // Define the Username property as the primary key for the User model, required field
        [Key]
        [Required]
        public string Username { get; set; }

        // Define the Password property as a required field
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Define the Email property as a required field
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Define the FirstName property as a required field
        [Required]
        public string FirstName { get; set; }

        // Define the LastName property as a required field
        [Required]
        public string LastName { get; set; }

        // Define the IsAdmin property as a required field
        [Required]
        public bool IsAdmin { get; set; }

    }
}
