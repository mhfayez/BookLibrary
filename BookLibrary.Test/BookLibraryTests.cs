using BookLibrary.Domain;

namespace BookLibrary.Test
{
    public class BookLibraryTests
    {
        // ---------- Author tests ----------

        [Fact]
        public void AuthorConstructsWithMinimumValidData()
        {
            var a = new Author("Jane", "Austen");

            Assert.Equal("Jane", a.FirstName);
            Assert.Equal("Austen", a.LastName);
            Assert.Empty(a.Books);
        }

        [Theory]
        [InlineData("", "Austen")]
        [InlineData(" ", "Austen")]
        [InlineData("Jane", "")]
        [InlineData("Jane", " ")]
        public void AuthorInvalidNamesThrow(string first, string last)
        {
            Assert.Throws<InvalidOperationException>(() => new Author(first, last));
        }

        [Fact]
        public void AuthorDeathYearCannotBeBeforeBirthYear()
        {
            Assert.Throws<InvalidOperationException>(() => new Author("Leo", "Tolstoy", birthYear: 1828, deathYear: 1827));
        }

        // ---------- Book tests (construction) ----------

        [Fact]
        public void BookConstructsAndRegistersWithAuthor()
        {
            var a = new Author("Mary", "Shelley", 1797, 1851);
            var b = new Book("Frankenstein", a, publicationYear: 1818, pages: 280);

            Assert.Equal("Frankenstein", b.Title);
            Assert.Equal(1818, b.PublicationYear);
            Assert.Equal(280, b.Pages);
            Assert.Same(a, b.Author);

            Assert.Single(a.Books);
            Assert.Same(b, a.Books[0]);
        }

        [Theory]
        [InlineData("", 1818, 200)]
        [InlineData(" ", 1818, 200)]
        public void BookEmptyTitleThrows(string title, int year, int pages)
        {
            var a = new Author("Mary", "Shelley");
            Assert.Throws<InvalidOperationException>(() => new Book(title, a, year, pages));
        }

        [Fact]
        public void BookNullAuthorThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new Book("Test", null!, 2000, 100));
        }

        [Theory]
        [InlineData(Book.MinPublicationYear - 1)]
        [InlineData(Book.MaxPublicationYear + 1)]
        public void BookInvalidPublicationYearThrows(int year)
        {
            var a = new Author("Author", "Person");
            Assert.Throws<InvalidOperationException>(() => new Book("Some Title", a, year, 123));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void BookInvalidPagesThrow(int pages)
        {
            var a = new Author("Author", "Person");
            Assert.Throws<InvalidOperationException>(() => new Book("Some Title", a, 2001, pages));
        }

        // ---------- Book tests (mutations & invariants) ----------

        [Fact]
        public void ChangeTitleUpdatesTitleWhenValid()
        {
            var a = new Author("Mary", "Shelley");
            var b = new Book("Frankenstein", a, 1818, 280);

            b.ChangeTitle("Frankenstein: The Modern Prometheus");
            Assert.Equal("Frankenstein: The Modern Prometheus", b.Title);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeTitleEmptyThrows(string newTitle)
        {
            var a = new Author("Mary", "Shelley");
            var b = new Book("Frankenstein", a, 1818, 280);

            Assert.Throws<ArgumentException>(() => b.ChangeTitle(newTitle));
        }

        [Fact]
        public void ChangeAuthorUpdatesBothSides()
        {
            var a1 = new Author("Mary", "Shelley");
            var a2 = new Author("Percy", "Shelley");
            var b = new Book("Frankenstein", a1, 1818, 280);

            b.ChangeAuthor(a2);

            Assert.Same(a2, b.Author);
            Assert.DoesNotContain(b, a1.Books);
            Assert.Contains(b, a2.Books);
        }

        [Fact]
        public void ChangeAuthorNullThrows()
        {
            var a = new Author("Mary", "Shelley");
            var b = new Book("Frankenstein", a, 1818, 280);

            Assert.Throws<ArgumentNullException>(() => b.ChangeAuthor(null!));
        }

        [Fact]
        public void ChangePublicationYearRespectsInvariant()
        {
            var a = new Author("Mary", "Shelley");
            var b = new Book("Frankenstein", a, 1818, 280);

            b.ChangePublicationYear(2000);
            Assert.Equal(2000, b.PublicationYear);

            Assert.Throws<InvalidOperationException>(() => b.ChangePublicationYear(Book.MinPublicationYear - 5));
        }

        [Fact]
        public void ChangePagesRespectsInvariant()
        {
            var a = new Author("Mary", "Shelley");
            var b = new Book("Frankenstein", a, 1818, 280);

            b.ChangePages(300);
            Assert.Equal(300, b.Pages);

            Assert.Throws<InvalidOperationException>(() => b.ChangePages(0));
            Assert.Throws<InvalidOperationException>(() => b.ChangePages(-10));
        }

        [Fact]
        public void SetIsbnStoresTrimmedOrNull()
        {
            var a = new Author("Mary", "Shelley");
            var b = new Book("Frankenstein", a, 1818, 280);

            b.SetIsbn("  978-3-16-148410-0 ");
            Assert.Equal("978-3-16-148410-0", b.Isbn);

            b.SetIsbn(null);
            Assert.Null(b.Isbn);

            b.SetIsbn("  ");
            Assert.Null(b.Isbn);
        }

        [Fact]
        public void AuthorBooksAlwaysReferenceBack()
        {
            var a = new Author("Mary", "Shelley");
            var b1 = new Book("Frankenstein", a, 1818, 280);
            var b2 = new Book("Mathilda", a, 1819, 180);

            Assert.All(a.Books, b => Assert.Same(a, b.Author));
            Assert.Contains(b1, a.Books);
            Assert.Contains(b2, a.Books);
        }
    }

}
