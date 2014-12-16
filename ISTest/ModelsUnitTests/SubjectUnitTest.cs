using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISCore.Models;

namespace ISTest.ModelsUnitTests
{
    [TestClass]
    public class SubjectUnitTest
    {
        private Subject SubjectForTest1 = new Subject(1);
        private Subject SubjectForTest2 = new Subject(2);

        [TestMethod]
        public void TestInit()
        {
            SubjectForTest1.Name = "OOP";
            SubjectForTest2.Name = "Architecture of PC";

            Assert.AreEqual(1, SubjectForTest1.Id);
            Assert.AreEqual(2, SubjectForTest2.Id);
            Assert.AreEqual(0, SubjectForTest1.GroupIds.Count);
            Assert.AreEqual(0, SubjectForTest2.GroupIds.Count);
            Assert.AreEqual("OOP", SubjectForTest1.Name);
            Assert.AreEqual("Architecture of PC", SubjectForTest2.Name);
        }

        [TestMethod]
        public void TestSetTeacher()
        {
            SubjectForTest1.SetTeacher(1);
            SubjectForTest2.SetTeacher(0);

            Assert.AreEqual(1, SubjectForTest1.TeacherId);
            Assert.AreEqual(0, SubjectForTest2.TeacherId);
        }

        [TestMethod]
        public void TestAddGroup()
        {
            for (int i = 0; i < 5; i++)
            {
                SubjectForTest1.AddGroup(i);
            }
            for (int i = 0; i < 3; i++)
            {
                SubjectForTest2.AddGroup(10-i);
            }

            Assert.AreEqual(5, SubjectForTest1.GroupIds.Count);
            Assert.AreEqual(3, SubjectForTest2.GroupIds.Count);
            Assert.AreEqual(0, SubjectForTest1.GroupIds[0]);
            Assert.AreEqual(9, SubjectForTest2.GroupIds[1]);
        }

        [TestMethod]
        public void TestRemoveGroup()
        {
            for (int i = 0; i < 5; i++)
            {
                SubjectForTest1.AddGroup(i);
            }
            SubjectForTest1.RemoveGroup(0);
            Assert.AreEqual(4, SubjectForTest1.GroupIds.Count);
            SubjectForTest1.RemoveGroup(1);
            Assert.AreEqual(3, SubjectForTest1.GroupIds.Count);
            SubjectForTest1.RemoveGroup(2);
            Assert.AreEqual(2, SubjectForTest1.GroupIds.Count);
        }
    }
}
