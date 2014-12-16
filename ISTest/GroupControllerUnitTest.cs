using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISCore;
using System.Collections.Generic;
using ISCore.Models;

namespace ISTest
{
    [TestClass]
    public class GroupControllerUnitTest
    {
        private GroupController TestGroupController;
        private Repository<ISGroup> TestRepository;

        [TestInitialize]
        public void Init()
        {
            TestRepository = new Repository<ISGroup>("E:/NAU/TEST/data.dat");
        }

        [TestMethod]
        public void TestInit()
        {
            TestGroupController = new GroupController(TestRepository);
            Assert.IsInstanceOfType(TestGroupController, typeof(GroupController));

            Repository<ISGroup> tRepo = new Repository<ISGroup>();
            GroupController tCtrl = new GroupController(tRepo);
            tCtrl.AddGroup(new ISGroup(14, 117));
            Assert.AreEqual(14, tRepo[0].Id);
        }

        [TestMethod]
        public void TestAddGroup()
        {
            TestGroupController = new GroupController(TestRepository);
            for (var i = 0; i < 100; i++)
            {
                TestGroupController.AddGroup(new ISGroup(i, 215+i));
            }
            try
            {
                TestGroupController.AddGroup(new ISGroup(99, 216));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Group with 99 id, is already exist!", e.Message);
            }
            Assert.AreEqual(new Student(5).Id, TestGroupController.GetGroup(5).Id);
        }

        [TestMethod]
        public void TestDeleteGroup()
        {
            TestGroupController = new GroupController(TestRepository);
            for (var i = 0; i < 50; i++)
            {
                TestGroupController.AddGroup(new ISGroup(i, 214+i));
            }

            for (var i = 0; i < 50; i += 5)
            {
                TestGroupController.RemoveGroup(TestGroupController.GetGroup(i));
            }

            Assert.AreEqual(40, TestGroupController.GetAll().Count);
        }

        [TestMethod]
        public void TestChangeGroup()
        {
            TestGroupController = new GroupController(TestRepository);
            for (var i = 0; i < 50; i++)
            {
                TestGroupController.AddGroup(new ISGroup(i, 214+i));
            }

            TestGroupController.ChangeGroup(
                TestGroupController.GetGroup(43),
                new ISGroup(90, 216));

            TestGroupController.ChangeGroup(
                TestGroupController.GetGroup(42),
                new ISGroup(80, 215));

            Assert.AreEqual(90, TestGroupController.GetGroup(90).Id);
            Assert.AreEqual(80, TestGroupController.GetGroup(80).Id);
        }

        [TestMethod]
        public void TestGetAll()
        {
            TestGroupController = new GroupController(TestRepository);

            for (var i = 0; i < 50; i++)
            {
                TestGroupController.AddGroup(new ISGroup(i, 217+i));
            }

            var listOfStudents = TestGroupController.GetAll();
            int tId = 0;
            foreach (var item in listOfStudents)
            {
                Assert.AreEqual(tId, item.Id);
                tId++;
            }
        }

        [TestMethod]
        public void TestGetGroup()
        {
            TestGroupController = new GroupController(TestRepository);

            for (var i = 0; i < 50; i++)
            {
                TestGroupController.AddGroup(new ISGroup(i, 213+i));
            }

            Assert.AreEqual(10, TestGroupController.GetGroup(10).Id);
            Assert.AreEqual(15, TestGroupController.GetGroup(15).Id);
            Assert.AreEqual(23, TestGroupController.GetGroup(23).Id);
        }
    }
}
