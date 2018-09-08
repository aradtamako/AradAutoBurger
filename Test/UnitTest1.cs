using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Core;
using Core.Material;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var autoBurger = new AutoBurger();
            Assert.AreEqual(File.Exists("imgs/001.png"), true);

            var results = autoBurger.GetMaterials("imgs/001.png").ToArray();

            Assert.AreEqual(results[0].GetType(), typeof(TomatoMaterial));
            Assert.AreEqual(results[1].GetType(), typeof(LettuceMaterial));
            Assert.AreEqual(results[2].GetType(), typeof(CheeseMaterial));
            Assert.AreEqual(results[3].GetType(), typeof(PattyMaterial));
        }
    }
}
