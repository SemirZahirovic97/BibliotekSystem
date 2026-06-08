using LibrarySystem.Core;
using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;
using Book = LibrarySystem.Core.Book;
using Member = LibrarySystem.Core.Member;
using Loan = LibrarySystem.Core.Loan;

namespace LibrarySystem.Tests
{
    public class BookRepositoryTests
    {
        private LibraryContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveBookToDatabase()
        {
            // Arrange
            using var context = CreateContext("AddBook");
            var repository = new BookRepository(context);
            var book = new Book("123", "Testbok", "Testförfattare", 2024);

            // Act
            await repository.AddAsync(book);

            // Assert
            var savedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "123");
            Assert.NotNull(savedBook);
            Assert.Equal("Testbok", savedBook.Title);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBooks()
        {
            // Arrange
            using var context = CreateContext("GetAllBooks");
            var repository = new BookRepository(context);
            await repository.AddAsync(new Book("123", "Bok 1", "Författare 1", 2020));
            await repository.AddAsync(new Book("456", "Bok 2", "Författare 2", 2021));

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectBook()
        {
            // Arrange
            using var context = CreateContext("GetById");
            var repository = new BookRepository(context);
            await repository.AddAsync(new Book("123", "Testbok", "Testförfattare", 2024));

            // Act
            var book = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(book);
            Assert.Equal("Testbok", book.Title);
        }

        [Fact]
        public async Task SearchAsync_ShouldFindBooksByTitle()
        {
            // Arrange
            using var context = CreateContext("SearchBooks");
            var repository = new BookRepository(context);
            await repository.AddAsync(new Book("123", "Sagan om ringen", "Tolkien", 1954));
            await repository.AddAsync(new Book("456", "Hobbiten", "Tolkien", 1937));

            // Act
            var result = await repository.SearchAsync("sagan");

            // Assert
            Assert.Single(result);
            Assert.Equal("Sagan om ringen", result.First().Title);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveBook()
        {
            // Arrange
            using var context = CreateContext("DeleteBook");
            var repository = new BookRepository(context);
            await repository.AddAsync(new Book("123", "Testbok", "Testförfattare", 2024));

            // Act
            await repository.DeleteAsync(1);

            // Assert
            var result = await repository.GetAllAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateBook()
        {
            // Arrange
            using var context = CreateContext("UpdateBook");
            var repository = new BookRepository(context);
            await repository.AddAsync(new Book("123", "Gammal titel", "Testförfattare", 2024));
            var book = await repository.GetByIdAsync(1);

            // Act
            book!.Title = "Ny titel";
            await repository.UpdateAsync(book);

            // Assert
            var updated = await repository.GetByIdAsync(1);
            Assert.Equal("Ny titel", updated!.Title);
        }
    }

    public class MemberRepositoryTests
    {
        private LibraryContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveMemberToDatabase()
        {
            // Arrange
            using var context = CreateContext("AddMember");
            var repository = new MemberRepository(context);
            var member = new Member("M001", "Anna Andersson", "anna@test.com");

            // Act
            await repository.AddAsync(member);

            // Assert
            var savedMember = await context.Members.FirstOrDefaultAsync(m => m.MemberId == "M001");
            Assert.NotNull(savedMember);
            Assert.Equal("Anna Andersson", savedMember.Name);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllMembers()
        {
            // Arrange
            using var context = CreateContext("GetAllMembers");
            var repository = new MemberRepository(context);
            await repository.AddAsync(new Member("M001", "Anna", "anna@test.com"));
            await repository.AddAsync(new Member("M002", "Erik", "erik@test.com"));

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectMember()
        {
            // Arrange
            using var context = CreateContext("GetMemberById");
            var repository = new MemberRepository(context);
            await repository.AddAsync(new Member("M001", "Anna", "anna@test.com"));

            // Act
            var member = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(member);
            Assert.Equal("Anna", member.Name);
        }
    }

    public class LoanRepositoryTests
    {
        private LibraryContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveLoanToDatabase()
        {
            // Arrange
            using var context = CreateContext("AddLoan");
            var bookRepo = new BookRepository(context);
            var memberRepo = new MemberRepository(context);
            var loanRepo = new LoanRepository(context);

            var book = new Book("123", "Testbok", "Testförfattare", 2024);
            var member = new Member("M001", "Anna", "anna@test.com");
            await bookRepo.AddAsync(book);
            await memberRepo.AddAsync(member);

            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act
            await loanRepo.AddAsync(loan);

            // Assert
            var activeLoans = await loanRepo.GetActiveLoansAsync();
            Assert.Single(activeLoans);
        }

        [Fact]
        public async Task GetActiveLoansAsync_ShouldOnlyReturnActiveLoans()
        {
            // Arrange
            using var context = CreateContext("ActiveLoans");
            var bookRepo = new BookRepository(context);
            var memberRepo = new MemberRepository(context);
            var loanRepo = new LoanRepository(context);

            var book = new Book("123", "Testbok", "Testförfattare", 2024);
            var member = new Member("M001", "Anna", "anna@test.com");
            await bookRepo.AddAsync(book);
            await memberRepo.AddAsync(member);

            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));
            await loanRepo.AddAsync(loan);
            loan.Return();
            await loanRepo.UpdateAsync(loan);

            // Act
            var activeLoans = await loanRepo.GetActiveLoansAsync();

            // Assert
            Assert.Empty(activeLoans);
        }
    }
}