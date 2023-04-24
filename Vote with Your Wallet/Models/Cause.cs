using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vote_with_Your_Wallet.Models
{
    // Create a class Cause representing a cause in the application
    public class Cause
	{
        // Define the ID property as the primary key for the Cause model, required field
        [Required]
        [Key]
		public int ID { get; set; }

        // Define the CauseName property as a required field
        [Required]
        public string CauseName { get; set; }

        // Define the Description property as a required field
        [Required]
        public string Description { get; set; }

        // Define the Date property as a required field
        [Required]
        public DateTime Date { get; set; }

        // Define the Username property as a required field, foreign key to the User model
        [Required]
        [ForeignKey("User")]
        public string Username { get; set; }

        // Define the User property as a required field
        [Required]
        public User User { get; set; }
    }
}
