$(function () {
    buildTable();
}); //jQuery default function

//main div click
$("#main").click(function (e) {
    //click on any row
    var utilId = e.target.parentNode.id;
    if (utilId === "main" || utilId === "") {
        utilId = e.target.id; //ignore click
    }
    if (utilId === "0")
    {
        loadcollections();
    }
    else
    {
        //other utility options can go here
    }
}); // main click

//build initial table
function buildTable()
{
    $("#main").empty();
    div = $("<div class=\"list-group up-20\"><div>" +
        "<span class=\"col-xs-10 h4\">Available Utilities</span>" +
        "</div>");
    btn = $("<button class =\"list-group-item\" id=\"0\">" +
        "<span class=\"text-primary\">Re-load Helpdesk Collections</span>");
    btn.appendTo(div);
    div.appendTo($("#main"))

}//buildTable

//loadcollections
function loadcollections()
{
    $("#LabelStatus").text("Deleting and Redefining Collections...");
    ajaxCall('Get', 'api/collections')
    .done(function (data) {
        $('#LabelStatus').text(data);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });//ajaxCall
}

