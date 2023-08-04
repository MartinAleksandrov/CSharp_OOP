namespace Book.Tests
{
    using System;

    using NUnit.Framework;
    using static System.Net.Mime.MediaTypeNames;

    public class Tests
    {
        private Book book;

        [SetUp]
        public void SetUp()
        {
            string bookName = "1984";
            string authorName = "Orwell";
            
            book = new Book(bookName,authorName);
        }

        [Test]
        public void ConstructorShoudWork()
        {
            Assert.AreEqual("1984",book.BookName);
            Assert.AreEqual("Orwell", book.Author);
            Assert.AreEqual(0,book.FootnoteCount);
            Assert.IsNotNull(book.FootnoteCount);
        }

        [Test]
        public void PropBookNameShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Book("","SomeAuthor"));
        }
        [Test]
        public void PropAuthowShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Book("Title", ""));
        }

        [Test]
        public void AddFootnoteShouldWork()
        {
            book.AddFootnote(1,"SomeText");

            Assert.AreEqual(1, book.FootnoteCount);
        }

        [Test]
        public void AddFootnoteShouldThrowException()
        {
            book.AddFootnote(1, "SomeText");

            Assert.AreEqual(1, book.FootnoteCount);

            Assert.Throws<InvalidOperationException>(() =>
             book.AddFootnote(1, "SomeText2ge2323"));

            Assert.AreEqual(1, book.FootnoteCount);
        }

        [Test]
        public void FindFootnoteShouldWorkProperly()
        {
            book.AddFootnote(1, "SomeText");
            book.AddFootnote(2, "SomeTextMadafaka");
            book.AddFootnote(3, "SomeText123");

            string result = $"Footnote #2: SomeTextMadafaka";

            Assert.AreEqual(result,book.FindFootnote(2));
        }


        [Test]
        public void FindFootnoteShouldThrowException()
        {
            book.AddFootnote(1, "SomeText");
            book.AddFootnote(2, "SomeTextMadafaka");
            book.AddFootnote(3, "SomeText123");

            Assert.Throws<InvalidOperationException>(() => book.FindFootnote(4));

        }

        [Test]
        public void AlterFootnoteShouldWorkProperly()
        {
            book.AddFootnote(1, "SomeText");
            book.AddFootnote(2, "SomeTextMadafaka");
            book.AddFootnote(3, "SomeText123");

            string result = $"Footnote #2: SomeTextMadafaka";

            Assert.AreEqual(result, book.FindFootnote(2));

            book.AlterFootnote(2,"SuperQkiqText");

            string resultAfterChange = $"Footnote #2: SuperQkiqText";

            Assert.AreEqual(resultAfterChange, book.FindFootnote(2));
        }

        [Test]
        public void AlterFootnoteShouldThrowException()
        {
            book.AddFootnote(1, "SomeText");
            book.AddFootnote(2, "SomeTextMadafaka");
            book.AddFootnote(3, "SomeText123");

            Assert.Throws<InvalidOperationException>(() => book.AlterFootnote(4,"newText"));
        }

    }
}