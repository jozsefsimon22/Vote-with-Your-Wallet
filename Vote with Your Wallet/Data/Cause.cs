using System.ComponentModel.DataAnnotations;
using Vote_with_Your_Wallet.Data;
using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.Data
{
	public class Cause
	{
		public int Id { get; set; }
		[MaxLength(100)]
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public string SignatureId { get; set; }
		public string UserName { get; set; }
	}
}
