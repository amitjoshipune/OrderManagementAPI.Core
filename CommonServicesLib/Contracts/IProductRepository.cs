using CommonServicesLib.Models;

namespace CommonServicesLib.Contracts
{
    public interface IProductRepository
    {
        IEnumerable<Book> GetAllProducts();
        Book GetProductById(string id);
        void AddProduct(Book product);
        void UpdateProduct(Book product);
        void DeleteProduct(string id);
    }
}
