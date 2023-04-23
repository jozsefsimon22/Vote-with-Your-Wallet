// Set the username to the username of the user that is logged in
let usernameCookie = "username";
let username;

// Mock data of causes from database (JSON) 
let causes = causesData;


// jQuery variables
var target = $("#cause");
var search_input = $("#search-input");

// Get the value of the cookie with the name usernameCookie
function getCookie(usernameCookie) {
    let name = usernameCookie.toLowerCase() + "=";
    let decodedCookie = decodeURIComponent(document.cookie).toLowerCase();
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i].trim();
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

// Displaying causes from the array
function addCauses() {
    for (var i = 0; i < causes.length; i++) {
        if (causes[i].username !== username) {
            const date = new Date(causes[i].date);
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            const dateCreated = date.toLocaleDateString('en-UK', options);
            target.append(
                "<div id='cause-" + causes[i].ID + "' style='border-radius: 5px; margin-bottom: 50px; background-color: #A9BDBD; padding: 10px; color: white'>" +
                "<h1>Cause: " + causes[i].causeName + "</h1>" +
                "<p>Creator: " + causes[i].username + "</p>" +
                "<p>Description: " + causes[i].description + "</p>" +
                "<p><span class='signature-toggle'>Signatures: " + "</span><span class='signature-list'><ul class=\"listTest\">" + "</ul></span></p>" +
                "<p>Date Created: " + dateCreated + "</p>" +
                "<div class='d-flex justify-content-between align-items-center'>" +
                "<button type=\"button\" class= \"btn btn-primary\" onclick=\"signCause(" + i + ")\">Sign<\/button>" +
                "<div class='d-flex flex-row-reverse'>" +
                "<i class='fa fa-facebook' onclick='shareFacebook()'></i>&nbsp;" +
                "<i class='fa fa-twitter' onclick='shareTwitter()'></i>&nbsp;" +
                "<i class='fa fa-linkedin' onclick='shareLinkedIn()'></i>" +
                "</div>" +
                "</div>" +
                "</div>"
            );
        }

    }
    // Add event listeners to signature toggles
    $(".signature-toggle").click(function () {
        $(".listTest").toggle();
    });
}

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




// Get the signature list for a cause
function getSignatureList(index) {
    var listItems = "<strong>Signatures:</strong> ";
    for (var i = 0; i < names.length; i++) {
        listItems += names[i] + ", ";
    }
    return listItems;
}

//Adds a new name to the Names list from newNames
function addNewName() {
    var randomIndex = Math.floor(Math.random() * names.length);
    names.push(names[randomIndex]);
}


function signCause(index) {
    console.log('signCause called');
    if (!search_input.val()) {
        if ($(".listTest").is(":visible")) {
            // The toggle is on (visible)
            causes[index].signatures++;
            addNewName();
            target.empty();
            addCauses();
            $(".listTest").toggle();
        } else {
            // The toggle is off (hidden)
            causes[index].signatures++;
            addNewName();
            target.empty();
            addCauses();
        }
    }
    else {
        if ($(".listTest").is(":visible")) {
            // The toggle is on (visible)
            causes[index].signatures++;
            addNewName();
            target.empty();
            search();
            $(".listTest").toggle();
        } else {
            // The toggle is off (hidden)
            causes[index].signatures++;
            addNewName();
            target.empty();
            search();
        }
    }
}

// Calling functions 
$(document).ready(function () {
    username = getCookie(usernameCookie);
    addCauses();
});

search_input.keyup(function () {
    search();
})

// Search function
function search() {
    console.log('search called');
    var search_term = search_input.val().toLowerCase();
    target.empty();
    for (var i = 0; i < causes.length; i++) {
        if (causes[i].title.toLowerCase().includes(search_term) || causes[i].description.toLowerCase().includes(search_term)) {
            target.append(
                "<div id='cause-" + i + "' style='border-radius: 5px; margin-top: 50px; background-color: #A9BDBD; padding: 10px; color: white'>" +
                "<h1>Cause: " + causes[i].title + "</h1>" +
                "<p>Creator: " + causes[i].creator + "</p>" +
                "<p>Description: " + causes[i].description + "</p>" +
                "<p><span class='signature-toggle'>Signatures: " + causes[i].signatures + "</span><span class='signature-list'><ul class=\"listTest\">" + getSignatureList(i) + "</ul></span></p>" +
                "<p>Date Created: " + causes[i].dateCreated + "</p>" +
                "<div class='d-flex justify-content-between align-items-center'>" +
                "<button type=\"button\" class= \"btn btn-primary\" onclick=\"signCause(" + i + ")\">Sign<\/button>" +
                "<div class='d-flex flex-row-reverse'>" +
                "<i class='fa fa-facebook' onclick='shareFacebook()'></i>&nbsp;" +
                "<i class='fa fa-twitter' onclick='shareTwitter()'></i>&nbsp;" +
                "<i class='fa fa-linkedin' onclick='shareLinkedIn()'></i>" +
                "</div>" +
                "</div>" +
                "</div>"
            );
        }
    }
    // Add event listeners to signature toggles
    $(".signature-toggle").click(function () {
        $(".listTest").toggle();
    });

}