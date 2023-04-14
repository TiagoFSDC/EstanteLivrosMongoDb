using EstanteLivrosMongoDb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {
        MongoClient mongo = new MongoClient("mongodb://localhost:27017");
        var database = mongo.GetDatabase("EstanteLivro");
        var collection = database.GetCollection<BsonDocument>("Livros");

        List<Book> books = new List<Book>();

        int op = 0;

        do
        {
            op = Menu(op);
            switch (op)
            {
                case 1:
                    collection.InsertOne(CreateBook());
                    break;
                case 2:
                    collection.DeleteOne(FindBook());
                    break;
                case 3:
                    MenuEdit(collection);
                    break;
                case 4:
                    PrintBooks(collection);
                    break;
                case 5:
                    PrintAllBooks(collection);
                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        } while (op != 6); 
    }

    private static void PrintBooks(IMongoCollection<BsonDocument> collection)
    {
        Console.WriteLine("Informe o titulo do livro que será mostrado");
        string t = Console.ReadLine();

        var filter = Builders<BsonDocument>.Filter.Eq("Titulo", t);
        var b = collection.Find(filter).FirstOrDefault();
        var books = BsonSerializer.Deserialize<Book>(b);
        Console.WriteLine("\n" + books.ToString());
    }

    private static void PrintAllBooks(IMongoCollection<BsonDocument> collection)
    {
        var b = collection.Find(new BsonDocument()).ToList();
        var books = b.Where(x => BsonSerializer.Deserialize<Book>(x).Title != null).ToList();
        books.ForEach(x => Console.WriteLine(x.ToString()));
    }
    private static void MenuEdit(IMongoCollection<BsonDocument> collection)
    {
        int option = 0;
        do
        {
            option = SubMenu();
            switch (option) 
            {
                default:
                    Console.WriteLine("Opção invalida!!");
                    break;
                case 1:
                    EditTitle(collection);
                    break;
                case 2:
                    EditISBN(collection);
                    break;
                case 3:
                    EditEdition(collection);
                    break;
                case 4:
                    EditYear(collection);
                    break;
                case 5:
                    EditStatus(collection);
                    break;
                case 6:
                    EditAuthor(collection);
                    break;
                case 7:
                    break;
            }
        } while (option != 7);
    }

    private static void EditTitle(IMongoCollection<BsonDocument> collection)
    {
        var book = FindBook();
        Console.WriteLine("Digite o novo titulo: ");
        string t = Console.ReadLine();
        var update = Builders<BsonDocument>.Update.Set("Titulo", t);
        collection.UpdateOne(book,update);
    }

    private static void EditISBN(IMongoCollection<BsonDocument> collection)
    {
        var book = FindBook();
        Console.WriteLine("Digite o novo isbn: ");
        string i = Console.ReadLine();
        var update = Builders<BsonDocument>.Update.Set("ISBN", i);
        collection.UpdateOne(book, update);
    }
    private static void EditEdition(IMongoCollection<BsonDocument> collection)
    {
        var book = FindBook();
        Console.WriteLine("Digite a nova edição: ");
        string e = Console.ReadLine();
        var update = Builders<BsonDocument>.Update.Set("Edicao", e);
        collection.UpdateOne(book, update);
    }

    private static void EditYear(IMongoCollection<BsonDocument> collection)
    {
        var book = FindBook();
        Console.WriteLine("Digite o novo ano: ");
        string a = Console.ReadLine();
        var update = Builders<BsonDocument>.Update.Set("Ano", a);
        collection.UpdateOne(book, update);
    }

    private static void EditAuthor(IMongoCollection<BsonDocument> collection)
    {
        var book = FindBook();
        Console.WriteLine("Digite o novo nome do autor: ");
        string a = Console.ReadLine();
        var update = Builders<BsonDocument>.Update.Set("Autores", a);
        collection.UpdateOne(book, update);
    }
    private static void EditStatus(IMongoCollection<BsonDocument> collection)
    {
        var book = FindBook();
        Console.WriteLine("Digite o novo status do livro: ");
        string s = Console.ReadLine();
        var update = Builders<BsonDocument>.Update.Set("Status", s);
        collection.UpdateOne(book, update);
    }

    private static FilterDefinition<BsonDocument> FindBook()
    {
        Console.WriteLine("Digite o nome completo do livro que sera modificado ou excluido: ");
        string title = Console.ReadLine();
        var filtro = Builders<BsonDocument>.Filter.Eq("Titulo", title);

        return filtro;
    }

    private static int SubMenu()
    {
        Console.WriteLine("Menu de opções para edição dos livros");
        Console.WriteLine("1 - Editar o nome do livro");
        Console.WriteLine("2 - Editar o ISBN do livro");
        Console.WriteLine("3 - Editar a Edição do livro");
        Console.WriteLine("4 - Editar o ano de lançamento do livro");
        Console.WriteLine("5 - Editar o Status do livro");
        Console.WriteLine("6- Editar o autor do liro");
        Console.WriteLine("7 - Voltar ao menu inicial");
        Console.WriteLine("Escolha umas das opções: ");
        int option = int.Parse(Console.ReadLine());
        return option;
    }

    private static BsonDocument CreateBook()
    {
        Book book = new Book();

        Console.WriteLine("Digite o nome do livro:");
        book.Title = Console.ReadLine();
        Console.WriteLine("Digite o ISBN do livro:");
        book.ISBN = Console.ReadLine();
        Console.WriteLine("Digite a Edição do livro:");
        book.Edition = Console.ReadLine();
        Console.WriteLine("Digite o Ano de lançamento do livro:");
        book.Year = Console.ReadLine();
        Console.WriteLine("Digite o nome do autor:");
        book.Name = Console.ReadLine();
        Console.WriteLine("Digite o status do livro: ");
        book.Status = Console.ReadLine();

        var newbook = new BsonDocument
        {
            {"Titulo", book.Title},
            {"ISBN", book.ISBN},
            {"Edicao", book.Edition},
            {"Ano", book.Year},
            {"Autores", book.Name},
            {"Status", book.Status},
        };
        return newbook;
    }

    private static int Menu(int option)
    {
        Console.WriteLine("Lista de opções: ");
        Console.WriteLine("1 - Adicionar livros");
        Console.WriteLine("2 - Excluir livros");
        Console.WriteLine("3 - Editar livros");
        Console.WriteLine("4 - Mostrar um livro");
        Console.WriteLine("5 - Mostrar todos os livros");
        Console.WriteLine("6 - Sair do programa");
        Console.Write("Escolha uma das opções: ");
        option = int.Parse(Console.ReadLine());
        return option;
    }
}