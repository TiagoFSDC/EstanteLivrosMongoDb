using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EstanteLivrosMongoDb
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Titulo")]
        public string Title { get; set; }
        [BsonElement("ISBN")]
        public string ISBN { get; set; }
        [BsonElement("Edicao")]
        public string Edition { get; set; }
        [BsonElement("Ano")]
        public string Year { get; set; }
        [BsonElement("Autores")]
        public string Name { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; }

        public Book()
        {
        }
        public Book(string n, string i, string e,string y, string s)
        {
            this.Title = n;
            this.ISBN = i;
            this.Edition = e;
            this.Status = s;
        }

        public override string ToString()
        {
            return $"Nome: {Title} \nISBN: {ISBN}\nEdição: {Edition}\nAno: {Year}\nNome do autor: {Name}\nStatus do livro: {Status}";
        }
    }
}
