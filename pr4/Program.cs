using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;
using System.ComponentModel;

namespace pr4
{
    enum Genre
    {
        Фэнтези,
        Детектив,
        Роман
    }
    internal class Book
    {
        private static int NextId = 1;
        public int Id { get; private set; }
        public string Title { get; init; }
        public string Author { get; init; }
        public Genre Genre { get; init; }
        public int Year { get; init; }
        public decimal Price { get; init; }

        public Book(string title, string author, Genre genre, int year, decimal price)
        {
            Id = NextId++;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        public void Print()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Название: {Title}");
            Console.WriteLine($"Автор: {Author}");
            Console.WriteLine($"Жанр: {Genre}");
            Console.WriteLine($"Год: {Year}");
            Console.WriteLine($"Цена: {Price}");
        }
    }
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
            books.Add(new Book("Парампарам", "Иванов", Genre.Фэнтези, 2000, 100));
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
                        break;
                    case "2":
                        DelBook();
                        break;
                    case "3":
                        SearchBook();
                        break;
                    case "4":
                        SortBooks();
                        break;
                    case "5":
                        MaxAndMinBook();
                        break;
                    case "6":
                        GroupByAuthor();
                        break;
                    case "7":
                        PrintBooks();
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

                if (int.TryParse(input, out int value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Введите целочисленное значение!");
                }
            }
        }
        static decimal CheckDecimal(string text)
        {
            while (true)
            {
                Console.Write(text);
                string? input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Введите число!");
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

        }

        static void MaxAndMinBook()
        {

        }

        static void GroupByAuthor()
        {

        }
    }
}