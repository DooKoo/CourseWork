using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISCore;
using System.Collections.Generic;
using ISCore.Models;

namespace ISTest
{
    [TestClass]
    public class StudentControllerUnitTest
    {
        private StudentController TestStudentController;

        [TestMethod]
        public void TestInit()
        {
            TestStudentController = new StudentController("E:/NAU/TEST/data.dat");
            Assert.IsInstanceOfType(TestStudentController, typeof(StudentController));

            Repository<Student> tRepo = new Repository<Student>();
            StudentController tCtrl = new StudentController(tRepo);
            tCtrl.AddStudent(new Student(14));
            Assert.AreEqual(14, tRepo[0].Id);
        }

        [TestMethod]
        public void TestAddStudent()
        {
            TestStudentController = new StudentController("E:/NAU/TEST/data.dat");
            for (var i = 0; i < 100; i++)
            {
                TestStudentController.AddStudent(new Student(i));
            }
            try
            {
                TestStudentController.AddStudent(new Student(99));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Student with 99 id, is already exist!", e.Message);
            }
            Assert.AreEqual(new Student(5).Id, TestStudentController.GetStudent(5).Id);
        }

        [TestMethod]
        public void TestDeleteStudent()
        {
            TestStudentController = new StudentController("E:/NAU/TEST/data.dat");
            for (var i = 0; i < 50; i++)
            {
                TestStudentController.AddStudent(new Student(i));
            }

            for (var i = 0; i < 50; i += 5)
            {
                TestStudentController.RemoveStudent(TestStudentController.GetStudent(i));
            }

            Assert.AreEqual(40, TestStudentController.GetAll().Count);
        }

        [TestMethod]
        public void TestChangeStudent()
        {
            TestStudentController = new StudentController("E:/NAU/TEST/data.dat");
            for (var i = 0; i < 50; i++)
            {
                TestStudentController.AddStudent(new Student(i));
            }

            TestStudentController.ChangeStudent(
                TestStudentController.GetStudent(43),
                new Student(90));

            TestStudentController.ChangeStudent(
                TestStudentController.GetStudent(42),
                new Student(80));

            Assert.AreEqual(90, TestStudentController.GetStudent(90).Id);
            Assert.AreEqual(80, TestStudentController.GetStudent(80).Id);
        }

        [TestMethod]
        public void TestGetAll()
        {
            TestStudentController = new StudentController("E:/NAU/TEST/data.dat");

            for (var i = 0; i < 50; i++)
            {
                TestStudentController.AddStudent(new Student(i));
            }

            var listOfStudents = TestStudentController.GetAll();
            int tId = 0;
            foreach (var item in listOfStudents)
            {
                Assert.AreEqual(tId, item.Id);
                tId++;
            }
        }

        [TestMethod]
        public void TestGetStudent()
        {
            TestStudentController = new StudentController("E:/NAU/TEST/data.dat");

            for (var i = 0; i < 50; i++)
            {
                TestStudentController.AddStudent(new Student(i));
            }

            Assert.AreEqual(10, TestStudentController.GetStudent(10).Id);
            Assert.AreEqual(15, TestStudentController.GetStudent(15).Id);
            Assert.AreEqual(23, TestStudentController.GetStudent(23).Id);
        }
    }
}
