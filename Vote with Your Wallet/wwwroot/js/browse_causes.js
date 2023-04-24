// Function to share the current page URL on Facebook
function shareFacebook() {
    var url = "http://localhost:5013/Home/BrowseCause";
    // Open a new window with the Facebook sharing dialog and the encoded URL
    window.open("https://www.facebook.com/sharer/sharer.php?u=" + encodeURIComponent(url), "_blank");
}

// Function to share the current page URL on Twitter
function shareTwitter() {
    var url = "http://localhost:5013/Home/BrowseCause";
    // Open a new window with the Twitter sharing dialog and the encoded URL
    window.open("https://twitter.com/intent/tweet?url=" + encodeURIComponent(url), "_blank");
}

// Function to share the current page URL on LinkedIn
function shareLinkedIn() {
    var url = "http://localhost:5013/Home/BrowseCause";
    // Open a new window with the LinkedIn sharing dialog and the encoded URL
    window.open("https://www.linkedin.com/shareArticle?url=" + encodeURIComponent(url), "_blank");
}

// Function to search and filter causes based on the search input
function searchCauses() {
    // Get the search input element and its value in uppercase
    const searchInput = document.getElementById("search-input");
    const searchValue = searchInput.value.toUpperCase();

    // Get the causes list and individual cause elements
    const causesList = document.getElementById("causes-list");
    const causes = causesList.getElementsByClassName("cause-item");

    // Iterate through each cause and check if it matches the search value
    for (let i = 0; i < causes.length; i++) {
        // Get the cause title text
        const causeTitle = causes[i].querySelector("h1");
        const causeText = causeTitle.textContent || causeTitle.innerText;

        // If the cause title contains the search value, display it, otherwise hide it
        if (causeText.toUpperCase().indexOf(searchValue) > -1) {
            causes[i].style.display = "";
        } else {
            causes[i].style.display = "none";
        }
    }
}
