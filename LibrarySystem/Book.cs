namespace LibrarySystem
{
    public class Book : ISearchable
    {
        public string ISBN { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }

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