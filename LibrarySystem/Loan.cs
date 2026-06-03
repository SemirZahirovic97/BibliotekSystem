namespace LibrarySystem
{
    public class Loan
    {
        public Book Book { get; }
        public Member Member { get; }
        public DateTime LoanDate { get; }
        public DateTime DueDate { get; }
        public DateTime? ReturnDate { get; private set; }

        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;
        public bool IsReturned => ReturnDate.HasValue;

        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            Book = book;
            Member = member;
            LoanDate = loanDate;
            DueDate = dueDate;
        }

        public void Return()
        {
            ReturnDate = DateTime.Now;
        }

        public string GetInfo()
        {
            string status = IsReturned ? $"Återlämnad {ReturnDate:yyyy-MM-dd}" :
                           IsOverdue ? "FÖRSENAD" : "Aktiv";
            return $"{Book.Title} lånad av {Member.Name} - Förfaller: {DueDate:yyyy-MM-dd} - Status: {status}";
        }
    }
}
