using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private Axe axe;
        private Dummy dummy;

        [SetUp]
        public void SetUp()
        {
             axe = new Axe(1,10);//attackPoints and durabilityPoints
             dummy = new Dummy(10,0);//healthPoits and durabilityPoints
        }

        [Test]
        public void Test_AxeDurabilityPointsMustDropAfterEachAttack()
        {
            for (int i = 0; i < 3; i++)
            {
                axe.Attack(dummy);
            }

            Assert.AreEqual(7,axe.DurabilityPoints ,"Durability points must be 7");

        }

        [Test]
        public void Test_DoesConstrucrotSetsAxeParamsProperly()
        {
            Assert.AreEqual(1,axe.AttackPoints);
            Assert.AreEqual(10,axe.DurabilityPoints);
        }

        [Test]
        public void Test_MethodMustThrowExceptionWhenAxeDurrabiltyIs0()
        {
            axe = new Axe(10, 0);

            Assert.Throws<InvalidOperationException>(() =>
            {
                axe.Attack(dummy); 
            });

            Assert.AreEqual(0,axe.DurabilityPoints);
        }
    }
}