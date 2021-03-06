﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpdeskDAL;

namespace Case1UnitTests
{
    [TestClass]
    public class EmployeeDAOTests
    {
        string eid = "";
        string did = "";

        public EmployeeDAOTests()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp = dao.GetByLastname("Smartypants");
            eid = emp.GetIdAsString();
            did = emp.GetDepartmentIdAsString();
        }

        [TestMethod]

        public void TestGetByLastnameShouldReturnEmployee()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Assert.IsInstanceOfType(dao.GetByLastname("Smartypants"), typeof(Employee));
        }

        [TestMethod]

        public void TestGetByIdShouldReturnEmployee()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Assert.IsInstanceOfType(dao.GetById(eid), typeof(Employee));
        }

        [TestMethod]
        public void TestCreateShouldReturnNewId()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp = new Employee();
            emp.Title = "Mr";
            emp.Firstname = "Evan";
            emp.Lastname = "Test";
            emp.Phoneno = "(555)555-5555";
            emp.Email = "elauersen@fanshaweonline.ca";
            emp.Version = 1;
            emp.SetDepartmentIdFromString(did);
            //12 byte hex = 24 byte string. This is the length of the DeptId string
            Assert.IsTrue(dao.Create(emp).GetIdAsString().Length == 24);
        }

        [TestMethod]
        public void TestUpdateShouldReturnOK()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp = dao.GetById(eid);
            emp.Phoneno = "(555)555-9999";
            Assert.IsTrue(dao.Update(emp) == UpdateStatus.Ok);
        }

        [TestMethod]
        public void TestUpdateShouldReturnStale()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp1 = dao.GetById(eid);
            Employee emp2 = dao.GetById(eid);
            emp1.Phoneno = "(555)555-1111";
            emp2.Phoneno = "(555)555-2222";
            UpdateStatus status = dao.Update(emp1);
            Assert.IsTrue(dao.Update(emp2) == UpdateStatus.Stale);
        }

        [TestMethod]
        public void TestDeleteShouldReturnOne()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp = dao.GetByLastname("Test");
            if (emp == null) //Asking if the delete ran before create
            {
                TestCreateShouldReturnNewId();
                //why does emp always come back null? 
                //emp id comes back as "00000000000etc" in debug mode Create()
                //possibly not being created properly?
                emp = dao.GetByLastname("Test");
            }
            Assert.IsTrue(dao.Delete(emp.GetIdAsString()) == 1);
        }

    }
}
