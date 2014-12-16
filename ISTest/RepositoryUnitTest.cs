using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISCore;

namespace ISTest
{
    [TestClass]
    public class RepositoryUnitTest
    {
        Repository<String> TestRepository = new Repository<String>();

        [TestMethod]
        public void TestInit()
        {
            Assert.AreNotEqual(null, TestRepository);
            Assert.AreEqual(0, TestRepository.Count);
            Assert.IsNotNull(TestRepository);
        }

        [TestMethod]
        public void TestAdd()
        {
            TestRepository.Add("10");
            Assert.AreEqual(1, TestRepository.Count);
            Assert.AreEqual("10", TestRepository[0]);
            TestRepository.Add("20");
            Assert.AreEqual(2, TestRepository.Count);
            Assert.AreEqual("20", TestRepository[1]);
            TestRepository.Add("30");
            Assert.AreEqual(3, TestRepository.Count);
            Assert.AreEqual("30", TestRepository[2]);
        }

        [TestMethod]
        public void TestIndexators()
        {
            try
            {
                var tValue = TestRepository[0];
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException exc)
            {
                Assert.IsNotNull(exc);
            }
            TestRepository.Add("1");
            TestRepository[0] = "78";
            Assert.AreEqual("78", TestRepository[0]);
        }

        [TestMethod]
        public void TestDelete()
        {
            for (int i = 0; i < 100; i++)
            {
                TestRepository.Add(i.ToString());
            }
            TestRepository.Remove(1);
            Assert.AreEqual("0", TestRepository[0]);
            Assert.AreEqual("2", TestRepository[1]);
            TestRepository.Remove(56);
            Assert.AreEqual("58", TestRepository[56]);
            Assert.AreEqual(98, TestRepository.Count);

            try
            {
                TestRepository.Remove(100);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }

            try
            {
                TestRepository.Remove("1000");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Error. data is not exist in Repository", e.Message);
            }

            TestRepository.Remove("78");
            Assert.AreEqual("79", TestRepository[76]);
        }

        [TestMethod]
        public void TestSaveAndLoad()
        {
            String path = "C:/NAU/TEST/data.dat";
            TestRepository.SetPath(path);
            TestRepository.Load();
            for (var i = 0; i < 100; i++)
            {
                TestRepository.Add(i.ToString());
            }
            TestRepository.Save();

            Repository<String> tRepo = new Repository<String>();
            tRepo.SetPath(path);
            tRepo.Load();
            int expected = 0;
            foreach (String item in tRepo)
            {
                Assert.AreEqual(TestRepository[expected], item);
                expected++;
            }
            Assert.AreEqual(TestRepository.Count, tRepo.Count);
        }

        [TestMethod]
        public void TestGetEnumerator()
        {
            for (var i = 0; i < 100; i++)
            {
                TestRepository.Add(i.ToString());
            }

            int expected = 0;
            foreach (var item in TestRepository)
            {
                Assert.AreEqual(expected.ToString(), item);
                expected++;
            }
        }
    }
}
