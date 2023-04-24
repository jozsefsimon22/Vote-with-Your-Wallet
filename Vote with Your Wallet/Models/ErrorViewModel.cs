// Define the namespace for the Vote with Your Wallet Models
namespace Vote_with_Your_Wallet.Models
{
    // Create a class ErrorViewModel representing an error view model in the application
    public class ErrorViewModel
    {
        // Define the RequestId property to store the request ID associated with the error, nullable string
        public string? RequestId { get; set; }

        // Define a read-only property ShowRequestId that returns true if RequestId is not null or empty
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
