using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using экзтарасова;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Exam xm = new Exam();
        [TestMethod]
        public void TestMethod1()
        {
            var Test = xm.Input();
            Assert.AreEqual(xm.MaxElem(Test), 8);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var Test = xm.Input();
            Assert.AreEqual(xm.MinElem(Test), 2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var Test = xm.Input();
            Assert.AreEqual(xm.LenFunc(Test), 52);
        }
    }
}
