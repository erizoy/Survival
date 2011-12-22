using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SurvivalTesting
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
		Survival.rifleSprite rifle = new Survival.rifleSprite();
		Survival.itemLogic item = new Survival.itemLogic();
		Survival.heroSprite hero = new Survival.heroSprite();
        Survival.bulletLogic l_bullet = new Survival.bulletLogic();
        Survival.bulletSprite s_bullet = new Survival.bulletSprite();

        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
		public void Test1()
        {
			bool x = rifle.raised_rifle;
			Assert.IsFalse(x);
        }

		[TestMethod]
		public void Test2()
		{
			int x = item.p_husky(30);
			Assert.AreEqual(x, 60);
		}

		[TestMethod]
		public void Test3()
		{
			bool x = hero.isRunning;
			Assert.IsFalse(x);
		}

        [TestMethod]
        public void Test4()
        {
            var x = l_bullet.bullets;
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void Test5()
        {
            bool x = s_bullet.starting;
            Assert.IsFalse(x);
        }

        [TestMethod]
        public void Test6()
        {
            var x = rifle;
            var y = l_bullet;
            Assert.AreNotSame(x, y);
        }
    }
}
