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
    public class ClientsService
    {
        public ClientsService() { }

        public List<ClientDto> GetClients()
        {
            using (var context = new FakturyDbContext())
            {
                return context.Clients.Select(x => new ClientDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Adress = x.Adress,
                    City = x.City,
                    PostalCode = x.PostalCode,
                    NIP = x.NIP,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                }).ToList();
            } 
        }

        public void AddClient(ClientDto client)
        {
            var clientEntity = new Client
            {
                Name = client.Name,
                Adress = client.Adress,
                City = client.City,
                PostalCode = client.PostalCode,
                NIP = client.NIP,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };

            using (var context = new FakturyDbContext())
            {
                context.Clients.Add(clientEntity);
                context.SaveChanges();
                return;
            }
        }

        public void DeleteClient(int Id)
        {

            using (var context = new FakturyDbContext())
            {
                var clientEntity = context.Clients.FirstOrDefault(x => x.Id == Id);
                if (clientEntity == null)
                {
                    throw new ArgumentException($"Nie znaleziono klienta o id {Id}");
                }
                context.Clients.Remove(clientEntity);
                context.SaveChanges();
                return;
            }
        }

        public void UpdateClient(ClientDto client)
        {
            using (var context = new FakturyDbContext())
            {
                var clientEntity = context.Clients.FirstOrDefault(x => x.Id == client.Id);

                if (clientEntity == null)
                {
                    throw new ArgumentException($"Nie znaleziono dokumentu o id {client.Id}!");
                }

                clientEntity.Name = client.Name;
                clientEntity.NIP = client.NIP;
                clientEntity.Adress = client.Adress;
                clientEntity.City = client.City;
                clientEntity.Email = client.Email;
                clientEntity.PhoneNumber = client.PhoneNumber;
                clientEntity.PostalCode = client.PostalCode;
                context.SaveChanges();
                return;
            }
        }
    }
}
