using YourNamespace.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace YourNamespace.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books = new List<Book>();
        private readonly IMemoryCache _cache;
        private const string CacheKey = "Books";

        public BookService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Book> GetBooks()
        {
            if (!_cache.TryGetValue(CacheKey, out List<Book> books))
            {
                books = _books;
                _cache.Set(CacheKey, books);
            }
            return books;
        }

        public Book GetBookById(string id)
        {
            return GetBooks().FirstOrDefault(b => b.Id == id);
        }

        public void CreateBook(Book book)
        {
            book.Id = Guid.NewGuid().ToString();

            if (!_cache.TryGetValue(CacheKey, out List<Book> books))
            {
                books = _books;
                _cache.Set(CacheKey, books);
            }

            _books.Add(book);
            _cache.Set(CacheKey, _books);
        }

        public void UpdateBook(Book book)
        {
            if (!_cache.TryGetValue(CacheKey, out List<Book> books))
            {
                books = _books;
                _cache.Set(CacheKey, books);
            }

            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.Description = book.Description;
                _cache.Set(CacheKey, _books);
            }
        }

        public void DeleteBook(string id)
        {
            if (!_cache.TryGetValue(CacheKey, out List<Book> books))
            {
                books = _books;
                _cache.Set(CacheKey, books);
            }
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
                _cache.Set(CacheKey, _books);
            }
        }
    }
}