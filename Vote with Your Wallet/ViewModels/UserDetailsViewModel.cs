using Vote_with_Your_Wallet.Models;

namespace Vote_with_Your_Wallet.ViewModels
{
    // View model for the user details page that contains the user and a boolean to check if the user is an admin
    public class UserDetailsViewModel
    {
        // User object that contains the user's information from the database 
        public User User { get; set; }

        // Boolean to check if the user is an admin 
        public bool IsAdmin { get; set; }
    }

}
