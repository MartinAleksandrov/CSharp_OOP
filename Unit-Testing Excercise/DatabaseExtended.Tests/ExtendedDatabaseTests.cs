namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Person[] people;
        private Database database;
        private Person person;

        [SetUp]
        public void SetUp()
        {
            people = new Person[] { new Person(121, "Ivan"), new Person(101, "Gosho") };
            database = new Database(people);
        }

        [Test]
        public void ConstructorMustSetProperly()
        {
            int expectedResult = 2;

            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void AddRangeMethodMustWorkCorrectly()
        {
            people = new Person[] { new Person(12, "Ivan"), new Person(10, "Gosho"), new Person(9, "Nikoi") };
            database = new Database(people);

            Assert.AreEqual(3, database.Count);
        }

        [Test]
        public void AddRangeMethodMustThrowExceptionWhenPersonArrayCountIs_16OrMore()
        {
            Person[] persons =
            {
            new Person(1, "Pesho"),
            new Person(2, "Gosho"),
            new Person(3, "Ivan_Ivan"),
            new Person(4, "Pesho_ivanov"),
            new Person(5, "Gosho_Naskov"),
            new Person(6, "Pesh-Peshov"),
            new Person(7, "Ivan_Kaloqnov"),
            new Person(8, "Ivan_Draganchov"),
            new Person(9, "Asen"),
            new Person(10, "Jivko"),
            new Person(11, "Toshko"),
            new Person(12, "Moshko"),
            new Person(13, "Foshko"),
            new Person(14, "Loshko"),
            new Person(15, "Roshko"),
            new Person(16, "Boshko"),
            new Person(17, "Kokoshko")
            };

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                database = new Database(persons);

            });

            Assert.AreEqual("Provided data length should be in range [0..16]!", exception.Message);
        }


        [Test]
        public void MethodCountMustWorkCorrectly()
        {
            int excpectedResult = 2;
            int actualResult = database.Count;

            Assert.AreEqual(excpectedResult, actualResult);

        }


        [Test]
        public void AddMethodMustWorkCorrectly()
        {
            person = new Person(123, "Test");

            database.Add(person);

            Assert.AreEqual(3, database.Count);
        }
        [Test]
        public void AddMethodMustThowExceptionWhenCountIs16_OrMore()
        {
            Person person1 = new(3, "John");
            Person person2 = new(4, "Paul");
            Person person3 = new(5, "Green");
            Person person4 = new(6, "Brown");
            Person person5 = new(7, "Killer");
            Person person6 = new(8, "Miler");
            Person person7 = new(9, "Viler");
            Person person8 = new(10, "Siler");
            Person person9 = new(11, "Diler");
            Person person10 = new(12, "Biler");
            Person person11 = new(13, "piler");
            Person person12 = new(14, "Ailer");
            Person person13 = new(15, "Qiler");
            Person person14 = new(16, "Eiler");
            Person person15 = new(17, "THE_CHOSEN_ONE");


            database.Add(person1);
            database.Add(person2);
            database.Add(person3);
            database.Add(person4);
            database.Add(person5);
            database.Add(person6);
            database.Add(person7);
            database.Add(person8);
            database.Add(person9);
            database.Add(person10);
            database.Add(person11);
            database.Add(person12);
            database.Add(person13);
            database.Add(person14);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(()
            => database.Add(person15));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", exception.Message);
        }

        [Test]
        public void AddMethodMustThowExceptionWhenNameIsAlredyExist()
        {
            Person person = new Person(888, "Gosho");
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
             {
                 database.Add(person);
             });

            Assert.AreEqual("There is already user with this username!", exception.Message);
        }
        [Test]
        public void AddMethodMustThowExceptionWhenIDIsAlredyExist()
        {
           Person person = new Person(101, "GoshoU");


            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(person);

            });

            Assert.AreEqual("There is already user with this Id!",exception.Message);
        }

        [Test]
        public void RemoveMethodMustWorkCorrectly()
        {
            int expectedResult = 1;
            database.Remove();

            Assert.AreEqual(expectedResult, database.Count);
        }
        [Test]
        public void RemoveMethodMustWorkThrowExceptionIfCountIs0()
        {
            for (int i = 0; i < 2; i++)
            {
                database.Remove();
            }
            Assert.Throws<InvalidOperationException>(()
                => database.Remove());
        }

        [Test]
        public void FindByUsernameMethodMustReturnCorrectPerson()
        {
            Person neededPerson = people.FirstOrDefault(p => p.UserName == "Gosho");

            Assert.AreEqual(neededPerson, database.FindByUsername("Gosho"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void FindByUsernameMethodMustThrowExceptionIfUserNameIsNullOrWhiteSpace(string wrongUserName)
        {
            Assert.Throws<ArgumentNullException>(()
               => database.FindByUsername(wrongUserName), "Username parameter is null!");

        }

        [Test]
        public void FindByUsernameMethodMustThrowExceptionWhenUserNameNonExist()
        {
            Assert.Throws<InvalidOperationException>(()
               => database.FindByUsername("SomeUserName"), "No user is present by this username!");
        }

        [Test]
        public void FindByIdMethodShouldWorkCorrectly()
        {
            Person neededPerson = people.FirstOrDefault(p => p.Id == 101);

            Assert.AreEqual(neededPerson, database.FindById(101));
        }

        [TestCase(-5)]
        [TestCase(-54)]
        public void FindByIdMethodShouldThrowExceptionWhenIdIsBelow0(long id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(()
               => database.FindById(id), "Id should be a positive number!");

        }

        [TestCase(628)]
        [TestCase(527)]
        public void FindByIdMethodShouldThrowExceptionWhenIdNonExist(long id)
        {
            Assert.Throws<InvalidOperationException>(()
               => database.FindById(id), "No user is present by this ID!");

        }
    }
}