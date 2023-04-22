using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vote_with_Your_Wallet.Models
{
    public class Admin
    {
        [Required]
        [Key]
        public int AdminId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string Username { get; set; }
        [Required]
        public User User { get; set; }
    }
}
