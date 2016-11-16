//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using HelpdeskViewModels;
//using HelpdeskDAL;

//namespace Case1UnitTests
//{


//    [TestClass]
//    public class ProblemViewModelTests
//    {

//        string id_string = "";
//        string prob_name = "";
//        string new_prob_name = "";

//        public ProblemViewModelTests()
//        {
//            ProblemViewModel vm = new ProblemViewModel();
//            prob_name = "Lab";
//            new_prob_name = "Test prob Name";
//            vm.Description = prob_name;
//            vm.GetBy();
//            id_string = vm.Id;

//        }// no-arg constructor

//        [TestMethod]
//        public void TestGetByProblemNameShouldPopulateProps()
//        {
//            ProblemViewModel vm = new ProblemViewModel();
//            vm.ProblemName = prob_name;
//            vm.GetByProblemName();

//            ProblemViewModel vmd = new ProblemViewModel();
//            vmd.Id = id_string;
//            vmd.GetById();

//            Assert.IsTrue(vm.Id.Length == 24); // 12 byte hex = 24 byte string
//            Assert.IsTrue(vm.Id == vmd.Id);
//            Assert.IsTrue(vm.ProblemName == prob_name);

//        }// TestCreateShouldReturnNewId

//        [TestMethod]
//        public void TestCreateShouldReturnNewId()
//        {
//            ProblemViewModel vm = new ProblemViewModel();
//            vm.ProblemName = new_prob_name;
//            vm.Version = 1; // Starting Versions at 1;
//            vm.Create();
//            Assert.IsTrue(vm.Id.Length == 24);
//        }

//        [TestMethod]
//        public void TestUpdateShouldReturnOk()
//        {
//            ProblemViewModel vm = new ProblemViewModel();
//            vm.Id = id_string;
//            vm.GetById();
//            vm.ProblemName = "prob name changed";

//            int ver = vm.Version;
//            Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
//            vm.GetById(); // get updated version
//            Assert.IsTrue(vm.Version == ver + 1);
//            Assert.IsTrue(vm.ProblemName == "prob name changed");

//            // change it back
//            vm.ProblemName = prob_name;
//            Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
//            vm.GetById();
//            Assert.IsTrue(vm.Version == ver + 2);
//            Assert.IsTrue(vm.ProblemName == prob_name);

//        }// TestUpdateShouldReturnOk

//        [TestMethod]
//        public void TestUpdateShouldReturnStale()
//        {
//            ProblemViewModel vm1 = new ProblemViewModel();
//            ProblemViewModel vm2 = new ProblemViewModel();

//            vm1.Id = vm2.Id = id_string;
//            vm1.GetById();
//            vm2.GetById();

//            string name = vm1.ProblemName;
//            vm1.ProblemName = "1";
//            vm2.ProblemName = "2";

//            int ver = vm1.Version;

//            Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);
//            Assert.IsTrue(vm2.Update() == (int)UpdateStatus.Stale);

//            vm1.GetById(); // get the update version of the emp
//            Assert.IsTrue(vm1.ProblemName == "1");
//            Assert.IsTrue(vm1.Version == ver + 1);

//            // change it back
//            vm1.GetById();
//            vm1.ProblemName = name;
//            Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);

//        }// TestUpdateShouldReturnStale

//        [TestMethod]
//        public void TestGetByIdShouldPopulateProps()
//        {
//            ProblemViewModel vm = new ProblemViewModel();
//            vm.Id = id_string;
//            vm.GetById();
//            Assert.IsTrue(vm.ProblemName == prob_name);

//        }// TestGetByIdShouldPopulateProps


//        [TestMethod]
//        public void TestDeleteShouldReturnOne()
//        {
//            ProblemViewModel vm = new ProblemViewModel();
//            vm.ProblemName = new_prob_name;
//            vm.GetByProblemName();

//            Assert.IsTrue(vm.Delete() == 1);

