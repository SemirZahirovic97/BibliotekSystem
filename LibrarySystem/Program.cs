namespace LibrarySystem
{
    class Program
    {
        static Library library = new Library();

        static void Main(string[] args)
        {
            SeedData();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Bibliotekssystem ===");
                Console.WriteLine("1. Visa alla böcker");
                Console.WriteLine("2. Sök bok");
                Console.WriteLine("3. Låna bok");
                Console.WriteLine("4. Returnera bok");
                Console.WriteLine("5. Visa medlemmar");
                Console.WriteLine("6. Statistik");
                Console.WriteLine("0. Avsluta");
                Console.Write("\nVälj: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowAllBooks();
                        break;
                    case "2":
                        SearchBook();
                        break;
                    case "3":
                        BorrowBook();
                        break;
                    case "4":
                        ReturnBook();
                        break;
                    case "5":
                        ShowMembers();
                        break;
                    case "6":
                        library.GetStatistics();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Avslutar...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        static void ShowAllBooks()
        {
            var books = library.GetAllBooks();
            Console.WriteLine("\n=== Alla böcker ===");
            if (!books.Any())
            {
                Console.WriteLine("Inga böcker finns.");
                return;
            }
            for (int i = 0; i < books.Count; i++)
                Console.WriteLine($"{i + 1}. {books[i].GetInfo()}");
        }

        static void SearchBook()
        {
            Console.Write("\nSökterm: ");
            string? term = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(term))
            {
                Console.WriteLine("Ogiltig sökterm.");
                return;
            }

            var results = library.SearchBooks(term);
            Console.WriteLine($"\nSökresultat för '{term}':");
            if (!results.Any())
            {
                Console.WriteLine("Inga böcker hittades.");
                return;
            }
            for (int i = 0; i < results.Count; i++)
                Console.WriteLine($"{i + 1}. {results[i].GetInfo()}");
        }

        static void BorrowBook()
        {
            Console.Write("\nAnge ISBN: ");
            string? isbn = Console.ReadLine();
            Console.Write("Ange medlems-ID: ");
            string? memberId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(memberId))
            {
                Console.WriteLine("Ogiltiga uppgifter.");
                return;
            }

            try
            {
                var loan = library.BorrowBook(isbn, memberId);
                Console.WriteLine($"\nBoken \"{loan.Book.Title}\" har lånats ut till {loan.Member.Name}.");
                Console.WriteLine($"Återlämningsdatum: {loan.DueDate:yyyy-MM-dd}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }

        static void ReturnBook()
        {
            Console.Write("\nAnge ISBN på boken som ska returneras: ");
            string? isbn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("Ogiltigt ISBN.");
                return;
            }

            try
            {
                library.ReturnBook(isbn);
                Console.WriteLine("Boken har returnerats.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }

        static void ShowMembers()
        {
            var members = library.GetAllMembers();
            Console.WriteLine("\n=== Medlemmar ===");
            if (!members.Any())
            {
                Console.WriteLine("Inga medlemmar finns.");
                return;
            }
            foreach (var member in members)
                Console.WriteLine(member.GetInfo());
        }

        static void SeedData()
        {
            library.AddBook(new Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954));
            library.AddBook(new Book("978-91-0-012346-3", "Hobbiten", "J.R.R. Tolkien", 1937));
            library.AddBook(new Book("978-91-0-012347-0", "Harry Potter och de vises sten", "J.K. Rowling", 1997));
            library.AddBook(new Book("978-91-0-012348-7", "Brott och straff", "Fjodor Dostojevskij", 1866));
            library.AddBook(new Book("978-91-0-012349-4", "1984", "George Orwell", 1949));

            library.AddMember(new Member("M001", "Anna Andersson", "anna@email.com"));
            library.AddMember(new Member("M002", "Erik Eriksson", "erik@email.com"));
            library.AddMember(new Member("M003", "Maria Nilsson", "maria@email.com"));
        }
    }
}