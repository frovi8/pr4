using System; 

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