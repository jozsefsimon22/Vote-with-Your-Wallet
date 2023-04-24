using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vote_with_Your_Wallet.Models
{
    public class Signatures
    {
        // Define the ID property as the primary key for the Signatures model, required field
        [Key]
        public int ID { get; set; }

        // Define the Username property as a required field, foreign key to the User model
        [ForeignKey("User")]
        public string Username { get; set; }

        // Define the User property as a required field
        [Required]
        public User User { get; set; }

        // Define the CauseId property as a required field, foreign key to the Cause model
        public int CauseId { get; set; }

        // Define the Cause property as a required field
        [Required]
        public Cause Cause { get; set; }
    }
}
