using CommonServicesLib.Contracts;
using CommonServicesLib.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CommonServicesLib.Services
{
    public class BookService : IBookService
    {
        private readonly IProductRepository _productRepository;

        private readonly List<Book> _books = new List<Book>();
        private readonly IMemoryCache _cache;
        private const string CacheKey = "Books";

        public BookService(IProductRepository productRepository, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        public IEnumerable<Book> GetBooks()
        {
            /*
            if (!_cache.TryGetValue(CacheKey, out List<Book> books))
            {
                books = _books;
                _cache.Set(CacheKey, books);
            }
            //// For time being add some dummy books to test it
            if (books == null || books.Count == 0)
            {
                SeedBooksForTesting().ForEach(book =>
                {
                    CreateBook(book);
                });
            }
            return books;
            */
            var products = _productRepository.GetAllProducts();
            return products.Select(p => new Book { Id = p.Id, Title = p.Title, Price = p.Price, Author = p.Author , Description =p.Description});

        }

        protected List<Book> SeedBooksForTesting()
        {
            var books = new List<Book>();

            for (int x = 1; x <= 10; x++)
            {
                var aBook = new Book()
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = $"Author {x}",
                    Title = $"Book Title {x}",
                    Price = 100 * x,
                    Description = @$"This is great book and it has {x} of stories. 
                                       It can serve as guide. It worth Reading"
                };
                books.Add(aBook);
            }
            return books;
        }

        public Book GetBookById(string id)
        {
            //return GetBooks().FirstOrDefault(b => b.Id == id);
            var product = _productRepository.GetProductById(id);
            return new Book { Id = product.Id, Title = product.Title, Price = product.Price, Author = product.Author, Description = product.Description };
        }

        public void CreateBook(Book book)
        {
            /*
            book.Id = Guid.NewGuid().ToString();

            if (!_cache.TryGetValue(CacheKey, out List<Book> books))
            {
                books = _books;
                _cache.Set(CacheKey, books);
            }

            _books.Add(book);
            _cache.Set(CacheKey, _books);
            */
            var  newBookId = Guid.NewGuid().ToString(); // Automatically generate Id
            var product = new Book { Id = newBookId, Title = book.Title, Price = book.Price ,  Author = book.Author,  Description = book.Description };
            _productRepository.AddProduct(product);

        }

        public void UpdateBook(Book book)
        {
            /*
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
            */

            var product = new Book { Id = book.Id, Title = book.Title, Price = book.Price, Author = book.Author, Description = book.Description };
            _productRepository.UpdateProduct(product);
        }

        public void DeleteBook(string id)
        {
            /*
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
            */
            _productRepository.DeleteProduct(id);
        }
    }
}
