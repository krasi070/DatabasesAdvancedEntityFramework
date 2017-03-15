namespace BookShopSystem.Client
{
    using System;
    using System.Linq;
    using Data;
    using System.Data.Entity;
    using Migrations;

    public class Strartup
    {
        public static void Main()
        {
            //var migrationStrategy = new MigrateDatabaseToLatestVersion<BookShopContext, Configuration>();
            //Database.SetInitializer(migrationStrategy);

            BookShopContext context = new BookShopContext();        
            //context.Database.Initialize(true);

            var books = context.Books
                .Take(3)
                .ToList();
            books[0].RelatedBooks.Add(books[1]);
            books[1].RelatedBooks.Add(books[0]);
            books[0].RelatedBooks.Add(books[2]);
            books[2].RelatedBooks.Add(books[0]);

            context.SaveChanges();

            var booksFromQuery = context.Books.Take(3);
            foreach (var book in booksFromQuery)
            {
                Console.WriteLine("--{0}", book.Title);
                foreach (var relatedBook in book.RelatedBooks)
                {
                    Console.WriteLine(relatedBook);
                }
            }
        }
    }
}