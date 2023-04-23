
function shareFacebook() {
    var url = "http://localhost:5013/Home/BrowseCause";
    window.open("https://www.facebook.com/sharer/sharer.php?u=" + encodeURIComponent(url), "_blank");
}

function shareTwitter() {
    var url = "http://localhost:5013/Home/BrowseCause";
    window.open("https://twitter.com/intent/tweet?url=" + encodeURIComponent(url), "_blank");
}

function shareLinkedIn() {
    var url = "http://localhost:5013/Home/BrowseCause";
    window.open("https://www.linkedin.com/shareArticle?url=" + encodeURIComponent(url), "_blank");
}

function searchCauses() {
    const searchInput = document.getElementById("search-input");
    const searchValue = searchInput.value.toUpperCase();
    const causesList = document.getElementById("causes-list");
    const causes = causesList.getElementsByClassName("cause-item");

    for (let i = 0; i < causes.length; i++) {
        const causeTitle = causes[i].querySelector("h1");
        const causeText = causeTitle.textContent || causeTitle.innerText;

        if (causeText.toUpperCase().indexOf(searchValue) > -1) {
            causes[i].style.display = "";
        } else {
            causes[i].style.display = "none";
        }
    }
}
