namespace LibrarySystem
{
    public class Member : ISearchable
    {
        public string MemberId { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MemberSince { get; set; }
        private List<Book> _borrowedBooks = new List<Book>();

        public Member(string memberId, string name, string email)
        {
            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = DateTime.Now;
        }

        public void AddBorrowedBook(Book book)
        {
            _borrowedBooks.Add(book);
        }

        public void RemoveBorrowedBook(Book book)
        {
            _borrowedBooks.Remove(book);
        }

        public List<Book> GetBorrowedBooks()
        {
            return _borrowedBooks;
        }

        public string GetInfo()
        {
            return $"{Name} (ID: {MemberId}) - Email: {Email} - Medlem sedan: {MemberSince:yyyy-MM-dd} - Lånade böcker: {_borrowedBooks.Count}";
        }

        public bool Matches(string searchTerm)
        {
            string term = searchTerm.ToLower();
            return Name.ToLower().Contains(term) ||
                   Email.ToLower().Contains(term) ||
                   MemberId.ToLower().Contains(term);
        }
    }
}