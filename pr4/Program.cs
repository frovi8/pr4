using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;
using System.ComponentModel;

namespace pr4
{
    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            AddBaseBook();
            Start();
        }

        static void AddBaseBook()
        {
            books.Add(new Book("Парампарам", "Толкин", Genre.Фэнтези, 2000, 100));
            books.Add(new Book("Властелин колец", "Толкин", Genre.Фэнтези, 1954, 1200));
            books.Add(new Book("Убийство в Восточном экспрессе", "Агата Кристи", Genre.Детектив, 1934, 450));
            books.Add(new Book("Гордость и предубеждение", "Джейн Остин", Genre.Роман, 1813, 600));
            books.Add(new Book("Гарри Поттер и философский камень", "Джоан Роулинг", Genre.Фэнтези, 1997, 800));
        }

        static void Start()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("Добавить книгу - 1");
                Console.WriteLine("Удалить книгу - 2");
                Console.WriteLine("Найти книгу - 3");
                Console.WriteLine("Отсортировать книги - 4");
                Console.WriteLine("Самую дорогая и самая дешёвая книга - 5");
                Console.WriteLine("Сгруппировать по авторам - 6");
                Console.WriteLine("Список всех книг - 7");
                Console.WriteLine("Выйти - 0");
                Console.Write("Ваш выбор: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddBook();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        DelBook();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        SearchBook();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        SortBooks();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "5":
                        MaxAndMinBook();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "6":
                        GroupByAuthor();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "7":
                        PrintBooks();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("poka");
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод!");
                        break;
                }
            }
        }

        static string CheckString(string text)
        {
            while (true)
            {
                Console.Write(text);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Надо ввести хоть что то");
                }
                else
                {
                    return input;
                }
            }
        }

        static int CheckInt(string text)
        {
            while (true)
            {
                Console.Write(text);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value > 0)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Введите целочисленное неотрицательное значение!");
                }
            }
        }
        static decimal CheckDecimal(string text)
        {
            while (true)
            {
                Console.Write(text);
                string? input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal value) && value > 0)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Введите неотрицательное число!");
                }
            }
        }

        static Genre CheckEnum(string text)
        {
            while (true)
            {
                int input = CheckInt(text);

                switch (input)
                {
                    case 1:
                        return Genre.Фэнтези;
                    case 2:
                        return Genre.Детектив;
                    case 3:
                        return Genre.Роман;
                    default:
                        Console.WriteLine("Введите цифру от 1 до 3!");
                        break;
                }
            }
        }

        static void AddBook()
        {
            string title = CheckString("Введите название: ");
            string author = CheckString("Введите автора: ");
            Genre genre = CheckEnum("Введите жанр (1 - Фэнтези, 2 - Детектив, 3 - Роман): ");
            int year = CheckInt("Введите год: ");
            decimal price = CheckDecimal("Введите цену: ");

            books.Add(new Book(title, author, genre, year, price));
        }

        static void DelBook()
        {
            int input = CheckInt("Введите идентификатор для удаления: ");

            foreach (var b in books)
            {
                if (b.Id == input)
                {
                    books.Remove(b);
                    Console.WriteLine("Книга удалена");
                    return;
                }
            }

            Console.WriteLine("Книга не найдена");
        }


        static void SearchBook()
        {
            Console.WriteLine("Поиск по названию - 1");
            Console.WriteLine("Поиск по автору - 2");
            Console.WriteLine("Поиск по жанру - 3");
            int input = CheckInt("По какому параметру хотите искать: ");

            switch (input)
            {
                case 1:
                    SearchByTitle();
                    break;
                case 2:
                    SearchByAuthor();
                    break;
                case 3:
                    SearchByGenre();
                    break;
                default:
                    Console.WriteLine("Некорректный ввод!");
                    break;
            }
        }

        static void SearchByTitle()
        {
            string input = CheckString("Введите название: ");

            var foundBooks = books.Where(x => x.Title.Contains(input, StringComparison.InvariantCultureIgnoreCase)).ToList();

            if (foundBooks.Count == 0)
            {
                Console.WriteLine("Книги с таким названием не найдено");
                return;
            }

            foreach (var istit in foundBooks)
            {
                istit.Print();
                Console.WriteLine();
            }
        }

        static void SearchByAuthor()
        {
            string input = CheckString("Введите автора: ");

            var foundAuthors = books.Where(x => x.Author.Split(' ', StringSplitOptions.RemoveEmptyEntries).Any(x => x.Contains(input, StringComparison.InvariantCultureIgnoreCase))).ToList();

            if (foundAuthors.Count == 0)
            {
                Console.WriteLine("Книги с таким автором не найдено");
                return;
            }

            foreach (var b in foundAuthors)
            {
                b.Print();
            }
        }

        static void SearchByGenre()
        {
            Genre input = CheckEnum("Введите жанр (1 - Фэнтези, 2 - Детектив, 3 - Роман): ");

            var foundGenre = books.Where(x => x.Genre == input);

            foreach (var b in foundGenre)
            {
                b.Print();
            }
        }

        static void PrintBooks()
        {
            foreach (var b in books)
            {
                b.Print();
                Console.WriteLine();
            }
        }

        static void SortBooks()
        {
            Console.WriteLine("По названию - 1");
            Console.WriteLine("По году - 2");
            Console.WriteLine("Выход - 0");
            int input = CheckInt("Как сортировать: ");

            switch (input)
            {
                case 1:
                    var sortByTitle = books.OrderBy(u => u.Title);
                    foreach (var b in sortByTitle) b.Print();
                    break;
                case 2:
                    var sortByYear = books.OrderBy(u => u.Year);
                    foreach (var b in sortByYear) b.Print();
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("1 или 2 ток выбор!");
                    break;
            }
        }

        static void MaxAndMinBook()
        {
            var min = books.Min(u => u.Price);
            Console.WriteLine($"Минимальная цена: {min}");

            var max = books.Max(u => u.Price);
            Console.WriteLine($"Максимальная цена: {max}");
        }

        static void GroupByAuthor()
        {
            var group = books.GroupBy(u => u.Author);

            foreach (var author in group)
            {
                Console.WriteLine($"{author.Key} - {author.Count()}");
            }
        }
    }
}