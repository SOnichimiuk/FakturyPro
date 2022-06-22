using System.Collections.Generic;
using FakturyPro.Data.Dto;

namespace FakturyPro.Interfaces
{
    public interface IClientsService
    {
        List<ClientDto> GetClients();
        void AddClient(ClientDto client);
        void DeleteClient(ClientDto client);
        void UpdateClient(ClientDto client);
    }
}