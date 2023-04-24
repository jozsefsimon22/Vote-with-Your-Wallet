using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.Controllers
{
    internal class CauseWithSignatures
    {
        public Cause Cause { get; set; }
        public object SignaturesCount { get; set; }
        public object SignerNames { get; set; }
    }
}