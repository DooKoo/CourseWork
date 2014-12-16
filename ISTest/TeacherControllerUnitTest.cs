using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISCore;
using System.Collections.Generic;
using ISCore.Models;

namespace ISTest
{
    [TestClass]
    public class TeacherControllerUnitTest
    {
        private TeacherController TestTeacherController;

        [TestMethod]
        public void TestInit()
        {
            TestTeacherController = new TeacherController("E:/NAU/TEST/data.dat");
            Assert.IsInstanceOfType(TestTeacherController, typeof(TeacherController));

            Repository<Teacher> tRepo = new Repository<Teacher>();
            TeacherController tCtrl = new TeacherController(tRepo);
            tCtrl.AddTeacher(new Teacher(14));
            Assert.AreEqual(14, tRepo[0].Id);
        }

        [TestMethod]
        public void TestAddTeacher()
        {
            TestTeacherController = new TeacherController("E:/NAU/TEST/data.dat");
            for (var i = 0; i < 100; i++)
            {
                TestTeacherController.AddTeacher(new Teacher(i));
            }
            try
            {
                TestTeacherController.AddTeacher(new Teacher(99));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Teacher with 99 id, is already exist!", e.Message);
            }
            Assert.AreEqual(new Student(5).Id, TestTeacherController.GetTeacher(5).Id);
        }

        [TestMethod]
        public void TestDeleteTeacher()
        {
            TestTeacherController = new TeacherController("E:/NAU/TEST/data.dat");
            for (var i = 0; i < 50; i++)
            {
                TestTeacherController.AddTeacher(new Teacher(i));
            }

            for (var i = 0; i < 50; i += 5)
            {
                TestTeacherController.RemoveTeacher(TestTeacherController.GetTeacher(i));
            }

            Assert.AreEqual(40, TestTeacherController.GetAll().Count);
        }

        [TestMethod]
        public void TestChangeTeacher()
        {
            TestTeacherController = new TeacherController("E:/NAU/TEST/data.dat");
            for (var i = 0; i < 50; i++)
            {
                TestTeacherController.AddTeacher(new Teacher(i));
            }

            TestTeacherController.ChangeTeacher(
                TestTeacherController.GetTeacher(43),
                new Teacher(90));

            TestTeacherController.ChangeTeacher(
                TestTeacherController.GetTeacher(42),
                new Teacher(80));

            Assert.AreEqual(90, TestTeacherController.GetTeacher(90).Id);
            Assert.AreEqual(80, TestTeacherController.GetTeacher(80).Id);
        }

        [TestMethod]
        public void TestGetAll()
        {
            TestTeacherController = new TeacherController("E:/NAU/TEST/data.dat");

            for (var i = 0; i < 50; i++)
            {
                TestTeacherController.AddTeacher(new Teacher(i));
            }

            var listOfStudents = TestTeacherController.GetAll();
            int tId = 0;
            foreach (var item in listOfStudents)
            {
                Assert.AreEqual(tId, item.Id);
                tId++;
            }
        }

        [TestMethod]
        public void TestGetTeacher()
        {
            TestTeacherController = new TeacherController("E:/NAU/TEST/data.dat");

            for (var i = 0; i < 50; i++)
            {
                TestTeacherController.AddTeacher(new Teacher(i));
            }

            Assert.AreEqual(10, TestTeacherController.GetTeacher(10).Id);
            Assert.AreEqual(15, TestTeacherController.GetTeacher(15).Id);
            Assert.AreEqual(23, TestTeacherController.GetTeacher(23).Id);
        }
    }
}
