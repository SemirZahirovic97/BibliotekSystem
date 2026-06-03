# Bibliotekssystem

Ett konsolbaserat bibliotekssystem byggt i C# som hanterar böcker, medlemmar och utlåning.

## Funktioner
- Visa och söka böcker
- Låna och returnera böcker
- Medlemshantering
- Statistik över utlåning

## Hur man kör projektet
1. Klona projektet: `git clone https://github.com/SemirZahirovic97/BibliotekSystem.git`
2. Öppna `LibrarySystem.sln` i Visual Studio
3. Sätt `LibrarySystem` som startprojekt
4. Tryck F5 för att köra

## Hur man kör testerna
1. Öppna Test Explorer: **Test → Run All Tests**
2. Alla tester ska vara gröna

## Projektstruktur
- `Book.cs` - Bokklass med ISearchable
- `Member.cs` - Medlemsklass med ISearchable
- `Loan.cs` - Låneklass med IsOverdue och IsReturned
- `BookCatalog.cs` - Hanterar böcker
- `MemberRegistry.cs` - Hanterar medlemmar
- `LoanManager.cs` - Hanterar utlåning
- `Library.cs` - Huvudklass med komposition
- `Program.cs` - Konsolgränssnitt

## Designbeslut
 Jag valde komposition (Alternativ B) framför arv eftersom det ger bättre separation 
av ansvar och är lättare att testa och underhålla.