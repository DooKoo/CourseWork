using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISCore.Models;

namespace ISTest.ModelsUnitTests
{
    [TestClass]
    public class TeacherUnitTest
    {
        private Teacher TeacherForTest = new Teacher(1);
        [TestMethod]
        public void TestInit()
        {
            TeacherForTest.Name = "Walt";
            TeacherForTest.Surname = "Greenberg";
            Assert.AreEqual(1, TeacherForTest.Id);
            Assert.AreEqual("Walt", TeacherForTest.Name);
            Assert.AreEqual("Greenberg", TeacherForTest.Surname);
        }
    }
}