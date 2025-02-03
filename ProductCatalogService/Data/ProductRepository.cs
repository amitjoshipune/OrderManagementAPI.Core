using CommonServicesLib.Contracts;
using CommonServicesLib.Models;

namespace ProductCatalogService.Data
{
    public class ProductRepository :IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllProducts()
        {
            return _context.Books.ToList();
        }

        public Book GetProductById(string id)
        {
            return _context.Books.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Book product)
        {
            _context.Books.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Book product)
        {
            _context.Books.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(string id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                _context.Books.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
