// Mock data of causes
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
var names = ["Emma Adams", "Oliver Alexander", "Sophia Allen", "Mason Anderson", "Isabella Bailey", "Ethan Baker", "Mia Barnes", "Lucas Bell", "Ava Bennett", "Logan Black", "Camila Brooks", "Noah Brown", "Natalie Campbell", "William Carter", "Chloe Clark", "James Collins", "Grace Cook", "Michael Cooper", "Emily Cox", "Gabriel Cruz", "Madison Davis", "Henry Diaz", "Evelyn Edwards", "Benjamin Evans", "Victoria Fisher", "Daniel Flores", "Lily Foster", "Matthew Garcia", "Hannah Gibson", "Samuel Gonzalez", "Avery Gray", "David Green", "Ella Griffin", "Christopher Hall", "Aria Harris", "Jonathan Hernandez", "Scarlett Hill", "Nathan Hughes", "Sofia Jackson", "Isaac James", "Avery Jenkins", "Jack Johnson", "Mila Jones", "Ryan Kelly", "Harper King", "Elijah Lee", "Abigail Lewis", "Connor Long", "Aaliyah Martin", "Evelyn Martinez", "Landon Mason", "Aubrey Matthews", "Nicholas Mcdonald", "Charlotte Meyer", "Caleb Mitchell", "Elizabeth Moore", "Lucas Morgan", "Hazel Morris", "Alexander Murphy", "Brianna Nelson", "Anthony Nguyen", "Riley Ortiz", "Ella Parker", "Leo Perez", "Lillian Perry", "Nicholas Phillips", "Addison Powell", "Benjamin Price", "Aaliyah Ramirez", "Mason Reed", "Madelyn Reyes", "Ethan Richardson", "Olivia Rivera", "Isabella Roberts", "David Robinson", "Grace Rodriguez", "Logan Rogers", "Sophia Ross", "Emily Russell", "Sebastian Sanchez", "Abigail Sanders", "Elijah Scott", "Mia Simmons", "Christopher Smith", "Avery Stewart", "Aria Taylor", "David Thomas", "Hazel Thompson", "Aubrey Torres", "Gabriel Turner", "Victoria Walker", "William Ward", "Charlotte Washington", "Isaac White", "Elizabeth Williams", "Evelyn Wilson", "Landon Wright", "Brianna Young"];




// Displaying causes from the array
function addCauses() {
    for (var i = 0; i < causes.length; i++) {
        target.append(
            "<div id='cause-" + i + "' style='border-radius: 5px; margin-top: 50px; background-color: #A9BDBD; padding: 10px; color: white'>" +
            "<h1>Cause: " + causes[i].title + "</h1>" +
            "<p>Creator: " + causes[i].creator + "</p>" +
            "<p>Description: " + causes[i].description + "</p>" +
            "<p><span class='signature-toggle'>Signatures: " + causes[i].signatures + "</span><span class='signature-list'><ul class=\"listTest\">" + getSignatureList(i) + "</ul></span></p>" +
            "<p>Date Created: " + causes[i].dateCreated + "</p>" +
            "<button type=\"button\" class= \"btn btn-primary\" onclick=\"signCause(" + i + ")\">Sign<\/button>&nbsp" +
            "<button type=\"button\" class= \"btn btn-secondary\" > Share <\/button>" +
            "<div>"
        )

    }
    // Add event listeners to signature toggles
    //$(".signature-toggle").click(function () {
    //    $(".listTest").toggle();
    //});

    $(".signature-toggle").click(function () {
        $(".listTest").toggle();

        if ($(".listTest").is(":visible")) {
            // The toggle is on (visible)
            console.log("Toggle is on");
        } else {
            // The toggle is off (hidden)
            console.log("Toggle is off");
        }
    });



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
    addCauses();
});

search_input.keyup(function () {
    search();
})

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
                "<p><span class='signature-toggle'>Signatures: " + causes[i].signatures + "</span><span class='signature-list'><ul class=\"listTest\">" + getSignatureList(i) + "</ul></span></p>" +
                "<p>Date Created: " + causes[i].dateCreated + "</p>" +
                "<button type=\"button\" class= \"btn btn-primary\" onclick=\"signCause(" + i + ")\">Sign<\/button>&nbsp" +
                "<button type=\"button\" class= \"btn btn-secondary\" > Share <\/button>" +
                "<div>"
            )
        }
    }
    // Add event listeners to signature toggles
    $(".signature-toggle").click(function () {
        $(".listTest").toggle();
    });

}
