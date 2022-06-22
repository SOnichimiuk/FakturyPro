using FakturyPro.Data;
using FakturyPro.Data.Dto;
using FakturyPro.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturyPro.Services
{
    public class ProductsService
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

        public void DeleteProduct(ProductDto product)
        {
            var productEntity = new Product
            {
                Id = product.Id,
                Name = product.Name,
                VatRate = product.VatRate,
                PriceNetto = product.PriceNetto,
                Quantity = product.Quantity
            };

            using (var context = new FakturyDbContext())
            {
                context.Products.Remove(productEntity);
                context.SaveChanges();
                return;
            }
        }

        public void UpdateProduct(ProductDto product)
        {
            using (var context = new FakturyDbContext())
            {
                context.Products.FirstOrDefault(x => x.Id == product.Id).Name = product.Name;
                context.Products.FirstOrDefault(x => x.Id == product.Id).VatRate = product.VatRate;
                context.Products.FirstOrDefault(x => x.Id == product.Id).PriceNetto = product.PriceNetto;
                context.Products.FirstOrDefault(x => x.Id == product.Id).Quantity = product.Quantity;
                context.SaveChanges();
                return;
            }
        }
    }
}
