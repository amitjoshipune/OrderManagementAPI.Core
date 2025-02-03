using CommonServicesLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
