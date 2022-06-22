using System.Collections.Generic;
using FakturyPro.Data.Dto;

namespace FakturyPro.Interfaces
{
    public interface IProductsService
    {
        List<ProductDto> GetProducts();
        void AddProduct(ProductDto product);
        void DeleteProduct(int productId);
        void UpdateProduct(ProductDto product);
    }
}