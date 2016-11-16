using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpdeskViewModels;
using HelpdeskDAL;

namespace Case1UnitTests
{


    [TestClass]
    public class DepartmentViewModelTests
    {

        string id_string = "";
        string dep_name = "";
        string new_dep_name = "";

        public DepartmentViewModelTests()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            dep_name = "Lab";
            new_dep_name = "Test Dep Name";
            vm.DepartmentName = dep_name;
            vm.GetByDepartmentName();
            id_string = vm.Id;

        }// no-arg constructor

        [TestMethod]
        public void TestGetByDepartmentNameShouldPopulateProps()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.DepartmentName = dep_name;
            vm.GetByDepartmentName();

            DepartmentViewModel vmd = new DepartmentViewModel();
            vmd.Id = id_string;
            vmd.GetById();

            Assert.IsTrue(vm.Id.Length == 24); // 12 byte hex = 24 byte string
            Assert.IsTrue(vm.Id == vmd.Id);
            Assert.IsTrue(vm.DepartmentName == dep_name);

        }// TestCreateShouldReturnNewId

        [TestMethod]
        public void TestCreateShouldReturnNewId()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.DepartmentName = new_dep_name;
            vm.Version = 1; // Starting Versions at 1;
            vm.Create();
            Assert.IsTrue(vm.Id.Length == 24);
        }

        [TestMethod]
        public void TestUpdateShouldReturnOk()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.Id = id_string;
            vm.GetById();
            vm.DepartmentName = "dep name changed";

            int ver = vm.Version;
            Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
            vm.GetById(); // get updated version
            Assert.IsTrue(vm.Version == ver + 1);
            Assert.IsTrue(vm.DepartmentName == "dep name changed");

            // change it back
            vm.DepartmentName = dep_name;
            Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
            vm.GetById();
            Assert.IsTrue(vm.Version == ver + 2);
            Assert.IsTrue(vm.DepartmentName == dep_name);

        }// TestUpdateShouldReturnOk

        [TestMethod]
        public void TestUpdateShouldReturnStale()
        {
            DepartmentViewModel vm1 = new DepartmentViewModel();
            DepartmentViewModel vm2 = new DepartmentViewModel();

            vm1.Id = vm2.Id = id_string;
            vm1.GetById();
            vm2.GetById();

            string name = vm1.DepartmentName;
            vm1.DepartmentName = "1";
            vm2.DepartmentName = "2";

            int ver = vm1.Version;

            Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);
            Assert.IsTrue(vm2.Update() == (int)UpdateStatus.Stale);

            vm1.GetById(); // get the update version of the emp
            Assert.IsTrue(vm1.DepartmentName == "1");
            Assert.IsTrue(vm1.Version == ver + 1);

            // change it back
            vm1.GetById();
            vm1.DepartmentName = name;
            Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);

        }// TestUpdateShouldReturnStale

        [TestMethod]
        public void TestGetByIdShouldPopulateProps()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.Id = id_string;
            vm.GetById();
            Assert.IsTrue(vm.DepartmentName == dep_name);

        }// TestGetByIdShouldPopulateProps
        

        [TestMethod]
        public void TestDeleteShouldReturnOne()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.DepartmentName = new_dep_name;
            vm.GetByDepartmentName();

            Assert.IsTrue(vm.Delete() == 1);

        }// TestDeleteShouldReturnOne
        //    string did = "";
        //    bool has_create_run_yet = false;
        //    public DepartmentViewModelTests()
        //    {
        //        DepartmentViewModel vm = new DepartmentViewModel();
        //        //vm.DepartmentName = "Administration";
        //        vm.DepartmentName = "Sales";
        //        vm.GetByDepartmentName();
        //        did = vm.Id;
        //    }

        //    [TestMethod]
        //    public void TestGetByDepartmentNameShouldPopulateProps()
        //    {
        //        DepartmentViewModel vm = new DepartmentViewModel();
        //        vm.DepartmentName = "Maintenance";
        //        vm.GetByDepartmentName();
        //        //12 byte hex = 24 byte string
        //        Assert.IsTrue(vm.Id.Length == 24);
        //    }

        //    [TestMethod]
        //    public void TestGetByIdShouldPopulateProps()
        //    {
        //        DepartmentViewModel vm = new DepartmentViewModel();
        //        vm.Id = did;
        //        vm.GetById();
        //        //12 byte hex = 24 byte string
        //        Assert.IsTrue(vm.Id.Length == 24);

        //    }

        //    [TestMethod]
        //    public void TestCreateShouldReturnNewId()
        //    {
        //        DepartmentViewModel vm = new DepartmentViewModel();
        //        vm.DepartmentName = "Test";
        //        vm.Id = did;
        //        vm.Create();
        //        //12 byte hex = 24 byte string
        //        Assert.IsTrue(vm.Id.Length == 24);

        //        has_create_run_yet = true;

        //    }

        //    [TestMethod]
        //    public void TestUpdateShouldReturnOk()
        //    {
        //        if (!has_create_run_yet)
        //        {
        //            TestCreateShouldReturnNewId();
        //        }

        //        DepartmentViewModel vm = new DepartmentViewModel();
        //        vm.Id = did;
        //        vm.GetById();
        //        string s = vm.DepartmentName;
        //        vm.DepartmentName = "TestU";
        //        Assert.IsTrue(vm.Update() == 1);
        //        //Reverting
        //        vm.GetById();
        //        vm.DepartmentName = s;
        //        Assert.IsTrue(vm.Update() == 1);
        //        Assert.IsTrue(vm.DepartmentName == s);
        //    }


        //    [TestMethod]
        //    public void TestUpdateShouldReturnStale()
        //    {
        //        DepartmentViewModel vm1 = new DepartmentViewModel();
        //        DepartmentViewModel vm2 = new DepartmentViewModel();

        //        if (!has_create_run_yet)
        //        {
        //            TestCreateShouldReturnNewId();
        //        }
        //        vm1.Id = did;
        //        vm1.GetById();
        //        vm1.DepartmentName = "TestU2";
        //        vm2.Id = did;
        //        vm2.GetById();
        //        vm2.DepartmentName = "TestU3";
        //        vm1.Update();
        //        Assert.IsTrue(vm2.Update() == -2);
        //        //Reverting
        //        string s = "Test";
        //        vm1.GetById();
        //        vm1.DepartmentName = s;
        //        vm1.Update();
        //        vm2.GetById();
        //        vm2.DepartmentName = s;
        //        vm2.Update();
        //        Assert.IsTrue(vm1.Update() == 1);
        //        Assert.IsTrue(vm1.DepartmentName == s);


        //    }

        //    [TestMethod]
        //    public void TestDeleteShouldReturnOne()
        //    {
        //        if (!has_create_run_yet)
        //        {
        //            TestCreateShouldReturnNewId();
        //        }

        //        DepartmentViewModel vm = new DepartmentViewModel();
        //        vm.DepartmentName = "Test";
        //        vm.GetByDepartmentName();
        //        Assert.IsTrue(vm.Delete() == 1);//Debug mode shows HelpdeskViewModels.DepartmentViewModel.Delete returned 1, why is it failing?
        //    }

    }
}


