using GraphQL_APIs.Types;

namespace GraphQL_APIs.Schema;

public class Query
{
    public string Hello() => "Hello, World!";
    
    public List<Book> GetBooks() => new ()
    {
        new Book { Title = "Book 1", Author = "Author 1" },
        new Book { Title = "Book 2", Author = "Author 2" }
    };
}