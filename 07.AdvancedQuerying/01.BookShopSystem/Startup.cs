namespace _01.BookShopSystem
{
    using System;
    using System.Linq;
    using Data;
    using Models;
    using System.Data.SqlClient;
    using System.Data;

    public class Startup
    {
        public static void Main()
        {
            PrintNumberOfBooksByAuthor();
        }

        // Problem 01 - Books Titles by Age Restriction
        private static void PrintBoooksByAgeRestriction()
        {
            using (var context = new BookShopContext())
            {
                Console.Write("Age restriction: ");
                string ageRestriction = Console.ReadLine().ToLower();
                var books = context.Books
                    .Where(b => b.AgeRestriction.ToString().ToLower() == ageRestriction)
                    .Select(b => b.Title)
                    .ToList();

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            } 
        }

        // Problem 02 - Golden Books
        private static void PrintGoldenBooks()
        {
            using (var context = new BookShopContext())
            {
                var books = context.Books
                    .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                    .OrderBy(b => b.Id)
                    .Select(b => b.Title);

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }

        // Problem 03 - Books by Price
        private static void PrintBooksByPrice()
        {
            using (var context = new BookShopContext())
            {
                var books = context.Books
                    .Where(b => b.Price < 5 || b.Price > 40)
                    .OrderBy(b => b.Id)
                    .Select(b => new
                    {
                        b.Title,
                        b.Price
                    });

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} - ${book.Price:F2}");
                }
            }
        }

        // Problem 04 - Not Released Books
        private static void PrintNotReleasedBooks()
        {
            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine());
            using (var context = new BookShopContext())
            {
                var books = context.Books
                    .Where(b => b.ReleaseDate.Year != year)
                    .OrderBy(b => b.Id)
                    .Select(b => b.Title);

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }

        // Problem 05 - Book Titles by Category
        private static void PrintBookTitlesByCategory()
        {
            Console.Write("Categories: ");
            string[] categories = Console.ReadLine().ToLower()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            using (var context = new BookShopContext())
            {
                var books = context.Books
                    .Where(b => categories
                        .Any(c => b.Categories
                            .Select(c1 => c1.Name.ToLower()).Contains(c)))
                    .OrderBy(b => b.Id)
                    .Select(b => b.Title);

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }

        // Problem 06 - Books Released Before Date
        private static void PrintBooksReleasedBeforeDate()
        {
            Console.Write("Date: ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            using (var context = new BookShopContext())
            {
                var books = context.Books
                    .Where(b => b.ReleaseDate.CompareTo(date) < 0)
                    .Select(b => new
                    {
                        b.Title,
                        b.EditionType,
                        b.Price
                    });

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} - {book.EditionType} - {book.Price:F2}");
                }
            }
        }

        // Problem 07 - Authors Search
        private static void PrintAuthorsNames()
        {
            Console.Write("Name ends in: ");
            string str = Console.ReadLine();
            using (var context = new BookShopContext())
            {
                var authors = context.Authors
                    .Where(a => a.FirstName.ToLower().EndsWith(str.ToLower()))
                    .Select(a => a.FirstName + " " + a.LastName);

                foreach (var author in authors)
                {
                    Console.WriteLine(author);
                }
            }
        }

        // Problem 08 - Books Search
        private static void PrintSearchedBooks()
        {
            Console.Write("Book: ");
            string str = Console.ReadLine();
            using (var context = new BookShopContext())
            {
                var books = context.Database
                    .SqlQuery<string>("USE BookShop; SELECT Title FROM Books WHERE Title LIKE {0}", "%" + str + "%")
                    .ToList();

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }

        // Problem 09 - Book Titles Search
        private static void PrintBooksByAuthor()
        {
            Console.Write("Last name starts with: ");
            string str = Console.ReadLine();
            using (var context = new BookShopContext())
            {
                var books = context.Books
                    .Where(b => b.Author.LastName.ToLower().StartsWith(str.ToLower()))
                    .OrderBy(b => b.Id)
                    .Select(b => new
                    {
                        b.Title,
                        Author = b.Author.FirstName + " " + b.Author.LastName
                    });

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} ({book.Author})");
                }
            }
        }

        // Problem 10 - Count Books
        private static void PrintBookCount()
        {
            Console.Write("Title length: ");
            int length = int.Parse(Console.ReadLine());
            using (var context = new BookShopContext())
            {
                Console.WriteLine(context.Books
                    .Where(b => b.Title.Length > length)
                    .Count());
            }
        }

        // Problem 11 - Total Book Copies
        private static void PrintTotalBookCopies()
        {
            using (var context = new BookShopContext())
            {
                var authors = context.Authors
                    .Select(a => new
                    {
                        Author = a.FirstName + " " + a.LastName,
                        Copies = a.Books.Select(b => b.Copies).Sum()
                    })
                    .OrderByDescending(a => a.Copies);
                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.Author} - {author.Copies}");
                }
            }
        }

        // Problem 12 - Find Profit
        private static void PrintProfitByCategories()
        {
            using (var context = new BookShopContext())
            {
                var categories = context.Categories
                    .Select(c => new
                    {
                        c.Name,
                        Profit = c.Books.Select(b => b.Copies * b.Price).Sum()
                    })
                    .OrderByDescending(a => a.Profit);
                foreach (var category in categories)
                {
                    Console.WriteLine($"{category.Name} - ${category.Profit:F2}");
                }
            }
        }

        // Problem 13 - Most Recent Books
        private static void PrintMostRecentBooksByCategories()
        {
            using (var context = new BookShopContext())
            {
                var categories = context.Categories
                    .Select(c => new
                    {
                        c.Name,
                        Books = c.Books
                            .OrderByDescending(b => b.ReleaseDate)
                            .ThenBy(b => b.Title)
                            .Take(3),
                        TotalBookCount = c.Books.Count()
                    })
                    .Where(c => c.TotalBookCount > 35)
                    .OrderByDescending(c => c.TotalBookCount);
                foreach (var category in categories)
                {
                    Console.WriteLine($"--{category.Name}: {category.TotalBookCount} books");
                    foreach (var book in category.Books)
                    {
                        Console.WriteLine($"{book.Title} ({book.ReleaseDate.Year})");
                    }
                }
            }
        }

        // Problem 14 - Increase Book Copies
        private static void PrintNumberOfCopiesAdded()
        {
            var date = new DateTime(2013, 6, 6);
            using (var context = new BookShopContext())
            {
                int count = 0;
                context.Books
                    .Where(b => b.ReleaseDate.CompareTo(date) > 0)
                    .ToList()
                    .ForEach(b =>
                    {
                        b.Copies += 44;
                        count += 44;
                    });

                context.SaveChanges();

                Console.WriteLine(count);
            }
        }

        // Problem 15 - Remove Books
        private static void PrintNumberOfDeletedBooks()
        {
            using (var context = new BookShopContext())
            {
                var books = context.Books
                    .Where(b => b.Copies < 4200)
                    .ToList();

                context.Books.RemoveRange(books);

                context.SaveChanges();

                Console.WriteLine(books.Count());
            }
        }

        // Problem 16 - Stored Procedure
        // CREATE PROCEDURE usp_GetAuthorBooksCount
        //     @FirstName nvarchar(MAX), 
        //	   @LastName nvarchar(MAX),
        //	   @Result int OUT
        // AS
        // BEGIN
        //     SET @Result = (
        //         SELECT COUNT(*) AS COUNT
        //         FROM Authors AS a
        //         INNER JOIN Books AS b
        //         ON b.AuthorId = a.Id
        //         WHERE a.FirstName = @FirstName AND a.LastName = @LastName)
        // END
        private static void PrintNumberOfBooksByAuthor()
        {
            Console.Write("First name: ");
            SqlParameter firstName = new SqlParameter("@FirstName", Console.ReadLine());
            Console.Write("Last name: ");
            SqlParameter lastName = new SqlParameter("@LastName", Console.ReadLine());
            SqlParameter result = new SqlParameter();
            result.ParameterName = "@Result";
            result.Direction = ParameterDirection.Output;
            result.SqlDbType = SqlDbType.Int;
            using (var context = new BookShopContext())
            {
                context.Database
                    .ExecuteSqlCommand("EXECUTE usp_GetAuthorBooksCount @FirstName, @LastName, @Result output", result, firstName, lastName);
                Console.WriteLine($"{firstName.Value} {lastName.Value} has written {result.Value} books");
            }
        }
    }
}