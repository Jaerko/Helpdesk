$(function () {
    getAll("");
});//jQuery default function

$("#main").click(function (e) {//click on any row
    var deptId = e.target.parentNode.id;
    if (deptId === "main" || deptId === "") {
        deptId = e.target.id;//click on row somewhere else
    }

    if (deptId !== "0") {
        $("#ButtonAction").prop("value", "Update");
        $("#ButtonDelete").show();
        getById(deptId);
    }

    else // reset fields
    {
        $("#ButtonDelete").hide();
        $("#ButtonAction").prop("value", "Add");
        localStorage.setItem("Id", "new");
        $("#TextBoxDepartment").val("");
        $("#ButtonUpdate").prop("value", "Add");
        $("#ButtonDelete").hide();
        loadDepartmentDDL(-1);

    }
});//main click handler



function create() {
    dept = new Object();
    dept.DepartmentName = $("#TextBoxDepartment").val();
    //dept.DepartmentId = $("#ddlDepts").val();
    //dept.Id = localStorage.getItem("Id");
    //dept.Version = localStorage.getItem("Version");

    ajaxCall("Post", "api/departments", dept)
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

    dept = new Object();
    dept.DepartmentName = $("#TextBoxDepartment").val();
    dept.Id = localStorage.getItem("Id");
    ajaxCall("Delete", "api/departments/", dept)
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
    dept = new Object();
    dept.DepartmentName = $("#TextBoxDepartment").val();
    
    dept.Id = localStorage.getItem("Id");
    dept.Version = localStorage.getItem("Version");

    ajaxCall("Put", "api/departments", dept)
        .done(function (data) {
            $("#myModal").modal("hide");
            getAll(data);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    return false; //make sure to return false for click or REST calls get cancelled
}

function getById(deptId) {

    ajaxCall("Get", "api/departments/" + deptId, "")
    .done(function (data) {
        copyInfoToModal(data);

    })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        }); //ajaxCall Id call done
}

function copyInfoToModal(dept) {
    $("#TextBoxDepartment").val(dept.DepartmentName);
    localStorage.setItem("Id", dept.Id);
    localStorage.setItem("Version", dept.Version);
    loadDepartmentDDL(dept.DepartmentId);
}//copyInfoToModal

function buildTable(data) {
    $("#main").empty();
    div = $("<div class=\"list-group up-20\"><div>" +
        "<span class=\"col-xs-4 h4\">Department Name</span>" +
        "</div>");
    div.appendTo($("#main"))
    departments = data; //copy to global var
    btn = $("<button class=\"list-group-item\" id=\"0\" " +
        "data-toggle=\"modal\"data-target=\"#myModal\">" +
        "<span class=\"text-primary\"> Add new department...</span>"
        );
    btn.appendTo(div);


    $.each(data, function (index, dept) {
        var deptId = dept.Id;
        btn = $("<button class =\"list-group-item\" id=\"" + deptId +
            "\" data-toggle=\"modal\" data-target=\"#myModal\">");
        btn.html
        (
            "<span class=\"col-xs-4\" id=\"departmenttitle" + deptId + "\">" + dept.DepartmentName + "</span>" 

        );
        btn.appendTo(div);
    });//each
}//buildTable


function loadDepartmentDDL(deptdep) {
    $.ajax
        ({
            type: "Get",
            url: "api/departments/",
            contentType: "application/json; charset=utf-8"

        })
    .done(function (data) {
        html = "";
        $("#ddlDepts").empty();
        $.each(data, function () {
            html += "<option value=\"" + this["Id"] + "\">" + this["DepartmentName"] + "</option>";

        });
        $("#ddlDepts").append(html);
        $("#ddlDepts").val(deptdep);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        alert("error");
    });
}//loadDepartmentDDL

//get all departments
function getAll(msg) {
    $("#LabelStatus").text("Departments Loading...");

    ajaxCall("Get", "api/departments", "")
    .done(function (data) {
        buildTable(data);
        if (msg == "")
            $("#LabelStatus").text("Departments Loaded");
        else
            $("#LabelStatus").text(msg + " - Departments Loaded");

    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}//getAll


$("#ButtonDelete").click(function () {
    var deletedept = confirm("Do you really want to delete this department?");

    if (deletedept) {
        _delete();
        return !deletedept
    }
    return deletedept;
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
