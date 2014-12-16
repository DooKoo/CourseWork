using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISCore.Models;

namespace ISTest.ModelsUnitTests
{
    [TestClass]
    public class StudentUnitTest
    {
        private Student StudentForTest1 = new Student(1);

        [TestMethod]
        public void TestInit()
        {
            Assert.AreEqual(1, StudentForTest1.Id);
        }

        [TestMethod]
        public void TestProperties()
        {
            StudentForTest1.Name = "Thomas";
            StudentForTest1.Surname = "Morgan";
            StudentForTest1.MobilePhone = "+380931234567";
            StudentForTest1.Course = 3;
            StudentForTest1.Address = "Nizhynska 29D";
            StudentForTest1.GradeBook = "ST1234";

            Assert.AreEqual(1, StudentForTest1.Id);
            Assert.AreEqual("Thomas", StudentForTest1.Name);
            Assert.AreEqual("Morgan", StudentForTest1.Surname);
            Assert.AreEqual("+380931234567", StudentForTest1.MobilePhone);
            Assert.AreEqual(3, StudentForTest1.Course);
            Assert.AreEqual("Nizhynska 29D", StudentForTest1.Address);
            Assert.AreEqual("ST1234", StudentForTest1.GradeBook);
        }
    }
}
