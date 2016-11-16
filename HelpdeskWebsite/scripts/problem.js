$(function () {
    getAll("");
});//jQuery default function

$("#main").click(function (e) {//click on any row
    var probId = e.target.parentNode.id;
    if (probId === "main" || probId === "") {
        probId = e.target.id;//click on row somewhere else
    }

    if (probId !== "0") {
        $("#ButtonAction").prop("value", "Update");
        $("#ButtonDelete").show();
        getById(probId);
    }

    else // reset fields
    {
        $("#ButtonDelete").hide();
        $("#ButtonAction").prop("value", "Add");
        localStorage.setItem("Id", "new");
        $("#TextBoxProblem").val("");
        $("#ButtonUpdate").prop("value", "Add");
        $("#ButtonDelete").hide();

    }
});//main click handler



function create() {
    prob = new Object();
    prob.Description = $("#TextBoxProblem").val();
    //prob.ProblemId = $("#ddlDepts").val();
    //prob.Id = localStorage.getItem("Id");
    //prob.Version = localStorage.getItem("Version");

    ajaxCall("Post", "api/problems", prob)
        .done(function (data) {
            $("#myModal").modal("hide");
            $("#lblstatus").text(data);
            getAll(data);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    return false; //make sure to return false for click or REST calls get cancelled
}

function _delete() {

    prob = new Object();
    prob.Description = $("#TextBoxProblem").val();
    prob.Id = localStorage.getItem("Id");
    ajaxCall("Delete", "api/problems/", prob)
            .done(function (data) {
                $("#lblstatus").text(data);
                $("#myModal").modal("hide");
                getAll(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
    return false; //make sure to return false for click or REST calls get cancelled
}

function update() {
    prob = new Object();
    prob.Description = $("#TextBoxProblem").val();

    prob.Id = localStorage.getItem("Id");
    prob.Version = localStorage.getItem("Version");

    ajaxCall("Put", "api/problems", prob)
        .done(function (data) {
            $("#myModal").modal("hide");
            getAll(data);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    return false; //make sure to return false for click or REST calls get cancelled
}

function getById(probId) {

    ajaxCall("Get", "api/problems/" + probId, "")
    .done(function (data) {
        copyInfoToModal(data);

    })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        }); //ajaxCall Id call done
}

function copyInfoToModal(prob) {
    $("#TextBoxProblem").val(prob.Description);
    localStorage.setItem("Id", prob.Id);
    localStorage.setItem("Version", prob.Version);
}//copyInfoToModal

function buildTable(data) {
    $("#main").empty();
    div = $("<div class=\"list-group up-20\"><div>" +
        "<span class=\"col-xs-4 h4\">Problem Name</span>" +
        "</div>");
    div.appendTo($("#main"))
    problems = data; //copy to global var
    btn = $("<button class=\"list-group-item\" id=\"0\" " +
        "data-toggle=\"modal\"data-target=\"#myModal\">" +
        "<span class=\"text-primary\"> Add new problem...</span>"
        );
    btn.appendTo(div);


    $.each(data, function (index, prob) {
        var probId = prob.Id;
        btn = $("<button class =\"list-group-item\" id=\"" + probId +
            "\" data-toggle=\"modal\" data-target=\"#myModal\">");
        btn.html
        (
            "<span class=\"col-xs-4\" id=\"problemtitle" + probId + "\">" + prob.Description + "</span>"

        );
        btn.appendTo(div);
    });//each
}//buildTable



//get all problems
function getAll(msg) {
    $("#LabelStatus").text("Problems Loading...");

    ajaxCall("Get", "api/problems", "")
    .done(function (data) {
        buildTable(data);
        if (msg == "")
            $("#LabelStatus").text("Problems Loaded");
        else
            $("#LabelStatus").text(msg + " - Problems Loaded");

    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}//getAll


$("#ButtonDelete").click(function () {
    var deleteprob = confirm("Do you really want to delete this problem?");

    if (deleteprob) {
        _delete();
        return !deleteprob
    }
    return deleteprob;
});//ButtonDelete click

$("#ButtonAction").click(function () {
    if ($("#ButtonAction").val() === "Update") {
        $("#ModalStatus").text("Loading...");
        update();
        $("#ModalStatus").text("");

    }
    else {
        create();
    }

    return false; //make sure to return false or REST calls get cancelled
});//ButtonAction click
