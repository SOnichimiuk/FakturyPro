using System.Collections.Generic;
using FakturyPro.Data.Dto;

namespace FakturyPro.Interfaces
{
    public interface IProductsService
    {
        List<ProductDto> GetProducts();
        void AddProduct(ProductDto product);
        void DeleteProduct(ProductDto product);
        void UpdateProduct(ProductDto product);
    }
}