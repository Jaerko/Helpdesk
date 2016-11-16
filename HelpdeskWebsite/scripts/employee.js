$(function () {
    getAll("");
});//jQuery default function

$("#main").click(function (e) {//click on any row


    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhone: { maxlength: 15, required: true }
        },
        ignore: ".ignore, :hidden",
        errorElement: "div",
        wrapper: "div", // a wrapper around the error message
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhone: {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail: {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: "need vaild email format"
            }
        }
    });


    $.validator.addMethod("validTitle", function (value, element) { // custom rule
        return this.optional(element) || (value == "Mr." || value == "Ms." || value == "Mrs." || value == "Dr.");
    }, "");


    var empId = e.target.parentNode.id;
    if (empId === "main" || empId === "") {
        empId = e.target.id;//click on row somewhere else
    }

    if(empId !=="0")
    {
        $("#ButtonAction").prop("value", "Update");
        $("#ButtonDelete").show();
        getById(empId);
    }

    else // reset fields
    {
        $("#ButtonDelete").hide();
        $("#ButtonAction").prop("value", "Add");
        localStorage.setItem("Id", "new");
        $("#TextBoxTitle").val("");
        $("#TextBoxFirstname").val("");
        $("#TextBoxLastname").val("");
        $("#TextBoxPhone").val("");
        $("#TextBoxEmail").val("");
        $("#ButtonUpdate").prop("value", "Add");
        $("#ButtonDelete").hide();
        loadDepartmentDDL(-1);

    }


});//main click handler



function create() {
    emp = new Object();
    emp.Title = $("#TextBoxTitle").val();
    emp.Firstname = $("#TextBoxFirstname").val();
    emp.Lastname = $("#TextBoxLastname").val();
    emp.Phoneno = $("#TextBoxPhone").val();
    emp.Email = $("#TextBoxEmail").val();
    emp.DepartmentId = $("#ddlDepts").val();
    emp.Id = localStorage.getItem("Id");
    emp.Version = localStorage.getItem("Version");

    ajaxCall("Post", "api/employees", emp)
        .done(function (data) {
            $("#lblstatus").text(data);
            getAll(data);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    return false; //make sure to return false for click or REST calls get cancelled
}

function _delete()
{

    emp = new Object();
    emp.Lastname = $("#TextBoxLastname").val();
    emp.Id = localStorage.getItem("Id");
    ajaxCall("Delete", "api/employees/", emp)
            .done(function (data) {
                $("#lblstatus").text(data);
                getAll(data);
                $("#myModal").modal("hide");
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
    return false; //make sure to return false for click or REST calls get cancelled
}

function update()
{
    emp = new Object();
    emp.Title = $("#TextBoxTitle").val();
    emp.Firstname = $("#TextBoxFirstname").val();
    emp.Lastname = $("#TextBoxLastname").val();
    emp.Phoneno = $("#TextBoxPhone").val();
    emp.Email = $("#TextBoxEmail").val();
    emp.DepartmentId = $("#ddlDepts").val();
    emp.Id = localStorage.getItem("Id");
    emp.Version = localStorage.getItem("Version");

    ajaxCall("Put", "api/employees", emp)
        .done(function (data) {
            $("#myModal").modal("hide");
            getAll(data);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    return false; //make sure to return false for click or REST calls get cancelled
}

function getById(empId)
{

    ajaxCall("Get", "api/employees/" + empId, "")
    .done(function (data)
    {
        copyInfoToModal(data);
      
    })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        }); //ajaxCall Id call done
}

function copyInfoToModal(emp)
{
    $("#TextBoxTitle").val(emp.Title);
    $("#TextBoxFirstname").val(emp.Firstname);
    $("#TextBoxLastname").val(emp.Lastname);
    $("#TextBoxPhone").val(emp.Phoneno);
    $("#TextBoxEmail").val(emp.Email);
    localStorage.setItem("Id", emp.Id);
    localStorage.setItem("Version", emp.Version);
    loadDepartmentDDL(emp.DepartmentId);
}//copyInfoToModal

function buildTable(data)
{
    $("#main").empty();
    div = $("<div class=\"list-group up-20\"><div>" +
        "<span class=\"col-xs-4 h4\">Title</span>" +
        "<span class=\"col-xs-4 h4\">First</span>" +
        "<span class=\"col-xs-4 h4\">Last</span>" +
        "</div>");
    div.appendTo($("#main"))
    employees = data; //copy to global var
    btn = $("<button class=\"list-group-item\" id=\"0\" " +
        "data-toggle=\"modal\"data-target=\"#myModal\">" +
        "<span class=\"text-primary\"> Add new Employee...</span>"
        );
    btn.appendTo(div);
    
   
    $.each(data, function (index, emp) {
        var empId = emp.Id;
        btn = $("<button class =\"list-group-item\" id=\"" + empId +
            "\" data-toggle=\"modal\" data-target=\"#myModal\">");
        btn.html
        (
            "<span class=\"col-xs-4\" id=\"employeetitle" + empId + "\">" + emp.Title + "</span>" +
            "<span class=\"col-xs-4\" id=\"employeefname" + empId + "\">" + emp.Firstname + "</span>" +
            "<span class=\"col-xs-4\" id=\"emplastname" + empId + "\">" + emp.Lastname + "</span>"

        );
        btn.appendTo(div);
    });//each
}//buildTable


function loadDepartmentDDL(empdep)
{
    $.ajax
        ({
            type: "Get",
            url: "api/departments/",
            contentType: "application/json; charset=utf-8"

        })
    .done(function (data)
    {
        html = ""; 
        $("#ddlDepts").empty();
        $.each(data, function ()
        {
            html += "<option value=\"" + this["Id"] + "\">" + this["DepartmentName"] + "</option>";

        });
        $("#ddlDepts").append(html); 
        $("#ddlDepts").val(empdep);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        alert("error");
    });
}//loadDepartmentDDL

//get all employees
function getAll(msg)
{
    $("#LabelStatus").text("Employees Loading...");

    ajaxCall("Get", "api/employees", "")
    .done(function (data)
    {
        buildTable(data);
        if (msg == "")
            $("#LabelStatus").text("Employees Loaded");
        else
            $("#LabelStatus").text(msg + " - Employees Loaded");

    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}//getAll


$("#ButtonDelete").click(function ()
{
    var deleteEmp = confirm("Do you really want to delete this employee?");
    
    if(deleteEmp)
    {
        _delete();
        return !deleteEmp
    }
    return deleteEmp;
});//ButtonDelete click

$("#ButtonAction").click(function ()
{
    
    //   reset validation
    var validator = $('#EmployeeModalForm').validate();
    validator.resetForm();

    if ($("#ButtonAction").val() === "Update")
    {
        $("#ModalStatus").text("Loading...");
        if ($("#EmployeeModalForm").valid())
        {
            $("#lblstatus").text("data Validated by jQuery!");
            $("#lblstatus.css").css({ "color": "green" });
            update();
            $("#ModalStatus").text("");
        }
        else
        {
            $("#lblstatus").text("fix existing problems");
            $("#lblstatus").css({ "color": "red" });
        }
    }
    else
    {
        if ($("#EmployeeModalForm").valid())
        {
            $("#lblstatus").text("data Validated by jQuery!");
            $("#lblstatus").css({ "color": "green"});
            create();
            $("#ModalStatus").text("");
        }
        else
        {
            $("#lblstatus").text("Fix the problem");
            $("#lblstatus").css({ "color": "red" });
        }
       
    }

    return false; //make sure to return false or REST calls get cancelled
});//ButtonAction click
