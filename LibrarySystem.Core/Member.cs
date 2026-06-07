namespace LibrarySystem.Core
{
    public class Member : ISearchable
    {
        public int Id { get; set; }
        public string MemberId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime MemberSince { get; set; } = DateTime.Now;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public Member() { }

        public Member(string memberId, string name, string email)
        {
            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = DateTime.Now;
        }

        public string GetInfo()
        {
            return $"{Name} (ID: {MemberId}) - Email: {Email} - Medlem sedan: {MemberSince:yyyy-MM-dd}";
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