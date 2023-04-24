using System.Collections.Generic;
using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.ViewModels
{
    // Create a class CauseWithSignaturesViewModel representing a cause with signatures view model in the application
    public class CauseWithSignaturesViewModel
    {
        // Define the Cause property as a required field
        public Cause Cause { get; set; }

        // Define the SignaturesCount property as a required field
        public int SignaturesCount { get; set; }

        // Define the SignerNames property as a required field
        public List<string> SignerNames { get; set; }
    }
}
