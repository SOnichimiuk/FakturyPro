using System.Collections.Generic;
using FakturyPro.Data.Dto;

namespace FakturyPro.Interfaces
{
    public interface IDocumentsService
    {
        List<DocumentDto> GetDocuments();
        List<DocumentDto> GetOrders();
        List<DocumentDto> GetInvoices();
        void AddDocument(DocumentDto document);
        void DeleteDocument(int id);
        void UpdateDocument(DocumentDto document);
    }
}