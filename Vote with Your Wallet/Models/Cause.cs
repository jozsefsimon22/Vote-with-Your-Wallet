using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vote_with_Your_Wallet.Models
{
	public class Cause
	{
		[Required]
        [Key]
		public int ID { get; set; }
        [Required]
        public string CauseName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [ForeignKey("User")]
        public string Username { get; set; }
        [Required]
        public User User { get; set; }
    }
}
