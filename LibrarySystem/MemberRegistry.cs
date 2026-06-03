namespace LibrarySystem
{
    public class MemberRegistry
    {
        private List<Member> _members = new List<Member>();

        public void AddMember(Member member)
        {
            _members.Add(member);
        }

        public List<Member> GetAllMembers()
        {
            return _members;
        }

        public Member? GetMemberById(string memberId)
        {
            return _members.FirstOrDefault(m => m.MemberId == memberId);
        }

        public List<Member> SearchMembers(string searchTerm)
        {
            return _members.Where(m => m.Matches(searchTerm)).ToList();
        }
    }
}
