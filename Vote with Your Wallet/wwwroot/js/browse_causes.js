﻿// Mock data of causes
let causes = [
    {
        title: "Stop Creative Tax Avoidance",
        creator: "John Doe",
        description: "This cause is to bring awareness to the issue of creative tax avoidance and encourage businesses to pay their fair share of taxes.",
        signatures: 254,
        dateCreated: "March 1, 2023"
    },
    {
        title: "Fight for Fair Employment Practices",
        creator: "Jane Smith",
        description: "This cause is to raise awareness about unfair employment practices and encourage businesses to treat their employees with respect and dignity.",
        signatures: 341,
        dateCreated: "March 5, 2023"
    },
    {
        title: "Demand Environmental Responsibility",
        creator: "Sam Lee",
        description: "This cause is to call attention to the environmental impact of businesses and demand that they take responsibility for their actions and reduce their carbon footprint.",
        signatures: 157,
        dateCreated: "March 8, 2023"
    },
];

// jQuery variables
var target = $("#cause");
var search_input = $("#search-input");

// Calling functions 
$(document).ready(function () {
    addCauses();
});

search_input.keyup(function () {
    search();
})

// Displaying causes from the array
function addCauses() {
    for (var i = 0; i < causes.length; i++) {
        target.append(
            "<div id='cause-'" + i + " style='border-radius: 5px; margin-top: 50px; background-color: #A9BDBD; padding: 10px; color: white'>" +
            "<h1>Cause: " + causes[i].title + "</h1>" +
            "<p>Creator: " + causes[i].creator + "</p>" +
            "<p>Description: " + causes[i].description + "</p>" +
            "<p>Signatures: " + causes[i].signatures + "</p>" +
            "<p>Date Created: " + causes[i].dateCreated + "</p>" +
            "<button type=\"button\" class= \"btn btn-primary\" onclick=\"signCause(" + i + ")\">Sign<\/button>&nbsp" +
            "<button type=\"button\" class= \"btn btn-secondary\" > Share <\/button>" +
            "<div>"
        )
    }
}

function signCause(index) {
    if (!search_input.val()) {
        causes[index].signatures++;
        target.empty();
        addCauses();
    }
    else {
        causes[index].signatures++;
        target.empty();
        search();
    }
}


// Search function
function search() {
    var search_term = search_input.val().toLowerCase();
    target.empty();
    for (var i = 0; i < causes.length; i++) {
        if (causes[i].title.toLowerCase().includes(search_term) || causes[i].description.toLowerCase().includes(search_term)) {
            target.append(
                "<div style='border-radius: 5px; margin-top: 50px; background-color: #A9BDBD; padding: 10px; color: white'>" +
                "<h1>Cause: " + causes[i].title + "</h1>" +
                "<p>Creator: " + causes[i].creator + "</p>" +
                "<p>Description: " + causes[i].description + "</p>" +
                "<p>Signatures: " + causes[i].signatures + "</p>" +
                "<p>Date Created: " + causes[i].dateCreated + "</p>" +
                "<button type=\"button\" class= \"btn btn-primary\" onclick=\"signCause(" + i + ")\">Sign<\/button>&nbsp" +
                "<button type=\"button\" class= \"btn btn-secondary\" > Share <\/button>" +
                "<div>"
            )
        }
    }
}
