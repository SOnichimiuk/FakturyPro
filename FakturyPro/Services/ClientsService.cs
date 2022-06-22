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

        public void DeleteClient(ClientDto client)
        {
            var clientEntity = new Client
            {
                Id = client.Id,
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
                context.Clients.Remove(clientEntity);
                context.SaveChanges();
                return;
            }
        }

        public void UpdateClient(ClientDto client)
        {
            using (var context = new FakturyDbContext())
            {
                context.Clients.FirstOrDefault(x => x.Id == client.Id).Name = client.Name;
                context.Clients.FirstOrDefault(x => x.Id == client.Id).NIP = client.NIP;
                context.Clients.FirstOrDefault(x => x.Id == client.Id).Adress = client.Adress;
                context.Clients.FirstOrDefault(x => x.Id == client.Id).City = client.City;
                context.Clients.FirstOrDefault(x => x.Id == client.Id).Email = client.Email;
                context.Clients.FirstOrDefault(x => x.Id == client.Id).PhoneNumber = client.PhoneNumber;
                context.Clients.FirstOrDefault(x => x.Id == client.Id).PostalCode = client.PostalCode;
                context.SaveChanges();
                return;
            }
        }
    }
}
