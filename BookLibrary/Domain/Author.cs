using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLibrary.Domain
{
    public sealed class Author
    {
        private readonly List<Book> _books = new();

        public string FirstName { get; }
        public string LastName { get; }
        public int? BirthYear { get; }  // Optional
        public int? DeathYear { get; }  // Optional
        public IReadOnlyList<Book> Books => _books.AsReadOnly();

        public Author(string firstName, string lastName, int? birthYear = null, int? deathYear = null)
        {
            //FirstName and LastName are required, trimmed, and non-empty.
            // TODO: trim inputs and assign

        }

        internal void AddBookInternal(Book book)
        {
            // Internal helper to avoid public mutation without checks
            // Called by Book to maintain bidirectional link

            CheckInvariant();
        }

        internal void RemoveBookInternal(Book book)
        {

            CheckInvariant();
        }


        private void CheckInvariant()
        {
            // Names must be present
            //Hint: check for string IsNullOrWhiteSpace for both FirstName and LastName
            //Hint: InvalidOperationException("Invariant violated: Author.FirstName cannot be empty.")
            /**
             * Code here to check for FirstName and LastName
             */

            // Plausible year checks
            if (BirthYear is not null && BirthYear < -3000)
                throw new InvalidOperationException("Invariant violated: Author.BirthYear is unrealistically small.");
            if (DeathYear is not null && DeathYear < -3000)
                throw new InvalidOperationException("Invariant violated: Author.DeathYear is unrealistically small.");
            if (BirthYear is not null && DeathYear is not null && DeathYear < BirthYear)
                throw new InvalidOperationException("Invariant violated: DeathYear cannot be before BirthYear.");

            // Books list must not contain nulls and all books must point back to this author
            if (_books.Any(b => b is null))
                throw new InvalidOperationException("Invariant violated: Author.Books cannot contain null.");
            if (_books.Any(b => !ReferenceEquals(b.Author, this)))
                throw new InvalidOperationException("Invariant violated: Author.Books must reference this author.");
        }
    }

}
