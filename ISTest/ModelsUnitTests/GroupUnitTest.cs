using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISCore.Models;

namespace ISTest.ModelsUnitTests
{
    [TestClass]
    public class GroupUnitTest
    {
        private ISGroup GroupForTest1 = new ISGroup(1, 216);
        private ISGroup GroupForTest2 = new ISGroup(2, 215);

        [TestMethod]
        public void TestInit()
        {
            Assert.AreEqual(1, GroupForTest1.Id);
            Assert.AreEqual(216, GroupForTest1.Number);

            Assert.AreEqual(2, GroupForTest2.Id);
            Assert.AreEqual(215, GroupForTest2.Number);
        }

        [TestMethod]
        public void TestAddStudent()
        {
            for (var i = 0; i < 10; i++)
            {
                GroupForTest1.AddStudent(i);
            }

            for (var i = 0; i < 5; i++)
            {
                GroupForTest2.AddStudent(100 - i);
            }

            Assert.AreEqual(10, GroupForTest1.StudentIDs.Count);
            Assert.AreEqual(5, GroupForTest2.StudentIDs.Count);
        }

        [TestMethod]
        public void TestRemoveStudent()
        {
            for (var i = 0; i < 10; i++)
            {
                GroupForTest1.AddStudent(i);
            }
            GroupForTest1.RemoveStudent(0);
            GroupForTest1.RemoveStudent(1);
            GroupForTest1.RemoveStudent(2);
            Assert.AreEqual(7, GroupForTest1.StudentIDs.Count);
        }

        [TestMethod]
        public void TestAddTeacher()
        {
            for (var i = 0; i < 10; i++)
            {
                GroupForTest1.AddTeacher(i);
            }

            for (var i = 0; i < 5; i++)
            {
                GroupForTest2.AddTeacher(100 - i);
            }

            Assert.AreEqual(10, GroupForTest1.TeacherIDs.Count);
            Assert.AreEqual(5, GroupForTest2.TeacherIDs.Count);
        }
    }
}
