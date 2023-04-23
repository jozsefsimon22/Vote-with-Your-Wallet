// Set the username to the username of the user that is logged in
let usernameCookie = "username";
let username;

// Mock data of causes from database (JSON)
let causes = causesData;

// jQuery variables
var target = $("#myCause");

// Calling functions
$(document).ready(function () {
    username = getCookie(usernameCookie);
    addCauses();
});

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
    for (let i = 0; i < causes.length; i++) {
        if (causes[i].username === username) {
            const date = new Date(causes[i].date);
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            const dateCreated = date.toLocaleDateString('en-UK', options);

            target.append(
                `<div id='cause-${causes[i].ID}' style='border-radius: 5px; margin-bottom: 50px; background-color: #A9BDBD; padding: 10px; color: white'>
                    <h1>Cause: ${causes[i].causeName}</h1>
                    <p>Creator: ${causes[i].username}</p>
                    <p>Description: ${causes[i].description}</p>
                    <p><span class='signature-toggle'>Signatures: </span><span class='signature-list'><ul class="listTest"></ul></span></p>
                    <p>Date Created: ${dateCreated}</p>
                    <button type="button" class="btn btn-primary editButton">Edit</button>&nbsp
                    <button type="button" class= "btn btn-secondary" onclick="deleteCause(${i})"> Delete </button>
                </div>`
            );

            // Select the edit button and add click event listener
            var editButton = $('#cause-' + i).find('.editButton');
            editButton.click(function () {
                window.location.href = 'BrowseCauses.cshtml';
            });
        }
    }
}

function deleteCause(index) {
    causes.splice(index, 1);
    target.empty();
    addCauses();
}
