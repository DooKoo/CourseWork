using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ISCore;
using ISCore.Models;

namespace ISTest
{
    [TestClass]
    public class SearchControllerUnitTest
    {
        private SearchController TestSearchController;
        private Repository<Student> TestStudentRepository;
        private Repository<ISGroup> TestGroupRepository;
        private Repository<Subject> TestSubjectRepository;

        [TestInitialize]
        public void TestInit()
        {
            TestStudentRepository = new Repository<Student>();
            TestSubjectRepository = new Repository<Subject>();
            TestGroupRepository = new Repository<ISGroup>();

            //Students test init;
            for (int i = 0; i < 30; i++)
            {
                Student std = new Student(i);
                std.Name = "Test" + i;
                std.Surname = "Student" + i;
                std.MobilePhone = "+3809312345" + i;
                TestStudentRepository.Add(std);
            }

            for (int i = 0; i < 3; i++)
            {
                ISGroup group = new ISGroup(i, 110 + i);
                for( int k = i*10; k < 10*(i+1); k++)
                {
                    group.AddStudent(k);
                }
                
                for(int j = 0; j < 5; j++)
                {
                    Subject subj = new Subject(i * 10 + j);
                    Teacher teacher = new Teacher(i * 10 + j);
                    teacher.Name = "Name" + i;
                    teacher.Surname = "Surname" + j;
                    subj.SetTeacher(teacher.Id);
                    subj.AddGroup(group.Id);
                    group.AddTeacher(teacher.Id);
                    TestSubjectRepository.Add(subj);
                }
                TestGroupRepository.Add(group);
            }

            TestSearchController = new SearchController(TestStudentRepository,
                TestGroupRepository, TestSubjectRepository);
        }
                

        [TestMethod]
        public void SearchByNameTest()
        {
            for (int i = 0; i < 30; i++)
            {
                Student fStud = TestSearchController.SerchByName("Test" + i, "Student" + i)[0];
                Assert.AreEqual(i, fStud.Id);
            }
        }
        
        [TestMethod]
        public void SearchByGroupTest()
        {
            for (int i = 0; i < 3; i++)
            { 
                List<Student> stdList = TestSearchController.SearchByGroup(i);
                Assert.AreEqual(10, stdList.Count);
            }
        }

        [TestMethod]
        public void SearchByTeacherTest()
        {
            for(int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                { 
                    List<Student> stdList = TestSearchController.SearchByTeacher(i*10+j);
                    Assert.AreEqual(10, stdList.Count);
                }
        }

        [TestMethod]
        public void SearchBySubjectTest()
        {
            for(int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                {
                    List<Student> stdList = TestSearchController.SearchBySubject(i * 10 + j);
                    Assert.AreEqual(10, stdList.Count);
                }
        }
    }
}