//        }// TestDeleteShouldReturnOne
//        //    string did = "";
//        //    bool has_create_run_yet = false;
//        //    public ProblemViewModelTests()
//        //    {
//        //        ProblemViewModel vm = new ProblemViewModel();
//        //        //vm.ProblemName = "Administration";
//        //        vm.ProblemName = "Sales";
//        //        vm.GetByProblemName();
//        //        did = vm.Id;
//        //    }

//        //    [TestMethod]
//        //    public void TestGetByProblemNameShouldPopulateProps()
//        //    {
//        //        ProblemViewModel vm = new ProblemViewModel();
//        //        vm.ProblemName = "Maintenance";
//        //        vm.GetByProblemName();
//        //        //12 byte hex = 24 byte string
//        //        Assert.IsTrue(vm.Id.Length == 24);
//        //    }

//        //    [TestMethod]
//        //    public void TestGetByIdShouldPopulateProps()
//        //    {
//        //        ProblemViewModel vm = new ProblemViewModel();
//        //        vm.Id = did;
//        //        vm.GetById();
//        //        //12 byte hex = 24 byte string
//        //        Assert.IsTrue(vm.Id.Length == 24);

//        //    }

//        //    [TestMethod]
//        //    public void TestCreateShouldReturnNewId()
//        //    {
//        //        ProblemViewModel vm = new ProblemViewModel();
//        //        vm.ProblemName = "Test";
//        //        vm.Id = did;
//        //        vm.Create();
//        //        //12 byte hex = 24 byte string
//        //        Assert.IsTrue(vm.Id.Length == 24);

//        //        has_create_run_yet = true;

//        //    }

//        //    [TestMethod]
//        //    public void TestUpdateShouldReturnOk()
//        //    {
//        //        if (!has_create_run_yet)
//        //        {
//        //            TestCreateShouldReturnNewId();
//        //        }

//        //        ProblemViewModel vm = new ProblemViewModel();
//        //        vm.Id = did;
//        //        vm.GetById();
//        //        string s = vm.ProblemName;
//        //        vm.ProblemName = "TestU";
//        //        Assert.IsTrue(vm.Update() == 1);
//        //        //Reverting
//        //        vm.GetById();
//        //        vm.ProblemName = s;
//        //        Assert.IsTrue(vm.Update() == 1);
//        //        Assert.IsTrue(vm.ProblemName == s);
//        //    }


//        //    [TestMethod]
//        //    public void TestUpdateShouldReturnStale()
//        //    {
//        //        ProblemViewModel vm1 = new ProblemViewModel();
//        //        ProblemViewModel vm2 = new ProblemViewModel();

//        //        if (!has_create_run_yet)
//        //        {
//        //            TestCreateShouldReturnNewId();
//        //        }
//        //        vm1.Id = did;
//        //        vm1.GetById();
//        //        vm1.ProblemName = "TestU2";
//        //        vm2.Id = did;
//        //        vm2.GetById();
//        //        vm2.ProblemName = "TestU3";
//        //        vm1.Update();
//        //        Assert.IsTrue(vm2.Update() == -2);
//        //        //Reverting
//        //        string s = "Test";
//        //        vm1.GetById();
//        //        vm1.ProblemName = s;
//        //        vm1.Update();
//        //        vm2.GetById();
//        //        vm2.ProblemName = s;
//        //        vm2.Update();
//        //        Assert.IsTrue(vm1.Update() == 1);
//        //        Assert.IsTrue(vm1.ProblemName == s);


//        //    }

//        //    [TestMethod]
//        //    public void TestDeleteShouldReturnOne()
//        //    {
//        //        if (!has_create_run_yet)
//        //        {
//        //            TestCreateShouldReturnNewId();
//        //        }

//        //        ProblemViewModel vm = new ProblemViewModel();
//        //        vm.ProblemName = "Test";
//        //        vm.GetByProblemName();
//        //        Assert.IsTrue(vm.Delete() == 1);//Debug mode shows HelpdeskViewModels.ProblemViewModel.Delete returned 1, why is it failing?
//        //    }

//    }
//}


