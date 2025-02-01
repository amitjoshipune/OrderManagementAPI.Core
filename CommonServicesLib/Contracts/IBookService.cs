using CommonServicesLib.Models;

namespace CommonServicesLib.Contracts
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
