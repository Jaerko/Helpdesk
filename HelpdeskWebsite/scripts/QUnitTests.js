QUnit.test("Helpdesk Tests", function (assert) {
    assert.async(5); //Asynchronous Tests
    //Get All Employees
    ajaxCall("Get", "api/employees", "")
    .then(function (data) {
        var numOfEmployees = data.length;
        assert.ok(numOfEmployees > 0, numOfEmployees + " Employees Retrieved"); //assert #1
        console.log("1");
    });

    
    //GetNyName and then Id. Subject: Smartypants
    ajaxCall("Get", "api/employeename/Smartypants", "")
    .then(function (returnedemp) {
        var empId = returnedemp.Id;
        assert.ok(returnedemp.Firstname === "Bigshot", "Employee " + returnedemp.Lastname + " retrieved"); //assert #2
        return ajaxCall("Get", "api/employees/" + empId, "");
        console.log("2");
    })
    .then(function (returnedempid)
    {
        assert.ok(returnedempid.Firstname === "Bigshot", "Found Smartypants by Id: " + returnedempid.Id);
        console.log("3");
    });



    //Get by name, create by smartypants departmentId (create with my info). Get by name (mine), then delete.
    ajaxCall("Get", "api/employeename/Smartypants", "")
    .then(function (smarty) {
        emp = new Object();
        emp.Title = "Mr.";
        emp.Firstname = "Shawn";
        emp.Lastname = "Paviglianiti";
        emp.Phoneno = "555-5556";
        emp.Email = "hogfather@relay.com";
        emp.DepartmentId = smarty.DepartmentId;
        emp.Id = localStorage.getItem("Id");
        emp.Version = localStorage.getItem("Version");

        return ajaxCall("Post", "api/employees", emp);
        console.log("4");

    })
    .then(function (createreturn) {
        assert.ok(createreturn === "Paviglianiti has been created", "Employee Paviglianiti added.");
        return ajaxCall("Get", "api/employeename/Paviglianiti", "")
        console.log("5");
    })
    .then(function (self) {
        assert.ok(self.Firstname === "Shawn", "Employee " + self.Lastname + " retrieved for delete"); //assert #2

        return ajaxCall("Delete", "api/employees/", self)
        console.log("6");
    })
    .then(function (delmsg)
    {
        assert.ok(delmsg === "Paviglianiti has been deleted", "Employee Paviglianiti has been deleted.");

    });

    //Concurrency test. Get Smartypants, update Smartypants, attempt to update again and find the "not" 
    //in the return message from the Update controller.
    ajaxCall("Get", "api/employeename/Smartypants", "")
    .then(function (returnedemp)
    {
        localStorage.setItem("emp", JSON.stringify(returnedemp));
        returnedemp.Email = "fatherhog@relay.com";
        return ajaxCall("Put", "api/employees", returnedemp);
    })
    .then (function (updatemsg)
    {
        emp = JSON.parse(localStorage.getItem("emp"));
        assert.equal(updatemsg.indexOf("not"), -1, "First update for Employee Smartypants was completed (concurrency test)");
        emp.Email = "DEATH@DEATH.COM"
        return ajaxCall("Put", "api/employees", emp);
        console.log("7");
    })
    .then(function (stalemsg)
    {
        assert.equal(stalemsg.indexOf("Stale") !== -1, "Second update for Employee Smartypants was stale (concurrency test)")
        console.log("8");
    })

    ////Get Adminstration
    ////make sure controller set up for GetByName
    //ajaxCall("Get", "api/departmentname/Administration", "")
    //.then(function (data) {
    //    assert.equal(data.DepartmentName, "Administration", "Got the Admin Dept");// assert #3
    //    console.log("4");
    //});

    ////Get Smartypants, then update him (chained example)
    //ajaxCall("Get", "api/employees/Smartypants", "")
    //.then(function (smarty) {
    //    smarty.Phoneno = "(555)555-5554";
    //    assert.ok(smarty.Id.length === 24, "Employee Smartypants retireved for update") //assert #4
    //    return ajaxCall("Put", "api/employees", smarty);
    //    console.log("4");
    //})
    //.then(function (updatemsg) {
    //    assert.equal(updatemsg.indexOf("not"), -1, "Employee Smartypants was updated"); //assert #5
    //    console.log("5");
    //});


});

