namespace LibrarySystem
{
    public class Library
    {
        private BookCatalog _bookCatalog = new BookCatalog();
        private MemberRegistry _memberRegistry = new MemberRegistry();
        private LoanManager _loanManager = new LoanManager();

        public void AddBook(Book book) => _bookCatalog.AddBook(book);
        public void AddMember(Member member) => _memberRegistry.AddMember(member);

        public List<Book> GetAllBooks() => _bookCatalog.GetAllBooks();
        public List<Member> GetAllMembers() => _memberRegistry.GetAllMembers();

        public Book? GetBookByISBN(string isbn) => _bookCatalog.GetBookByISBN(isbn);
        public Member? GetMemberById(string id) => _memberRegistry.GetMemberById(id);

        public List<Book> SearchBooks(string searchTerm) => _bookCatalog.SearchBooks(searchTerm);

        public List<Book> SortBooksByTitle() => _bookCatalog.SortByTitle();
        public List<Book> SortBooksByYear() => _bookCatalog.SortByYear();

        public Loan BorrowBook(string isbn, string memberId)
        {
            var book = _bookCatalog.GetBookByISBN(isbn);
            if (book == null)
                throw new InvalidOperationException("Boken hittades inte.");

            var member = _memberRegistry.GetMemberById(memberId);
            if (member == null)
                throw new InvalidOperationException("Medlemmen hittades inte.");

            return _loanManager.CreateLoan(book, member);
        }

        public void ReturnBook(string isbn)
        {
            var book = _bookCatalog.GetBookByISBN(isbn);
            if (book == null)
                throw new InvalidOperationException("Boken hittades inte.");

            var loan = _loanManager.GetActiveLoans()
                .FirstOrDefault(l => l.Book == book);
            if (loan == null)
                throw new InvalidOperationException("Inget aktivt lån hittades.");

            _loanManager.ReturnBook(book, loan.Member);
        }

        public void GetStatistics()
        {
            var allBooks = _bookCatalog.GetAllBooks();
            var activeLoans = _loanManager.GetActiveLoans();
            var mostActive = _loanManager.GetMostActiveBorrower(_memberRegistry);

            Console.WriteLine("\n=== Statistik ===");
            Console.WriteLine($"Totalt antal böcker: {allBooks.Count}");
            Console.WriteLine($"Antal utlånade böcker: {activeLoans.Count}");
            Console.WriteLine($"Antal tillgängliga böcker: {allBooks.Count(b => b.IsAvailable)}");

            if (mostActive != null)
            {
                int loanCount = _loanManager.GetAllLoans()
                    .Count(l => l.Member == mostActive);
                Console.WriteLine($"Mest aktiva låntagare: {mostActive.Name} ({loanCount} lån)");
            }
            else
            {
                Console.WriteLine("Mest aktiva låntagare: Inga lån registrerade");
            }
        }
    }
}
