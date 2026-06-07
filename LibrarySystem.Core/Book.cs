using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Core
{
    public class Book : ISearchable
    {
        public int Id { get; set; }

        [Required]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public Book() { }

        public Book(string isbn, string title, string author, int publishedYear)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            IsAvailable = true;
        }

        public string GetInfo()
        {
            string status = IsAvailable ? "Tillgänglig" : "Utlånad";
            return $"\"{Title}\" av {Author} ({PublishedYear}) - {status}";
        }

        public bool Matches(string searchTerm)
        {
            string term = searchTerm.ToLower();
            return Title.ToLower().Contains(term) ||
                   Author.ToLower().Contains(term) ||
                   ISBN.ToLower().Contains(term);
        }
    }
}