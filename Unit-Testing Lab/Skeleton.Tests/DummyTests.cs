using NUnit.Compatibility;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;
        private const int DummyXPPoints = 15;
        private const int DummyHealthPoints = 10;


        [SetUp]
        public void Setup()
        {
            dummy = new Dummy(DummyHealthPoints,DummyXPPoints);//healthPoints and expiriencePoints
        }

        //[Test]
        //public void Test_DoesConstructorSetsParamsProperly()
        //{
        //    Type type = typeof(Dummy);
        //    FieldInfo field = type.GetField("experience",BindingFlags.NonPublic | BindingFlags.Instance);

        //    object fieldValue = field.GetValue(dummy);
        //    int intValue = Convert.ToInt32(fieldValue);

        //    Assert.AreEqual(DummyHealthPoints, dummy.Health);
        //    Assert.AreEqual(DummyXPPoints, intValue);
        //}

        [Test]
        public void Test_DoesConstructorSetHealth()
        {
            Assert.AreEqual(DummyHealthPoints, dummy.Health);
        }

        [Test]
        public void Test_MethodMustThrowExceptionWhenHealthIs0()
        {
            dummy.TakeAttack(DummyHealthPoints+10);
            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.TakeAttack(1);

            });
        }
        [Test]
        public void Test_MethodMustThrowExceptionWhenHealthIsAbove0()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.GiveExperience();
            });
        }
        [Test]
        public void Test_DummyMustGiveExpirienceWhenDeath()
        {
            dummy = new Dummy(0, 0);

            Assert.AreEqual(0,0);
        }
    }
}