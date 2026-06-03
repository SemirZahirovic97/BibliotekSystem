namespace LibrarySystem
{
    public class BookCatalog
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public List<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable).ToList();
        }

        public Book? GetBookByISBN(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            return _books.Where(b => b.Matches(searchTerm)).ToList();
        }

        public List<Book> SortByTitle()
        {
            return _books.OrderBy(b => b.Title).ToList();
        }

        public List<Book> SortByYear()
        {
            return _books.OrderBy(b => b.PublishedYear).ToList();
        }
    }
}
