namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {

        private Database database;
        private int[] array;

        [SetUp]
        public void SetUp()
        {
            array = new int[] { 1, 2, 3, 4 };
            database = new Database();
        }

        [Test]
        public void DoesConstructorSetParamsProperly()
        {
            database = new Database(array);

            Assert.AreEqual(array.Length, database.Count);
        }

        [Test]
        public void AddMethodMustWorkCorrectly()
        {
            database.Add(1);
            database.Add(2);
            database.Add(3);

            Assert.AreEqual(3, database.Count);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 123, 123 })]

        public void AddMethodMustThrowExceptionWhenCountIs16_OrAbove(int[] ints)
        {
            Assert.Throws<InvalidOperationException>(()
                => database = new Database(ints), "Array's capacity must be exactly 16 integers!");
        }
        [Test]
        public void RemoveMethodMustWorkCorrectly()
        {
            database = new Database(array);

            database.Remove();
            database.Remove();

            Assert.AreEqual(2, database.Count);
        }
        [Test]
        public void RemoveMethodMustThrowExceptionWhenCountIs0()
        {
            database = new Database(1, 2);

            database.Remove();
            database.Remove();

            Assert.Throws<InvalidOperationException>(()
                => database.Remove(), "The collection is empty!");
        }

        [Test]
        public void FetchMethodMustWorkCorrectly()
        {
            int[] ints = { 1, 2, 3, 4, 5, 6 };
            database = new Database(ints);

            int[] expectctedResult = database.Fetch();

            Assert.AreEqual(expectctedResult,ints);
        }

    }
}
