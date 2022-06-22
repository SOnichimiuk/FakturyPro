﻿using FakturyPro.Data;
using FakturyPro.Data.Dto;
using FakturyPro.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturyPro.Services
{
    public class DocumentsService
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
                    CreationDate = x.CreationDate,
                    State = x.State,
                    SaleDate = x.SaleDate,
                    Type = x.Type
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
                    CreationDate = x.CreationDate,
                    State = x.State,
                    SaleDate = x.SaleDate,
                    Type = x.Type
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
                context.Documents.Add(documentEntity);
                context.SaveChanges();
                return;
            }
        }

        public void DeleteDocument(DocumentDto document)
        {
            var documentEntity = new Document
            {
                Id = document.Id,
                DocumentNr = document.DocumentNr,
                ClientId = document.ClientId,
                CreationDate = document.CreationDate,
                State = document.State,
                SaleDate = document.SaleDate,
                Type = document.Type
            };

            using (var context = new FakturyDbContext())
            {
                context.Documents.Remove(documentEntity);
                context.SaveChanges();
                return;
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
