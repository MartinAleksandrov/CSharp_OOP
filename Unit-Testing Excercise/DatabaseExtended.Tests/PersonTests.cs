using ExtendedDatabase;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseExtended.Tests
{
    [TestFixture]
    public class PersonTests
    {
        private Person Person;

        [SetUp]
        public void SetUp()
        {
            Person = new Person(123,"Gosho");
        }

        [Test]
        public void ConstructorMustSetProperly()
        {
            long _Id = 012345678;
            string userName = "Grumna go v Patkata";

            Person = new Person(_Id,userName);

            Assert.AreEqual(_Id,Person.Id);
            Assert.AreEqual(userName, Person.UserName);
        }

    }
}
