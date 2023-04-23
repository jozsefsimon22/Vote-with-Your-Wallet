using System.Collections.Generic;
using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.ViewModels
{
    public class CauseWithSignaturesViewModel
    {
        public Cause Cause { get; set; }
        public int SignaturesCount { get; set; }
        public List<string> SignerNames { get; set; }
    }
}
