using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Domain
{

    public sealed class Book
    {
        
        public const int MinPublicationYear = 1450;
        public const int MaxPublicationYear = 3000; // allows future-dated catalog entries

        public string Title { get; private set; }
        public Author Author { get; private set; }
        public int PublicationYear { get; private set; }
        public int Pages { get; private set; }
        public string? Isbn { get; private set; }

        public Book(string title, Author author, int publicationYear, int pages, string? isbn = null)
        {
            
            Author = author;

            Isbn = string.IsNullOrWhiteSpace(isbn) ? null : isbn.Trim();

            // Maintain bidirectional consistency (i.e. between Book and Author)
            //hint: check if you can find a method in Author class for this purpose


        }

        public void ChangeTitle(string newTitle)
        {
            // Title must be present
            //Hint: check for string IsNullOrWhiteSpace for newTitle
            //Hint: if IsNullOrWhiteSpace then throw ArguemntException("Title cannot be empty.", nameof(newTitle));
            //If newTitle is ok then trim and assign it to the property

        }

        public void ChangeAuthor(Author newAuthor)
        {
            if (newAuthor is null) // todo: throw Argument null exception;
            if (ReferenceEquals(newAuthor, Author)) return;

            // Update both sides consistently
            Author.RemoveBookInternal(this);
            Author = newAuthor;
            Author.AddBookInternal(this);

            CheckInvariant();
        }

        public void ChangePublicationYear(int newYear)
        {

        }

        public void ChangePages(int newPages)
        {

            CheckInvariant();
        }

        public void SetIsbn(string? isbn)
        {
            Isbn = string.IsNullOrWhiteSpace(isbn) ? null : isbn.Trim();
            CheckInvariant();
        }

        public override string ToString() => $"{Title} ({PublicationYear}) by {Author}";

        private void CheckInvariant()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new InvalidOperationException("Invariant violated: Book.Title cannot be empty.");
            if (Author is null)
                throw new InvalidOperationException("Invariant violated: Book.Author cannot be null.");
            if (PublicationYear < MinPublicationYear || PublicationYear > MaxPublicationYear)
                throw new InvalidOperationException($"Invariant violated: Book.PublicationYear must be in [{MinPublicationYear}, {MaxPublicationYear}].");
            if (Pages <= 0)
                throw new InvalidOperationException("Invariant violated: Book.Pages must be positive.");

            // If we had ISBN format checks, we could add them here. For now, allow null/any trimmed string.
        }
    }

}
