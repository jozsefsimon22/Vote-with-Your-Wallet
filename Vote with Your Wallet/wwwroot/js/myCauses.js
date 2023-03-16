var userName = "John Doe";

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
    {
        title: "Support Local Businesses",
        creator: "John Doe",
        description: "This cause is to promote and support local businesses in the community.",
        signatures: 63,
        dateCreated: "March 12, 2023"
    },
    {
        title: "Reduce Plastic Waste",
        creator: "Mary Johnson",
        description: "This cause is to encourage people to reduce their use of plastic and promote alternative environmentally-friendly options.",
        signatures: 218,
        dateCreated: "March 15, 2023"
    }
];


// jQuery variables
var target = $("#myCause");

// Calling functions 
$(document).ready(function () {
    addCauses();
});

// Displaying causes from the array
function addCauses() {
    for (var i = 0; i < causes.length; i++) {
        if (causes[i].creator == userName) {
            target.append(
                "<div id='cause-" + i + "' style='border-radius: 5px; margin-top: 50px; background-color: #A9BDBD; padding: 10px; color: white'>" +
                "<h1>Cause: " + causes[i].title + "</h1>" +
                "<p>Description: " + causes[i].description + "</p>" +
                "<p>Signatures: " + causes[i].signatures + "</p>" +
                "<p>Date Created: " + causes[i].dateCreated + "</p>" +
                "<button type=\"button\" class=\"btn btn-primary editButton\">Edit</button>&nbsp" +
                "<button type=\"button\" class= \"btn btn-secondary\" onclick=\"deleteCause(" + i + ")\"> Delete </button>" +
                "</div>"
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


