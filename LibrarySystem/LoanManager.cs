namespace LibrarySystem
{
    public class LoanManager
    {
        private List<Loan> _loans = new List<Loan>();

        public Loan CreateLoan(Book book, Member member)
        {
            if (!book.IsAvailable)
                throw new InvalidOperationException($"Boken '{book.Title}' är inte tillgänglig.");

            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));
            book.IsAvailable = false;
            member.AddBorrowedBook(book);
            _loans.Add(loan);
            return loan;
        }

        public void ReturnBook(Book book, Member member)
        {
            var loan = _loans.FirstOrDefault(l => l.Book == book && !l.IsReturned);
            if (loan == null)
                throw new InvalidOperationException($"Inget aktivt lån hittades för '{book.Title}'.");

            loan.Return();
            book.IsAvailable = true;
            member.RemoveBorrowedBook(book);
        }

        public List<Loan> GetActiveLoans()
        {
            return _loans.Where(l => !l.IsReturned).ToList();
        }

        public List<Loan> GetOverdueLoans()
        {
            return _loans.Where(l => l.IsOverdue).ToList();
        }

        public List<Loan> GetAllLoans()
        {
            return _loans;
        }

        public Member? GetMostActiveBorrower(MemberRegistry registry)
        {
            var members = registry.GetAllMembers();
            if (!members.Any())
                return null;

            return members.OrderByDescending(m =>
                _loans.Count(l => l.Member == m)).FirstOrDefault();
        }
    }
}