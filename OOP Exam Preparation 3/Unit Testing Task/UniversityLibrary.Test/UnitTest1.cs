namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System;
    using System.Text;

    public class Tests
    {
        TextBook textBook;
        UniversityLibrary universityLibrary;

        [SetUp]
        public void Setup()
        {
            string title = "Mecho Puh";
            string author = "Alun Miln";
            string category = "FairyTale";

            textBook = new TextBook(title, author, category);

            universityLibrary = new UniversityLibrary();
        }

        [Test]
        public void TextBookConstructorShouldWork()
        {
            string excpectedTitle = "Mecho Puh";
            string excpecteDauthor = "Alun Miln";
            string excpectedCategory = "FairyTale";

            Assert.That(excpectedTitle, Is.EqualTo(textBook.Title));
            Assert.That(excpecteDauthor, Is.EqualTo(textBook.Author));
            Assert.That(excpectedCategory, Is.EqualTo(textBook.Category));
        }

        [Test]
        public void TextBookPropertiesSetShouldWork()
        {
            string excpectedTitle = "New Title";
            string excpecteDauthor = "New Author";
            string excpectedCategory = "Roman";

            textBook.Title = excpectedTitle;
            textBook.Author = excpecteDauthor;
            textBook.Category = excpectedCategory;

            Assert.That(excpectedTitle, Is.EqualTo(textBook.Title));
            Assert.That(excpecteDauthor, Is.EqualTo(textBook.Author));
            Assert.That(excpectedCategory, Is.EqualTo(textBook.Category));
        }

        [Test]
        public void TextBookToStringMethodShouldWork()
        {
            StringBuilder sb = new StringBuilder();

            string result = $"Book: {textBook.Title} - {textBook.InventoryNumber}{Environment.NewLine}Category: {textBook.Category}{Environment.NewLine}Author: {textBook.Author}";

            Assert.That(textBook.ToString, Is.EqualTo(result));

        }

        [Test]
        public void UniversityLibraryAddBookToLibraryShouldWork()
        {
            Assert.That(universityLibrary.Catalogue.Count, Is.EqualTo(0));

            TextBook newBook = new TextBook("MyBook", "MyAuthor", "MyCategory");

            universityLibrary.AddTextBookToLibrary(newBook);

            Assert.That(newBook.InventoryNumber, Is.EqualTo(1));
            Assert.That(universityLibrary.Catalogue.Count, Is.EqualTo(1));

            StringBuilder sb = new StringBuilder();

            string result = $"Book: {newBook.Title} - {newBook.InventoryNumber}{Environment.NewLine}Category: {newBook.Category}{Environment.NewLine}Author: {newBook.Author}";
        
            Assert.That(newBook.ToString(), Is.EqualTo(result));
        }

        [Test]
        public void UniversityLibraryLoanTextBookShouldWork()
        {

            TextBook newBook = new TextBook("MyBook", "MyAuthor", "MyCategory");
            TextBook newBook2 = new TextBook("SomeBook", "Author", "Category");

            universityLibrary.AddTextBookToLibrary(newBook);
            universityLibrary.AddTextBookToLibrary(newBook2);

            string result = universityLibrary.LoanTextBook(2, "Henry");

            Assert.That(newBook2.Holder, Is.EqualTo("Henry"));
            Assert.That(result, Is.EqualTo($"{newBook2.Title} loaned to Henry."));

            string result2 = universityLibrary.LoanTextBook(2, "Henry");

            Assert.That(result2, Is.EqualTo($"Henry still hasn't returned {newBook2.Title}!"));
        }

        [Test]
        public void UniversityLibraryReturnTextBookShouldWork()
        {
            TextBook newBook = new TextBook("MyBook", "MyAuthor", "MyCategory");
            TextBook newBook2 = new TextBook("SomeBook", "Author", "Category");

            universityLibrary.AddTextBookToLibrary(newBook);
            universityLibrary.AddTextBookToLibrary(newBook2);

            universityLibrary.LoanTextBook(2, "Henry");

            string result = universityLibrary.ReturnTextBook(2);
            string emprySting = string.Empty;

            Assert.That(emprySting, Is.EqualTo(newBook2.Holder));

            Assert.That(result, Is.EqualTo($"{newBook2.Title} is returned to the library."));
        }
    }
}