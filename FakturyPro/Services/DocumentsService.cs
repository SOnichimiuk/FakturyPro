using FakturyPro.Data;
using FakturyPro.Data.Dto;
using FakturyPro.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using FakturyPro.Interfaces;

namespace FakturyPro.Services
{
    public class DocumentsService : IDocumentsService
    {
        public DocumentsService() { }
        
        public List<DocumentDto> GetDocuments()
        {
            using (var context = new FakturyDbContext())
            {
                return context.Documents.Select(x => new DocumentDto
                {
                    Id = x.Id,
                    DocumentNr = x.DocumentNr,
                    ClientId = x.ClientId,
                    CreationDate = x.CreationDate,
                    State = x.State,
                    SaleDate = x.SaleDate,
                    Type = x.Type
                }).ToList();
            }
        }

        public List<DocumentDto> GetOrders()
        {
            using (var context = new FakturyDbContext())
            {
                return context.Documents.Select(x => new DocumentDto
                {
                    Id = x.Id,
                    DocumentNr = x.DocumentNr,
                    ClientId = x.ClientId,
                    ClientName = x.Client.Name, 
                    CreationDate = x.CreationDate,
                    State = x.State,
                    SaleDate = x.SaleDate,
                    Type = x.Type,
                    Products = x.ProductDocuments.Select(p => new ProductDto
                    {
                        Id = p.Product.Id,
                        Name = p.Product.Name,
                        Quantity = p.Quantity,
                        PriceNetto = p.Product.PriceNetto,
                        VatRate = p.Product.VatRate
                    }).ToList()
                }).Where(x => x.Type == DocumentType.Order).ToList();
            }
        }

        public List<DocumentDto> GetInvoices()
        {
            using (var context = new FakturyDbContext())
            {
                return context.Documents.Select(x => new DocumentDto
                {
                    Id = x.Id,
                    DocumentNr = x.DocumentNr,
                    ClientId = x.ClientId,
                    ClientName = x.Client.Name, 
                    CreationDate = x.CreationDate,
                    State = x.State,
                    SaleDate = x.SaleDate,
                    Type = x.Type,
                    Products = x.ProductDocuments.Select(p => new ProductDto
                    {
                        Id = p.Product.Id,
                        Name = p.Product.Name,
                        Quantity = p.Quantity,
                        PriceNetto = p.Product.PriceNetto,
                        VatRate = p.Product.VatRate
                    }).ToList()
                }).Where(x => x.Type == DocumentType.Invoice).ToList();
            }
        }

        public void AddDocument(DocumentDto document)
        {
            var documentEntity = new Document
            {
                DocumentNr = document.DocumentNr,
                ClientId = document.ClientId,
                CreationDate = document.CreationDate,
                State = document.State,
                SaleDate = document.SaleDate,
                Type = document.Type
            };

            using (var context = new FakturyDbContext())
            {
                var selectedProductsIds = document.Products.Select(x => x.Id);

                var productDocuments = document.Products.Select(x => new ProductDocument
                {
                    ProductId = x.Id,
                    Quantity = x.Quantity,
                }).ToList();

                documentEntity.ProductDocuments = productDocuments;
                
                context.Documents.Add(documentEntity);
                context.SaveChanges();
            }
        }

        public void DeleteDocument(int id)
        {
            using (var context = new FakturyDbContext())
            {
                var documentEntity = context.Documents.FirstOrDefault(x => x.Id == id);
                
                if (documentEntity == null)
                {
                    throw new ArgumentException($"Nie znaleziono dokumentu o id {id}!");
                }
                
                context.Documents.Remove(documentEntity);
                context.SaveChanges();
            }
        }

        public void UpdateDocument(DocumentDto document)
        {
            using (var context = new FakturyDbContext())
            {
                var documentEntity = context.Documents.FirstOrDefault(x => x.Id == document.Id);

                if (documentEntity == null)
                {
                    throw new ArgumentException($"Nie znaleziono dokumentu o id {document.Id}!");
                }
                
                documentEntity.DocumentNr = document.DocumentNr;
                documentEntity.ClientId = document.ClientId;
                documentEntity.CreationDate = document.CreationDate;
                documentEntity.State = document.State;
                documentEntity.SaleDate = document.SaleDate;
                documentEntity.Type = document.Type;
                
                context.SaveChanges();
            }
        }
    }
}
