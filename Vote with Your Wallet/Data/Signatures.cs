using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vote_with_Your_Wallet.Data
{
    public class Signatures
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public string Username { get; set; }
        public User User { get; set; }
        public int CauseId { get; set; }
        public Cause Cause { get; set; }
    }
}
