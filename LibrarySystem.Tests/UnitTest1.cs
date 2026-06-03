using LibrarySystem;

namespace LibrarySystem.Tests
{
    public class BookTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

            // Assert
            Assert.Equal("978-91-0-012345-6", book.ISBN);
            Assert.Equal("Testbok", book.Title);
            Assert.Equal("Testförfattare", book.Author);
            Assert.Equal(2024, book.PublishedYear);
        }

        [Fact]
        public void IsAvailable_ShouldBeTrueForNewBook()
        {
            // Arrange & Act
            var book = new Book("123", "Testbok", "Testförfattare", 2024);

            // Assert
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void GetInfo_ShouldReturnFormattedString()
        {
            // Arrange
            var book = new Book("123", "Testbok", "Testförfattare", 2024);

            // Act
            var result = book.GetInfo();

            // Assert
            Assert.Contains("Testbok", result);
            Assert.Contains("Testförfattare", result);
            Assert.Contains("2024", result);
        }
    }

    public class LoanTests
    {
        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenDueDateIsInFuture()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act & Assert
            Assert.False(loan.IsOverdue);
        }

        [Fact]
        public void IsOverdue_ShouldReturnTrue_WhenDueDateHasPassed()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-6));

            // Act & Assert
            Assert.True(loan.IsOverdue);
        }

        [Fact]
        public void IsReturned_ShouldReturnTrue_WhenReturnDateIsSet()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act
            loan.Return();

            // Assert
            Assert.True(loan.IsReturned);
        }
    }

    public class SearchTests
    {
        [Theory]
        [InlineData("Tolkien", true)]
        [InlineData("tolkien", true)]
        [InlineData("Rowling", false)]
        public void Book_Matches_ShouldFindByAuthor(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Sagan", true)]
        [InlineData("sagan", true)]
        [InlineData("Harry Potter", false)]
        public void Book_Matches_ShouldFindByTitle(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }
    }

    public class LibraryStatisticsTests
    {
        [Fact]
        public void GetTotalBooks_ShouldReturnCorrectCount()
        {
            // Arrange
            var library = new Library();
            library.AddBook(new Book("123", "Bok 1", "Författare 1", 2020));
            library.AddBook(new Book("456", "Bok 2", "Författare 2", 2021));
            library.AddBook(new Book("789", "Bok 3", "Författare 3", 2022));

            // Act
            var result = library.GetAllBooks().Count;

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
        {
            // Arrange
            var library = new Library();
            var book1 = new Book("123", "Bok 1", "Författare 1", 2020);
            var book2 = new Book("456", "Bok 2", "Författare 2", 2021);
            var member1 = new Member("M001", "Anna", "anna@test.com");
            var member2 = new Member("M002", "Erik", "erik@test.com");

            library.AddBook(book1);
            library.AddBook(book2);
            library.AddMember(member1);
            library.AddMember(member2);

            library.BorrowBook("123", "M001");
            library.BorrowBook("456", "M001");

            // Act
            var result = library.GetAllMembers()
                .OrderByDescending(m => m.GetBorrowedBooks().Count)
                .FirstOrDefault();

            // Assert
            Assert.Equal("Anna", result?.Name);
        }
    }
}