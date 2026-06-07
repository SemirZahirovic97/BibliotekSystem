namespace LibrarySystem.Core
{
    public interface ISearchable
    {
        bool Matches(string searchTerm);
    }
}