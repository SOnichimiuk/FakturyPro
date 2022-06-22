using FakturyPro.Data;
using FakturyPro.Data.Dto;
using FakturyPro.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakturyPro.Interfaces;

namespace FakturyPro.Services
{
    public class ProductsService : IProductsService
    {
        public ProductsService() { }

        public List<ProductDto> GetProducts()
        {
            using (var context = new FakturyDbContext())
            {
                return context.Products.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    VatRate = x.VatRate,
                    PriceNetto = x.PriceNetto,
                    Quantity = x.Quantity
                }).ToList();
            }
        }

        public void AddProduct(ProductDto product)
        {
            var productEntity = new Product
            {
                Name = product.Name,
                VatRate = product.VatRate,
                PriceNetto = product.PriceNetto,
                Quantity = product.Quantity
            };

            using (var context = new FakturyDbContext())
            {
                context.Products.Add(productEntity);
                context.SaveChanges();
                return;
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new FakturyDbContext())
            {
                var productEntity = context.Products.FirstOrDefault(x => x.Id == id);

                if (productEntity == null)
                {
                    throw new ArgumentException($"Nie znaleziono produktu o id {id}");
                }

                context.Products.Remove(productEntity);
                context.SaveChanges();
                return;
            }
        }

        public void UpdateProduct(ProductDto product)
        {
            using (var context = new FakturyDbContext())
            {
                var productEntity = context.Products.FirstOrDefault(x => x.Id == product.Id);

                if (productEntity == null)
                {
                    throw new ArgumentException($"Nie znaleziono dokumentu o id {product.Id}!");
                }

                productEntity.Name = product.Name;
                productEntity.VatRate = product.VatRate;
                productEntity.PriceNetto = product.PriceNetto;
                productEntity.Quantity = product.Quantity;
                context.SaveChanges();
                return;
            }
        }
    }
}
