namespace LibrarySystem.Core
{
    public class Loan
    {
        public int Id { get; set; }
        public Book Book { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;
        public bool IsReturned => ReturnDate.HasValue;

        public Loan() { }

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