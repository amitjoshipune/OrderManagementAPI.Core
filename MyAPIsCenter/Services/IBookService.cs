using YourNamespace.Models;
using System.Collections.Generic;

namespace YourNamespace.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(string id);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(string id);
    }
}