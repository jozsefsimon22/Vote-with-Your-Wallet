// Define a list of users with usernames and passwords
var users = [
    {
        username: "admin",
        password: "admin"
    },
    {
        username: "nex.simon@gmail.com",
        password: "12345"
    }
];

// Attach an event listener to the login button
$("#login-btn").click(function (event) {
    event.preventDefault(); // Prevent the form from submitting

    // Get the values of the username and password input fields
    var username = $("#username").val();
    var password = $("#password").val();

    // Loop through the list of users to check if the username and password match
    var isValidUser = false;
    for (var i = 0; i < users.length; i++) {
        console.log("User " + i + ": " + users[i].username + " / " + users[i].password);
        if (users[i].username === username && users[i].password === password) {
            isValidUser = true;
            break;
        }
    }

    // Display an alert message based on whether the login was successful
    if (isValidUser) {
        alert("Login successful!");
    } else {
        alert("Invalid username or password.");
    }
});
